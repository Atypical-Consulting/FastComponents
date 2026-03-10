using FastComponents.Generators.UnitTests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;

namespace FastComponents.Generators.UnitTests;

/// <summary>
/// Tests for the HtmxParametersGenerator source generator.
/// </summary>
public class HtmxParametersGeneratorTests
{
    [Fact]
    public void Generator_WithIntProperty_ProducesBuildQueryStringAndBindFromQuery()
    {
        // Arrange
        const string source = """
            using FastComponents;

            namespace TestNamespace;

            [GenerateParameterMethods]
            public partial record TestParams : HtmxComponentParameters
            {
                public int Count { get; init; } = 0;
            }
            """;

        // Act
        GeneratorDriverRunResult result = CompilationHelper.CreateAndRunGenerator(source);

        // Assert
        result.Diagnostics.ShouldBeEmpty();
        result.GeneratedTrees.Length.ShouldBe(1);

        string generatedCode = result.GeneratedTrees[0].GetText().ToString();
        generatedCode.ShouldContain("protected override string BuildQueryString()");
        generatedCode.ShouldContain("public override HtmxComponentParameters BindFromQuery(IQueryCollection query)");
        generatedCode.ShouldContain("GetQueryInt(query, \"Count\")");
    }

    [Fact]
    public void Generator_WithStringProperty_ProducesUrlEncodedQueryString()
    {
        // Arrange
        const string source = """
            using FastComponents;

            namespace TestNamespace;

            [GenerateParameterMethods]
            public partial record TestParams : HtmxComponentParameters
            {
                public string Name { get; init; } = string.Empty;
            }
            """;

        // Act
        GeneratorDriverRunResult result = CompilationHelper.CreateAndRunGenerator(source);

        // Assert
        result.GeneratedTrees.Length.ShouldBe(1);

        string generatedCode = result.GeneratedTrees[0].GetText().ToString();
        generatedCode.ShouldContain("System.Uri.EscapeDataString(Name)");
        generatedCode.ShouldContain("GetQueryValue(query, \"Name\")");
    }

    [Fact]
    public void Generator_WithBoolProperty_ProducesGetQueryBoolHelper()
    {
        // Arrange
        const string source = """
            using FastComponents;

            namespace TestNamespace;

            [GenerateParameterMethods]
            public partial record TestParams : HtmxComponentParameters
            {
                public bool IsActive { get; init; }
            }
            """;

        // Act
        GeneratorDriverRunResult result = CompilationHelper.CreateAndRunGenerator(source);

        // Assert
        result.GeneratedTrees.Length.ShouldBe(1);

        string generatedCode = result.GeneratedTrees[0].GetText().ToString();
        generatedCode.ShouldContain("GetQueryBool(query, \"IsActive\")");
        generatedCode.ShouldContain("private static bool? GetQueryBool(IQueryCollection query, string key)");
        generatedCode.ShouldContain("bool.TryParse");
    }

    [Fact]
    public void Generator_WithMultipleProperties_GeneratesAllBindings()
    {
        // Arrange
        const string source = """
            using FastComponents;

            namespace TestNamespace;

            [GenerateParameterMethods]
            public partial record TestParams : HtmxComponentParameters
            {
                public int Page { get; init; } = 1;
                public string Query { get; init; } = string.Empty;
                public bool ShowAll { get; init; }
            }
            """;

        // Act
        GeneratorDriverRunResult result = CompilationHelper.CreateAndRunGenerator(source);

        // Assert
        result.GeneratedTrees.Length.ShouldBe(1);

        string generatedCode = result.GeneratedTrees[0].GetText().ToString();

        // BuildQueryString should handle all three types
        generatedCode.ShouldContain("Page");
        generatedCode.ShouldContain("Query");
        generatedCode.ShouldContain("ShowAll");

        // BindFromQuery should use appropriate helpers
        generatedCode.ShouldContain("GetQueryInt(query, \"Page\")");
        generatedCode.ShouldContain("GetQueryValue(query, \"Query\")");
        generatedCode.ShouldContain("GetQueryBool(query, \"ShowAll\")");
    }

    [Fact]
    public void Generator_WithoutAttribute_DoesNotProduceOutput()
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
        GeneratorDriverRunResult result = CompilationHelper.CreateAndRunGenerator(source);

