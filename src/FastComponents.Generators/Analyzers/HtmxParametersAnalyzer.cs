using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace FastComponents.Generators.Analyzers;

/// <summary>
/// Analyzer for detecting issues with HtmxComponentParameters and GenerateParameterMethods attribute usage.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class HtmxParametersAnalyzer : DiagnosticAnalyzer
{
    /// <summary>
    /// Diagnostic rule for when a record inheriting from HtmxComponentParameters is missing the [GenerateParameterMethods] attribute.
    /// </summary>
    public static readonly DiagnosticDescriptor MissingGenerateParameterMethodsAttributeRule = new(
        id: "FC0001",
        title: "Record inheriting from HtmxComponentParameters should use [GenerateParameterMethods] attribute",
        messageFormat: "Record '{0}' inherits from HtmxComponentParameters but is missing the [GenerateParameterMethods] attribute",
        category: "FastComponents.Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "Records that inherit from HtmxComponentParameters should use the [GenerateParameterMethods] attribute to automatically generate query string and binding methods.");

    /// <summary>
    /// Diagnostic rule for when a record with [GenerateParameterMethods] attribute is not declared as partial.
    /// </summary>
    public static readonly DiagnosticDescriptor NotPartialRecordRule = new(
        id: "FC0002",
        title: "Record with [GenerateParameterMethods] must be partial",
        messageFormat: "Record '{0}' has [GenerateParameterMethods] attribute but is not declared as partial",
        category: "FastComponents.Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Records with the [GenerateParameterMethods] attribute must be declared as partial to allow source generation.");

    /// <summary>
    /// Diagnostic rule for when a record with [GenerateParameterMethods] attribute does not inherit from HtmxComponentParameters.
    /// </summary>
    public static readonly DiagnosticDescriptor NotInheritingFromHtmxComponentParametersRule = new(
        id: "FC0003",
        title: "Record with [GenerateParameterMethods] must inherit from HtmxComponentParameters",
        messageFormat: "Record '{0}' has [GenerateParameterMethods] attribute but does not inherit from HtmxComponentParameters",
        category: "FastComponents.Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Records with the [GenerateParameterMethods] attribute must inherit from HtmxComponentParameters to enable code generation.");

    /// <summary>
    /// Diagnostic rule for when properties in a record with [GenerateParameterMethods] are not init-only.
    /// </summary>
    public static readonly DiagnosticDescriptor PropertyNotInitOnlyRule = new(
        id: "FC0004",
        title: "Properties in record with [GenerateParameterMethods] should be init-only",
        messageFormat: "Property '{0}' in record '{1}' should be init-only for proper parameter binding",
        category: "FastComponents.Usage",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        description: "Properties in records with [GenerateParameterMethods] should be init-only to ensure immutable parameter objects.");

    /// <summary>
    /// Diagnostic rule for when a record with [GenerateParameterMethods] manually implements generated methods.
    /// </summary>
    public static readonly DiagnosticDescriptor ManualImplementationWithAttributeRule = new(
        id: "FC0005",
        title: "Record with [GenerateParameterMethods] has manual implementation of generated methods",
        messageFormat: "Record '{0}' has [GenerateParameterMethods] attribute but manually implements '{1}' method",
        category: "FastComponents.Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "Records with [GenerateParameterMethods] attribute should not manually implement BuildQueryString or BindFromQuery methods as they will be auto-generated.");

    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        => ImmutableArray.Create(
            MissingGenerateParameterMethodsAttributeRule,
            NotPartialRecordRule,
            NotInheritingFromHtmxComponentParametersRule,
            PropertyNotInitOnlyRule,
            ManualImplementationWithAttributeRule);

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeRecordDeclaration, SyntaxKind.RecordDeclaration);
    }

    private static void AnalyzeRecordDeclaration(SyntaxNodeAnalysisContext context)
    {
        var recordDeclaration = (RecordDeclarationSyntax)context.Node;
        SemanticModel semanticModel = context.SemanticModel;
        INamedTypeSymbol? symbol = semanticModel.GetDeclaredSymbol(recordDeclaration);

        if (symbol is not { } namedTypeSymbol)
        {
            return;
        }

        bool hasGenerateParameterMethodsAttribute = HasGenerateParameterMethodsAttribute(namedTypeSymbol);
        bool inheritsFromHtmxComponentParameters = InheritsFromHtmxComponentParameters(namedTypeSymbol);

        // FC0001: Missing [GenerateParameterMethods] attribute when inheriting from HtmxComponentParameters
        if (inheritsFromHtmxComponentParameters && !hasGenerateParameterMethodsAttribute)
        {
            Diagnostic diagnostic = Diagnostic.Create(
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
                Diagnostic diagnostic = Diagnostic.Create(
                    NotPartialRecordRule,
                    recordDeclaration.Identifier.GetLocation(),
                    namedTypeSymbol.Name);
                context.ReportDiagnostic(diagnostic);
            }

            // FC0003: Record with attribute must inherit from HtmxComponentParameters
            if (!inheritsFromHtmxComponentParameters)
            {
                Diagnostic diagnostic = Diagnostic.Create(
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
            a.AttributeClass?.Name == "GenerateParameterMethodsAttribute"
                || a.AttributeClass?.ToDisplayString() == "FastComponents.GenerateParameterMethodsAttribute");
    }

    private static bool InheritsFromHtmxComponentParameters(INamedTypeSymbol symbol)
    {
        INamedTypeSymbol? baseType = symbol.BaseType;
        while (baseType is not null)
        {
            if (baseType.Name == "HtmxComponentParameters"
                && baseType.ContainingNamespace.ToDisplayString() == "FastComponents")
            {
                return true;
            }

            baseType = baseType.BaseType;
        }

        return false;
    }

    private static void AnalyzeProperties(
        in SyntaxNodeAnalysisContext context,
        RecordDeclarationSyntax recordDeclaration,
        INamedTypeSymbol namedTypeSymbol)
    {
        IEnumerable<IPropertySymbol> properties = namedTypeSymbol.GetMembers()
            .OfType<IPropertySymbol>()
            .Where(p => p.DeclaredAccessibility == Accessibility.Public);

        foreach (IPropertySymbol? property in properties)
        {
            if (property.SetMethod is not null && !property.SetMethod.IsInitOnly)
            {
                // Find the property syntax to get its location
                PropertyDeclarationSyntax? propertySyntax = recordDeclaration.Members
                    .OfType<PropertyDeclarationSyntax>()
                    .FirstOrDefault(p => p.Identifier.ValueText == property.Name);

                if (propertySyntax is not null)
                {
                    Diagnostic diagnostic = Diagnostic.Create(
                        PropertyNotInitOnlyRule,
                        propertySyntax.Identifier.GetLocation(),
                        property.Name,
                        namedTypeSymbol.Name);
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }

    private static void AnalyzeManualImplementations(
        in SyntaxNodeAnalysisContext context,
        RecordDeclarationSyntax recordDeclaration,
        INamedTypeSymbol namedTypeSymbol)
    {
        IEnumerable<IMethodSymbol> methods = namedTypeSymbol.GetMembers()
            .OfType<IMethodSymbol>()
            .Where(m => m.Name is "BuildQueryString" or "BindFromQuery");

        foreach (IMethodSymbol? method in methods)
        {
            // Find the method syntax to get its location
            MethodDeclarationSyntax? methodSyntax = recordDeclaration.Members
                .OfType<MethodDeclarationSyntax>()
                .FirstOrDefault(m => m.Identifier.ValueText == method.Name);

            if (methodSyntax is not null)
            {
                Diagnostic diagnostic = Diagnostic.Create(
                    ManualImplementationWithAttributeRule,
                    methodSyntax.Identifier.GetLocation(),
                    namedTypeSymbol.Name,
                    method.Name);
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
