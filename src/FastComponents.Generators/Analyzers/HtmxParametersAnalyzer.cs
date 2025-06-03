using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace FastComponents.Generators.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class HtmxParametersAnalyzer : DiagnosticAnalyzer
{
    public static readonly DiagnosticDescriptor MissingGenerateParameterMethodsAttributeRule = new(
        id: "FC0001",
        title: "Record inheriting from HtmxComponentParameters should use [GenerateParameterMethods] attribute",
        messageFormat: "Record '{0}' inherits from HtmxComponentParameters but is missing the [GenerateParameterMethods] attribute",
        category: "FastComponents.Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "Records that inherit from HtmxComponentParameters should use the [GenerateParameterMethods] attribute to automatically generate query string and binding methods.");

    public static readonly DiagnosticDescriptor NotPartialRecordRule = new(
        id: "FC0002", 
        title: "Record with [GenerateParameterMethods] must be partial",
        messageFormat: "Record '{0}' has [GenerateParameterMethods] attribute but is not declared as partial",
        category: "FastComponents.Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Records with the [GenerateParameterMethods] attribute must be declared as partial to allow source generation.");

    public static readonly DiagnosticDescriptor NotInheritingFromHtmxComponentParametersRule = new(
        id: "FC0003",
        title: "Record with [GenerateParameterMethods] must inherit from HtmxComponentParameters",
        messageFormat: "Record '{0}' has [GenerateParameterMethods] attribute but does not inherit from HtmxComponentParameters",
        category: "FastComponents.Usage", 
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Records with the [GenerateParameterMethods] attribute must inherit from HtmxComponentParameters to enable code generation.");

    public static readonly DiagnosticDescriptor PropertyNotInitOnlyRule = new(
        id: "FC0004",
        title: "Properties in record with [GenerateParameterMethods] should be init-only",
        messageFormat: "Property '{0}' in record '{1}' should be init-only for proper parameter binding",
        category: "FastComponents.Usage",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        description: "Properties in records with [GenerateParameterMethods] should be init-only to ensure immutable parameter objects.");

    public static readonly DiagnosticDescriptor ManualImplementationWithAttributeRule = new(
        id: "FC0005",
        title: "Record with [GenerateParameterMethods] has manual implementation of generated methods",
        messageFormat: "Record '{0}' has [GenerateParameterMethods] attribute but manually implements '{1}' method",
        category: "FastComponents.Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "Records with [GenerateParameterMethods] attribute should not manually implement BuildQueryString or BindFromQuery methods as they will be auto-generated.");

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics =>
        ImmutableArray.Create(
            MissingGenerateParameterMethodsAttributeRule,
            NotPartialRecordRule,
            NotInheritingFromHtmxComponentParametersRule,
            PropertyNotInitOnlyRule,
            ManualImplementationWithAttributeRule);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeRecordDeclaration, SyntaxKind.RecordDeclaration);
    }

    private static void AnalyzeRecordDeclaration(SyntaxNodeAnalysisContext context)
    {
        var recordDeclaration = (RecordDeclarationSyntax)context.Node;
        var semanticModel = context.SemanticModel;
        var symbol = semanticModel.GetDeclaredSymbol(recordDeclaration);

        if (symbol is not INamedTypeSymbol namedTypeSymbol)
            return;

        bool hasGenerateParameterMethodsAttribute = HasGenerateParameterMethodsAttribute(namedTypeSymbol);
        bool inheritsFromHtmxComponentParameters = InheritsFromHtmxComponentParameters(namedTypeSymbol);

        // FC0001: Missing [GenerateParameterMethods] attribute when inheriting from HtmxComponentParameters
        if (inheritsFromHtmxComponentParameters && !hasGenerateParameterMethodsAttribute)
        {
            var diagnostic = Diagnostic.Create(
                MissingGenerateParameterMethodsAttributeRule,
                recordDeclaration.Identifier.GetLocation(),
                namedTypeSymbol.Name);
            context.ReportDiagnostic(diagnostic);
        }

        if (hasGenerateParameterMethodsAttribute)
        {
            // FC0002: Record with attribute must be partial
            if (!recordDeclaration.Modifiers.Any(SyntaxKind.PartialKeyword))
            {
                var diagnostic = Diagnostic.Create(
                    NotPartialRecordRule,
                    recordDeclaration.Identifier.GetLocation(),
                    namedTypeSymbol.Name);
                context.ReportDiagnostic(diagnostic);
            }

            // FC0003: Record with attribute must inherit from HtmxComponentParameters
            if (!inheritsFromHtmxComponentParameters)
            {
                var diagnostic = Diagnostic.Create(
                    NotInheritingFromHtmxComponentParametersRule,
                    recordDeclaration.Identifier.GetLocation(),
                    namedTypeSymbol.Name);
                context.ReportDiagnostic(diagnostic);
            }

            // FC0004: Properties should be init-only
            AnalyzeProperties(context, recordDeclaration, namedTypeSymbol);

            // FC0005: Check for manual implementation of generated methods
            AnalyzeManualImplementations(context, recordDeclaration, namedTypeSymbol);
        }
    }

    private static bool HasGenerateParameterMethodsAttribute(INamedTypeSymbol symbol)
    {
        return symbol.GetAttributes().Any(a =>
            a.AttributeClass?.Name == "GenerateParameterMethodsAttribute" ||
            a.AttributeClass?.ToDisplayString() == "FastComponents.GenerateParameterMethodsAttribute");
    }

    private static bool InheritsFromHtmxComponentParameters(INamedTypeSymbol symbol)
    {
        var baseType = symbol.BaseType;
        while (baseType != null)
        {
            if (baseType.Name == "HtmxComponentParameters" &&
                baseType.ContainingNamespace.ToDisplayString() == "FastComponents")
            {
                return true;
            }
            baseType = baseType.BaseType;
        }
        return false;
    }

    private static void AnalyzeProperties(SyntaxNodeAnalysisContext context, RecordDeclarationSyntax recordDeclaration, INamedTypeSymbol namedTypeSymbol)
    {
        var properties = namedTypeSymbol.GetMembers()
            .OfType<IPropertySymbol>()
            .Where(p => p.DeclaredAccessibility == Accessibility.Public);

        foreach (var property in properties)
        {
            if (property.SetMethod != null && !property.SetMethod.IsInitOnly)
            {
                // Find the property syntax to get its location
                var propertySyntax = recordDeclaration.Members
                    .OfType<PropertyDeclarationSyntax>()
                    .FirstOrDefault(p => p.Identifier.ValueText == property.Name);

                if (propertySyntax != null)
                {
                    var diagnostic = Diagnostic.Create(
                        PropertyNotInitOnlyRule,
                        propertySyntax.Identifier.GetLocation(),
                        property.Name,
                        namedTypeSymbol.Name);
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }

    private static void AnalyzeManualImplementations(SyntaxNodeAnalysisContext context, RecordDeclarationSyntax recordDeclaration, INamedTypeSymbol namedTypeSymbol)
    {
        var methods = namedTypeSymbol.GetMembers()
            .OfType<IMethodSymbol>()
            .Where(m => m.Name is "BuildQueryString" or "BindFromQuery");

        foreach (var method in methods)
        {
            // Find the method syntax to get its location
            var methodSyntax = recordDeclaration.Members
                .OfType<MethodDeclarationSyntax>()
                .FirstOrDefault(m => m.Identifier.ValueText == method.Name);

            if (methodSyntax != null)
            {
                var diagnostic = Diagnostic.Create(
                    ManualImplementationWithAttributeRule,
                    methodSyntax.Identifier.GetLocation(),
                    namedTypeSymbol.Name,
                    method.Name);
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}