        // Assert
        result.GeneratedTrees.ShouldBeEmpty();
    }

    [Fact]
    public void Generator_ForNonRecord_DoesNotProduceOutput()
    {
        // Arrange
        const string source = """
            using FastComponents;

            namespace TestNamespace;

            [GenerateParameterMethods]
            public partial class TestParams : HtmxComponentParameters
            {
                public int Count { get; init; } = 0;
            }
            """;

        // Act
        GeneratorDriverRunResult result = CompilationHelper.CreateAndRunGenerator(source);

        // Assert
        result.GeneratedTrees.ShouldBeEmpty();
    }

    [Fact]
    public void Generator_WithNoProperties_GeneratesCommentOnly()
    {
        // Arrange
        const string source = """
            using FastComponents;

            namespace TestNamespace;

            [GenerateParameterMethods]
            public partial record EmptyParams : HtmxComponentParameters;
            """;

        // Act
        GeneratorDriverRunResult result = CompilationHelper.CreateAndRunGenerator(source);

        // Assert
        result.GeneratedTrees.Length.ShouldBe(1);

        string generatedCode = result.GeneratedTrees[0].GetText().ToString();
        generatedCode.ShouldContain("No properties found to generate methods for");
    }

    [Fact]
    public void Generator_WithSkipDefaultsFalse_OmitsDefaultValueChecks()
    {
        // Arrange
        const string source = """
            using FastComponents;

            namespace TestNamespace;

            [GenerateParameterMethods(SkipDefaults = false)]
            public partial record TestParams : HtmxComponentParameters
            {
                public int Count { get; init; } = 10;
            }
            """;

        // Act
        GeneratorDriverRunResult result = CompilationHelper.CreateAndRunGenerator(source);

        // Assert
        result.GeneratedTrees.Length.ShouldBe(1);

        string generatedCode = result.GeneratedTrees[0].GetText().ToString();
        // Without SkipDefaults, should NOT compare against default values
        generatedCode.ShouldNotContain("var defaultValue");
    }

    [Fact]
    public void Generator_WithSkipDefaultsTrue_IncludesDefaultValueChecks()
    {
        // Arrange
        const string source = """
            using FastComponents;

            namespace TestNamespace;

            [GenerateParameterMethods(SkipDefaults = true)]
            public partial record TestParams : HtmxComponentParameters
            {
                public int Count { get; init; } = 10;
            }
            """;

        // Act
        GeneratorDriverRunResult result = CompilationHelper.CreateAndRunGenerator(source);

        // Assert
        result.GeneratedTrees.Length.ShouldBe(1);

        string generatedCode = result.GeneratedTrees[0].GetText().ToString();
        // With SkipDefaults (default), should compare against default values
        generatedCode.ShouldContain("var defaultValue");
    }

    [Fact]
    public void Generator_WithNotInheritingHtmxComponentParameters_DoesNotProduceOutput()
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
        GeneratorDriverRunResult result = CompilationHelper.CreateAndRunGenerator(source);

        // Assert
        result.GeneratedTrees.ShouldBeEmpty();
    }

    [Fact]
    public void Generator_OutputFileName_MatchesRecordName()
    {
        // Arrange
        const string source = """
            using FastComponents;

            namespace TestNamespace;

            [GenerateParameterMethods]
            public partial record MyCustomParams : HtmxComponentParameters
            {
                public int Value { get; init; }
            }
            """;

        // Act
        GeneratorDriverRunResult result = CompilationHelper.CreateAndRunGenerator(source);

        // Assert
        result.GeneratedTrees.Length.ShouldBe(1);
        result.GeneratedTrees[0].FilePath.ShouldEndWith("MyCustomParams.g.cs");
    }

    [Fact]
    public void Generator_PreservesNamespace()
    {
        // Arrange
        const string source = """
            using FastComponents;

            namespace My.Deep.Namespace;

            [GenerateParameterMethods]
            public partial record TestParams : HtmxComponentParameters
            {
                public int Count { get; init; }
            }
            """;

        // Act
        GeneratorDriverRunResult result = CompilationHelper.CreateAndRunGenerator(source);

        // Assert
        string generatedCode = result.GeneratedTrees[0].GetText().ToString();
        generatedCode.ShouldContain("namespace My.Deep.Namespace");
    }

    [Fact]
    public void Generator_ProducesValidCSharp()
    {
        // Arrange
        const string source = """
            using FastComponents;

            namespace TestNamespace;

            [GenerateParameterMethods]
            public partial record CounterParams : HtmxComponentParameters
            {
                public int Count { get; init; } = 10;
                public string Label { get; init; } = "default";
                public bool IsVisible { get; init; }
            }
            """;

        // Act
        GeneratorDriverRunResult result = CompilationHelper.CreateAndRunGenerator(source);

        // Assert
        string generatedCode = result.GeneratedTrees[0].GetText().ToString();

        // The generated code should be parseable C#
        SyntaxTree parsedTree = CSharpSyntaxTree.ParseText(generatedCode);
        parsedTree.GetDiagnostics().ShouldBeEmpty("Generated code should have no syntax errors");
    }

    [Fact]
    public void Generator_BindFromQuery_UsesWithExpression()
    {
        // Arrange
        const string source = """
            using FastComponents;

            namespace TestNamespace;

            [GenerateParameterMethods]
            public partial record TestParams : HtmxComponentParameters
            {
                public int Page { get; init; } = 1;
            }
            """;

        // Act
        GeneratorDriverRunResult result = CompilationHelper.CreateAndRunGenerator(source);

        // Assert
        string generatedCode = result.GeneratedTrees[0].GetText().ToString();
        generatedCode.ShouldContain("return this with");
    }

    [Fact]
    public void Generator_BuildQueryString_UsesListOfParts()
    {
        // Arrange
        const string source = """
            using FastComponents;

            namespace TestNamespace;

            [GenerateParameterMethods]
            public partial record TestParams : HtmxComponentParameters
            {
                public int Page { get; init; } = 1;
            }
            """;

        // Act
        GeneratorDriverRunResult result = CompilationHelper.CreateAndRunGenerator(source);

        // Assert
        string generatedCode = result.GeneratedTrees[0].GetText().ToString();
        generatedCode.ShouldContain("var parts = new System.Collections.Generic.List<string>()");
        generatedCode.ShouldContain("return string.Join(\"&\", parts)");
    }

    [Fact]
    public void Generator_WithOnlySetProperty_IgnoresNonInitProperties()
    {
        // Arrange - property with set (not init) should be ignored by the generator
        const string source = """
            using FastComponents;

            namespace TestNamespace;

            [GenerateParameterMethods]
            public partial record TestParams : HtmxComponentParameters
            {
                public int ReadWrite { get; set; } = 0;
                public int InitOnly { get; init; } = 0;
            }
            """;

        // Act
        GeneratorDriverRunResult result = CompilationHelper.CreateAndRunGenerator(source);

        // Assert
        result.GeneratedTrees.Length.ShouldBe(1);

        string generatedCode = result.GeneratedTrees[0].GetText().ToString();
        // Only init-only properties should be included
        generatedCode.ShouldContain("InitOnly");
        generatedCode.ShouldNotContain("ReadWrite");
    }

    [Fact]
    public void Generator_BoolProperty_UsesToLowerInvariant()
    {
        // Arrange
        const string source = """
            using FastComponents;

            namespace TestNamespace;

            [GenerateParameterMethods]
            public partial record TestParams : HtmxComponentParameters
            {
                public bool Enabled { get; init; }
            }
            """;

        // Act
        GeneratorDriverRunResult result = CompilationHelper.CreateAndRunGenerator(source);

        // Assert
        string generatedCode = result.GeneratedTrees[0].GetText().ToString();
        generatedCode.ShouldContain("ToLowerInvariant()");
    }

    [Fact]
    public void Generator_StringProperty_ChecksNullOrEmpty()
    {
        // Arrange
        const string source = """
            using FastComponents;

            namespace TestNamespace;

            [GenerateParameterMethods]
            public partial record TestParams : HtmxComponentParameters
            {
                public string Name { get; init; } = string.Empty;
            }
            """;

        // Act
        GeneratorDriverRunResult result = CompilationHelper.CreateAndRunGenerator(source);

        // Assert
        string generatedCode = result.GeneratedTrees[0].GetText().ToString();
        generatedCode.ShouldContain("string.IsNullOrEmpty(Name)");
    }
}
