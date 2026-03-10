using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace FastComponents.Generators.UnitTests.Helpers;

/// <summary>
/// Helper methods for creating compilations in generator tests.
/// </summary>
public static class CompilationHelper
{
    /// <summary>
    /// Stub source that provides the minimal types needed by the generator.
    /// </summary>
    private const string TypeStubs = """
        namespace Microsoft.AspNetCore.Http
        {
            public interface IQueryCollection : System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues>>
            {
                bool TryGetValue(string key, out Microsoft.Extensions.Primitives.StringValues value);
            }
        }

        namespace Microsoft.Extensions.Primitives
        {
            public readonly struct StringValues
            {
                public static readonly StringValues Empty;
                public static implicit operator string(StringValues values) => string.Empty;
                public override string ToString() => string.Empty;
            }
        }

        namespace FastComponents
        {
            [System.AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
            public sealed class GenerateParameterMethodsAttribute : System.Attribute
            {
                public bool SkipDefaults { get; set; } = true;
            }

            public abstract record HtmxComponentParameters
            {
                public string ToComponentUrl(string route)
                {
                    string queryString = BuildQueryString();
                    return string.IsNullOrEmpty(queryString) ? route : $"{route}?{queryString}";
                }

                protected virtual string BuildQueryString()
                    => throw new System.InvalidOperationException();

                public virtual HtmxComponentParameters BindFromQuery(Microsoft.AspNetCore.Http.IQueryCollection query)
                    => throw new System.InvalidOperationException();

                protected static string? GetQueryValue(Microsoft.AspNetCore.Http.IQueryCollection query, string key)
                    => null;

                protected static int? GetQueryInt(Microsoft.AspNetCore.Http.IQueryCollection query, string key)
                    => null;
            }
        }
        """;

    /// <summary>
    /// Creates a CSharpCompilation with the given source code and type stubs.
    /// </summary>
    public static CSharpCompilation CreateCompilation(string source)
    {
        SyntaxTree sourceSyntaxTree = CSharpSyntaxTree.ParseText(source);
        SyntaxTree stubSyntaxTree = CSharpSyntaxTree.ParseText(TypeStubs);

        List<MetadataReference> references = GetMetadataReferences();

        return CSharpCompilation.Create(
            "TestAssembly",
            [sourceSyntaxTree, stubSyntaxTree],
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
    }

    /// <summary>
    /// Runs the HtmxParametersGenerator on the given compilation and returns the result.
    /// </summary>
    public static GeneratorDriverRunResult RunGenerator(CSharpCompilation compilation)
    {
        HtmxParametersGenerator generator = new();

        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);
        driver = driver.RunGeneratorsAndUpdateCompilation(
            compilation,
            out _,
            out _);

        return driver.GetRunResult();
    }

    /// <summary>
    /// Creates a compilation and runs the generator, returning the result.
    /// </summary>
    public static GeneratorDriverRunResult CreateAndRunGenerator(string source)
    {
        CSharpCompilation compilation = CreateCompilation(source);
        return RunGenerator(compilation);
    }

    private static List<MetadataReference> GetMetadataReferences()
    {
        List<MetadataReference> references = [];

        // Add essential runtime references
        string runtimeDir = Path.GetDirectoryName(typeof(object).Assembly.Location)!;

        string[] essentialAssemblies =
        [
            "System.Runtime.dll",
            "System.Collections.dll",
            "System.Linq.dll",
            "netstandard.dll"
        ];

        references.Add(MetadataReference.CreateFromFile(typeof(object).Assembly.Location));

        foreach (string assembly in essentialAssemblies)
        {
            string path = Path.Combine(runtimeDir, assembly);
            if (File.Exists(path))
            {
                references.Add(MetadataReference.CreateFromFile(path));
            }
        }

        // Also add all currently loaded assemblies that are likely needed
        foreach (Assembly loadedAssembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            if (!loadedAssembly.IsDynamic && !string.IsNullOrEmpty(loadedAssembly.Location))
            {
                string name = loadedAssembly.GetName().Name ?? string.Empty;
                if (name.StartsWith("System.", StringComparison.Ordinal) || name == "mscorlib")
                {
                    references.Add(MetadataReference.CreateFromFile(loadedAssembly.Location));
                }
            }
        }

        return references;
    }
}
