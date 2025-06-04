# Changelog

All notable changes to FastComponents will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added
- Enhanced documentation with complete API reference
- Migration guide for upgrading from previous versions
- Performance optimization examples
- Security best practices guide

### Changed
- Improved table of contents structure in documentation

## [1.0.0] - 2024-12-XX

### Added
- **Core Components**
  - `HtmxComponentBase<T>` - Base class for HTMX-enabled components
  - `SimpleHtmxComponent<T>` - Simplified component with automatic routing
  - `HtmxComponentParameters` - Base class for component state management
  - `ClassNamesBuilder` - Fluent API for CSS class name construction

- **HTTP Integration**  
  - `HtmxHttpContextExtensions` - Extension methods for HTMX request/response handling
  - `HtmxRequestHeaders` - Strongly-typed HTMX request headers
  - `HtmxResponseHeaders` - Helper for setting HTMX response headers

- **Endpoint Registration**
  - `HtmxComponentEndpoints` - Extension methods for mapping components to endpoints
  - Support for GET, POST, PUT, PATCH, DELETE HTTP methods
  - Convention-based automatic component discovery and registration

- **Builder APIs**
  - `HtmxBuilder` - Fluent builder for creating HTMX elements programmatically
  - `HtmxBuilderExtensions` - Common patterns and shortcuts
  - `HtmxPatterns` - Static helpers for frequent HTMX patterns

- **Real-time Communication**
  - `HtmxSseTag` - Server-Sent Events support
  - `HtmxWsTag` - WebSocket integration
  - Built-in connection management and error handling

- **Events API**
  - `HtmxEvents` - Constants for all HTMX event names
  - Type-safe event handling
  - Complete coverage of HTMX lifecycle events

- **Source Generators**
  - `GenerateParameterMethodsAttribute` - Automatic fluent method generation
  - `HtmxParametersGenerator` - Source generator implementation
  - `HtmxParametersAnalyzer` - Code analysis for best practices

- **Services**
  - `ComponentHtmlResponseService` - Blazor component rendering to HTML
  - `HtmlBeautifier` - HTML formatting and beautification
  - `MainExtensions` - Service registration helpers
  - `SimplifiedExtensions` - Auto-registration for convention-based setup

- **AOT Support**
  - Full compatibility with .NET Native AOT compilation
  - Appropriate `RequiresUnreferencedCode` and `RequiresDynamicCode` attributes
  - Optimized for trimming and AOT scenarios

- **Testing Support**
  - Unit testing helpers and utilities
  - Integration testing patterns
  - Component rendering test helpers

### Framework Support
- **.NET 9.0** - Primary target framework
- **AngleSharp 1.3.0** - HTML parsing and manipulation
- **HTMX** - Client-side JavaScript library included

### Documentation
- Complete API reference documentation
- Getting started guide
- Component development patterns
- Advanced features documentation
- Performance optimization guide
- Security best practices
- Deployment guidance

## [0.9.0-preview] - 2024-11-XX

### Added
- Initial preview release
- Basic component infrastructure
- HTMX attribute support
- Simple endpoint mapping

### Known Issues
- Limited documentation
- Missing advanced features
- No source generator support

## Project Milestones

### Version 1.0 Goals ✅
- [x] Stable API surface
- [x] Complete HTMX attribute coverage
- [x] Source generator implementation
- [x] AOT compatibility
- [x] Comprehensive documentation
- [x] Real-time communication support
- [x] Performance optimizations

### Future Roadmap

#### Version 1.1 (Planned)
- [ ] Enhanced debugging tools
- [ ] Additional builder patterns
- [ ] Performance monitoring integration
- [ ] Extended validation support
- [ ] Custom authentication helpers

#### Version 1.2 (Planned)
- [ ] Visual Studio tooling
- [ ] Project templates
- [ ] CLI scaffolding tools
- [ ] Enhanced IDE integration
- [ ] Component library ecosystem

#### Version 2.0 (Future)
- [ ] Multi-framework support (.NET 10+)
- [ ] Advanced caching strategies
- [ ] Built-in state management
- [ ] Component marketplace
- [ ] Enterprise features

## Breaking Changes

### 1.0.0
- **Namespace consolidation**: All public APIs moved to `FastComponents` namespace
- **Component base classes**: Renamed for clarity and consistency
  - `HtmxComponent<T>` → `HtmxComponentBase<T>`
  - `SimpleComponent<T>` → `SimpleHtmxComponent<T>`
- **Service registration**: Simplified API with `AddFastComponentsAuto()`
- **Route conventions**: Updated automatic route generation logic

### Migration Guide
See [Migration Guide](Migration-Guide.md) for detailed upgrade instructions.

## Contributors

- **Development Team**: Atypical Consulting SRL
- **Community**: Thanks to all contributors and early adopters

## License

FastComponents is licensed under the Apache License 2.0. See the [LICENSE](../LICENSE) file for details.

## Support

- **Documentation**: [FastComponents Docs](https://docs.fastcomponents.dev)
- **Issues**: [GitHub Issues](https://github.com/atypical-consulting/FastComponents/issues)
- **Discussions**: [GitHub Discussions](https://github.com/atypical-consulting/FastComponents/discussions)
- **NuGet**: [FastComponents Package](https://www.nuget.org/packages/FastComponents/)

---

For older versions and detailed release notes, see the [GitHub Releases](https://github.com/atypical-consulting/FastComponents/releases) page.