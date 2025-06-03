using System.Collections.Immutable;
using System.Composition;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using FastComponents.Generators.Analyzers;

namespace FastComponents.Generators.CodeFixes;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(HtmxParametersCodeFixProvider)), Shared]
public class HtmxParametersCodeFixProvider : CodeFixProvider
{
    public sealed override ImmutableArray<string> FixableDiagnosticIds =>
        ImmutableArray.Create(
            HtmxParametersAnalyzer.MissingGenerateParameterMethodsAttributeRule.Id,
            HtmxParametersAnalyzer.NotPartialRecordRule.Id,
            HtmxParametersAnalyzer.PropertyNotInitOnlyRule.Id);

    public sealed override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

    public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
        if (root == null) return;

        foreach (var diagnostic in context.Diagnostics)
        {
            switch (diagnostic.Id)
            {
                case "FC0001":
                    RegisterAddAttributeCodeFix(context, root, diagnostic);
                    break;
                case "FC0002":
                    RegisterAddPartialModifierCodeFix(context, root, diagnostic);
                    break;
                case "FC0004":
                    RegisterMakePropertyInitOnlyCodeFix(context, root, diagnostic);
                    break;
            }
        }
    }

    private static void RegisterAddAttributeCodeFix(CodeFixContext context, SyntaxNode root, Diagnostic diagnostic)
    {
        var diagnosticSpan = diagnostic.Location.SourceSpan;
        var recordDeclaration = root.FindToken(diagnosticSpan.Start).Parent?.AncestorsAndSelf().OfType<RecordDeclarationSyntax>().FirstOrDefault();
        
        if (recordDeclaration == null) return;

        var action = CodeAction.Create(
            title: "Add [GenerateParameterMethods] attribute",
            createChangedDocument: cancellationToken => AddGenerateParameterMethodsAttribute(context.Document, recordDeclaration, cancellationToken),
            equivalenceKey: "AddGenerateParameterMethodsAttribute");

        context.RegisterCodeFix(action, diagnostic);
    }

    private static void RegisterAddPartialModifierCodeFix(CodeFixContext context, SyntaxNode root, Diagnostic diagnostic)
    {
        var diagnosticSpan = diagnostic.Location.SourceSpan;
        var recordDeclaration = root.FindToken(diagnosticSpan.Start).Parent?.AncestorsAndSelf().OfType<RecordDeclarationSyntax>().FirstOrDefault();
        
        if (recordDeclaration == null) return;

        var action = CodeAction.Create(
            title: "Add 'partial' modifier",
            createChangedDocument: cancellationToken => AddPartialModifier(context.Document, recordDeclaration, cancellationToken),
            equivalenceKey: "AddPartialModifier");

        context.RegisterCodeFix(action, diagnostic);
    }

    private static void RegisterMakePropertyInitOnlyCodeFix(CodeFixContext context, SyntaxNode root, Diagnostic diagnostic)
    {
        var diagnosticSpan = diagnostic.Location.SourceSpan;
        var propertyDeclaration = root.FindToken(diagnosticSpan.Start).Parent?.AncestorsAndSelf().OfType<PropertyDeclarationSyntax>().FirstOrDefault();
        
        if (propertyDeclaration == null) return;

        var action = CodeAction.Create(
            title: "Make property init-only",
            createChangedDocument: cancellationToken => MakePropertyInitOnly(context.Document, propertyDeclaration, cancellationToken),
            equivalenceKey: "MakePropertyInitOnly");

        context.RegisterCodeFix(action, diagnostic);
    }

    private static async Task<Document> AddGenerateParameterMethodsAttribute(Document document, RecordDeclarationSyntax recordDeclaration, CancellationToken cancellationToken)
    {
        var root = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
        if (root == null) return document;

        // Create the attribute
        var attributeList = SyntaxFactory.AttributeList(
            SyntaxFactory.SingletonSeparatedList(
                SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("GenerateParameterMethods"))));

        // Add the attribute to the record
        var newRecordDeclaration = recordDeclaration.WithAttributeLists(
            recordDeclaration.AttributeLists.Add(attributeList));

        var newRoot = root.ReplaceNode(recordDeclaration, newRecordDeclaration);

        // Add using statement if not present
        var compilationUnit = newRoot as CompilationUnitSyntax;
        if (compilationUnit != null && !HasFastComponentsUsing(compilationUnit))
        {
            var usingDirective = SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName("FastComponents"));
            newRoot = compilationUnit.WithUsings(compilationUnit.Usings.Add(usingDirective));
        }

        return document.WithSyntaxRoot(newRoot);
    }

    private static async Task<Document> AddPartialModifier(Document document, RecordDeclarationSyntax recordDeclaration, CancellationToken cancellationToken)
    {
        var root = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
        if (root == null) return document;

        var partialModifier = SyntaxFactory.Token(SyntaxKind.PartialKeyword);
        var newRecordDeclaration = recordDeclaration.WithModifiers(
            recordDeclaration.Modifiers.Add(partialModifier));

        var newRoot = root.ReplaceNode(recordDeclaration, newRecordDeclaration);
        return document.WithSyntaxRoot(newRoot);
    }

    private static async Task<Document> MakePropertyInitOnly(Document document, PropertyDeclarationSyntax propertyDeclaration, CancellationToken cancellationToken)
    {
        var root = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
        if (root == null) return document;

        var accessorList = propertyDeclaration.AccessorList;
        if (accessorList == null) return document;

        var setter = accessorList.Accessors.FirstOrDefault(a => a.IsKind(SyntaxKind.SetAccessorDeclaration));
        if (setter == null) return document;

        // Replace 'set' with 'init'
        var initAccessor = setter.WithKeyword(SyntaxFactory.Token(SyntaxKind.InitKeyword));
        var newAccessorList = accessorList.WithAccessors(
            accessorList.Accessors.Replace(setter, initAccessor));

        var newPropertyDeclaration = propertyDeclaration.WithAccessorList(newAccessorList);
        var newRoot = root.ReplaceNode(propertyDeclaration, newPropertyDeclaration);

        return document.WithSyntaxRoot(newRoot);
    }

    private static bool HasFastComponentsUsing(CompilationUnitSyntax compilationUnit)
    {
        return compilationUnit.Usings.Any(u => 
            u.Name?.ToString() == "FastComponents");
    }
}