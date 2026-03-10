# Contributing to FastComponents

Thank you for your interest in contributing to FastComponents! This guide will help you get started.

## Code of Conduct

By participating in this project, you agree to maintain a respectful and inclusive environment for everyone.

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) >= 9.0
- A code editor (VS Code, Visual Studio, Rider)
- Git

### Setup

```bash
# Clone the repository
git clone https://github.com/Atypical-Consulting/FastComponents.git
cd FastComponents

# Build the solution
dotnet build

# Run all tests
dotnet test

# Run the demo application
dotnet run --project demo/HtmxAppServer/HtmxAppServer.csproj
```

## Development Workflow

### Branch Naming

Use descriptive branch names with prefixes:

- `feature/` - New features (e.g., `feature/add-hx-on-attribute`)
- `fix/` - Bug fixes (e.g., `fix/query-string-encoding`)
- `docs/` - Documentation changes (e.g., `docs/update-api-reference`)
- `chore/` - Maintenance tasks (e.g., `chore/update-dependencies`)
- `refactor/` - Code refactoring (e.g., `refactor/simplify-endpoint-registration`)

### Making Changes

1. **Fork** the repository and create your branch from `dev`:
   ```bash
   git checkout dev
   git pull origin dev
   git checkout -b feature/your-feature-name
   ```

2. **Make your changes** following the code style guidelines below.

3. **Write or update tests** for any changed functionality.

4. **Run the full test suite** to ensure nothing is broken:
   ```bash
   dotnet test
   ```

5. **Build in Release mode** to verify no warnings:
   ```bash
   dotnet build --configuration Release
   ```

6. **Commit** using [Conventional Commits](https://www.conventionalcommits.org/):
   ```
   feat: add support for hx-on attribute
   fix: correct query string encoding for special characters
   docs: update getting started guide
   chore: upgrade AngleSharp to 1.5.0
   refactor: simplify convention-based registration
   test: add generator tests for bool properties
   ```

7. **Push** your branch and **open a Pull Request** against `dev`.

## Code Style Guidelines

### General

- Follow the existing `.editorconfig` rules (enforced at build time)
- Use file-scoped namespaces
- Enable nullable reference types
- Maximum line length: 140 characters
- Use `var` when the type is obvious from context

### C# Conventions

- Use records for immutable data (e.g., parameter objects)
- Prefer `init` properties over `set` for parameter records
- Use `readonly` fields where possible
- Add XML documentation for all public APIs
- Use `ConfigureAwait(false)` in library code

### Component Development

- Inherit from `HtmxComponentBase<T>` for components with parameters
- Use `[GenerateParameterMethods]` on parameter records
- Parameter records must be `partial` and inherit from `HtmxComponentParameters`
- Use `HtmxTag` for rendering HTML elements with HTMX attributes

### Testing

- Follow Arrange-Act-Assert pattern
- Use [Shouldly](https://github.com/shouldly/shouldly) for assertions
- Name test methods descriptively: `MethodName_Scenario_ExpectedResult`
- Place test files in the corresponding test project mirroring the source structure

## Project Structure

```
FastComponents/
├── src/
│   ├── FastComponents/              # Core library
│   └── FastComponents.Generators/   # Source generators
├── tests/
│   ├── FastComponents.UnitTests/    # Library tests
│   └── FastComponents.Generators.UnitTests/  # Generator tests
├── demo/
│   └── HtmxAppServer/              # Demo application
└── docs/                            # Generated API docs
```

## Types of Contributions

### Bug Reports

- Use the [Bug Report](https://github.com/Atypical-Consulting/FastComponents/issues/new?template=bug_report.yml) issue template
- Include a minimal reproduction if possible
- Specify your .NET SDK version and OS

### Feature Requests

- Use the [Feature Request](https://github.com/Atypical-Consulting/FastComponents/issues/new?template=feature_request.yml) issue template
- Explain the use case and expected behavior
- Consider how it fits with the existing API

### Pull Requests

- Keep PRs focused on a single change
- Update documentation if adding/changing public APIs
- Add tests for new functionality
- Ensure CI passes before requesting review

## Building and Testing

```bash
# Build everything
dotnet build

# Run all tests
dotnet test

# Run tests with coverage
./coverage.sh

# Run specific test project
dotnet test tests/FastComponents.UnitTests/FastComponents.UnitTests.csproj

# Run a single test
dotnet test --filter "FullyQualifiedName~TestMethodName"

# Pack NuGet package locally
dotnet pack --configuration Release
```

## Need Help?

- Open a [Discussion](https://github.com/Atypical-Consulting/FastComponents/discussions) for questions
- Check existing [Issues](https://github.com/Atypical-Consulting/FastComponents/issues) for known problems
- Review the [documentation](https://github.com/Atypical-Consulting/FastComponents/tree/main/Writerside) for guides

## License

By contributing, you agree that your contributions will be licensed under the [Apache-2.0 License](LICENSE).
