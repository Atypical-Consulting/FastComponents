using System.Collections.Immutable;
using FastComponents.Generators.Analyzers;
using FastComponents.Generators.UnitTests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Shouldly;

namespace FastComponents.Generators.UnitTests;

/// <summary>
/// Tests for the HtmxParametersAnalyzer diagnostic analyzer.
/// </summary>
public class HtmxParametersAnalyzerTests
{
    [Fact]
    public async Task FC0001_RecordInheritingHtmxComponentParameters_WithoutAttribute_ReportsWarning()
    {
        // Arrange
        const string source = """
            using FastComponents;

            namespace TestNamespace;

            public partial record TestParams : HtmxComponentParameters
            {
                public int Count { get; init; } = 0;
            }
            """;

        // Act
        ImmutableArray<Diagnostic> diagnostics = await GetAnalyzerDiagnosticsAsync(source).ConfigureAwait(true);

        // Assert
        diagnostics.ShouldContain(d => d.Id == "FC0001");
        Diagnostic fc0001 = diagnostics.First(d => d.Id == "FC0001");
        fc0001.Severity.ShouldBe(DiagnosticSeverity.Warning);
        fc0001.GetMessage().ShouldContain("TestParams");
    }

    [Fact]
    public async Task FC0002_NonPartialRecordWithAttribute_ReportsError()
    {
        // Arrange
        const string source = """
            using FastComponents;

            namespace TestNamespace;

            [GenerateParameterMethods]
            public record TestParams : HtmxComponentParameters
            {
                public int Count { get; init; } = 0;
            }
            """;

        // Act
        ImmutableArray<Diagnostic> diagnostics = await GetAnalyzerDiagnosticsAsync(source).ConfigureAwait(true);

        // Assert
        diagnostics.ShouldContain(d => d.Id == "FC0002");
        Diagnostic fc0002 = diagnostics.First(d => d.Id == "FC0002");
        fc0002.Severity.ShouldBe(DiagnosticSeverity.Error);
    }

    [Fact]
    public async Task FC0003_RecordWithAttribute_NotInheritingHtmxComponentParameters_ReportsError()
    {
        // Arrange
        const string source = """
            using FastComponents;

            namespace TestNamespace;

            [GenerateParameterMethods]
            public partial record TestParams
            {
                public int Count { get; init; } = 0;
            }
            """;

        // Act
        ImmutableArray<Diagnostic> diagnostics = await GetAnalyzerDiagnosticsAsync(source).ConfigureAwait(true);

        // Assert
        diagnostics.ShouldContain(d => d.Id == "FC0003");
        Diagnostic fc0003 = diagnostics.First(d => d.Id == "FC0003");
        fc0003.Severity.ShouldBe(DiagnosticSeverity.Error);
    }

    [Fact]
    public async Task FC0004_PropertyWithSet_ReportsInfo()
    {
        // Arrange
        const string source = """
            using FastComponents;

            namespace TestNamespace;

            [GenerateParameterMethods]
            public partial record TestParams : HtmxComponentParameters
            {
                public int Count { get; set; } = 0;
            }
            """;

        // Act
        ImmutableArray<Diagnostic> diagnostics = await GetAnalyzerDiagnosticsAsync(source).ConfigureAwait(true);

        // Assert
        diagnostics.ShouldContain(d => d.Id == "FC0004");
        Diagnostic fc0004 = diagnostics.First(d => d.Id == "FC0004");
        fc0004.Severity.ShouldBe(DiagnosticSeverity.Info);
        fc0004.GetMessage().ShouldContain("Count");
    }

    [Fact]
    public async Task FC0005_ManualBuildQueryStringWithAttribute_ReportsWarning()
    {
        // Arrange
        const string source = """
            using FastComponents;

            namespace TestNamespace;

            [GenerateParameterMethods]
            public partial record TestParams : HtmxComponentParameters
            {
                public int Count { get; init; } = 0;

                protected override string BuildQueryString() => "Count=" + Count;
            }
            """;

        // Act
        ImmutableArray<Diagnostic> diagnostics = await GetAnalyzerDiagnosticsAsync(source).ConfigureAwait(true);

        // Assert
        diagnostics.ShouldContain(d => d.Id == "FC0005");
        Diagnostic fc0005 = diagnostics.First(d => d.Id == "FC0005");
        fc0005.Severity.ShouldBe(DiagnosticSeverity.Warning);
        fc0005.GetMessage().ShouldContain("BuildQueryString");
    }

    [Fact]
    public async Task FC0005_ManualBindFromQueryWithAttribute_ReportsWarning()
    {
        // Arrange
        const string source = """
            using FastComponents;

            namespace TestNamespace;

            [GenerateParameterMethods]
            public partial record TestParams : HtmxComponentParameters
            {
                public int Count { get; init; } = 0;

                public override HtmxComponentParameters BindFromQuery(Microsoft.AspNetCore.Http.IQueryCollection query) => this;
            }
            """;

        // Act
        ImmutableArray<Diagnostic> diagnostics = await GetAnalyzerDiagnosticsAsync(source).ConfigureAwait(true);

        // Assert
        diagnostics.ShouldContain(d => d.Id == "FC0005");
    }

    [Fact]
    public async Task CorrectUsage_ReportsNoDiagnostics()
    {
        // Arrange
        const string source = """
            using FastComponents;

            namespace TestNamespace;

            [GenerateParameterMethods]
            public partial record TestParams : HtmxComponentParameters
            {
                public int Count { get; init; } = 0;
                public string Name { get; init; } = string.Empty;
            }
            """;

        // Act
        ImmutableArray<Diagnostic> diagnostics = await GetAnalyzerDiagnosticsAsync(source).ConfigureAwait(true);

        // Assert
        diagnostics.ShouldBeEmpty();
    }

    [Fact]
    public async Task RecordWithoutAttributeOrBaseClass_ReportsNoDiagnostics()
    {
        // Arrange
        const string source = """
            namespace TestNamespace;

            public record SimpleRecord
            {
                public int Value { get; init; }
            }
            """;

        // Act
        ImmutableArray<Diagnostic> diagnostics = await GetAnalyzerDiagnosticsAsync(source).ConfigureAwait(true);

        // Assert
        diagnostics.ShouldBeEmpty();
    }

    private static async Task<ImmutableArray<Diagnostic>> GetAnalyzerDiagnosticsAsync(string source)
    {
        CSharpCompilation compilation = CompilationHelper.CreateCompilation(source);
        HtmxParametersAnalyzer analyzer = new();

        CompilationWithAnalyzers compilationWithAnalyzers = compilation.WithAnalyzers([analyzer]);

        ImmutableArray<Diagnostic> diagnostics = await compilationWithAnalyzers.GetAnalyzerDiagnosticsAsync().ConfigureAwait(true);

        return diagnostics;
    }
}
