; Shipped analyzer releases
; https://github.com/dotnet/roslyn-analyzers/blob/main/src/Microsoft.CodeAnalysis.Analyzers/ReleaseTrackingAnalyzers.Help.md

## Release 1.0

### New Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|---------
FC0001 | FastComponents.Usage | Warning | Record inheriting from HtmxComponentParameters should use [GenerateParameterMethods] attribute
FC0002 | FastComponents.Usage | Error | Record with [GenerateParameterMethods] must be partial
FC0003 | FastComponents.Usage | Error | Record with [GenerateParameterMethods] must inherit from HtmxComponentParameters
FC0004 | FastComponents.Usage | Info | Properties in record with [GenerateParameterMethods] should be init-only
FC0005 | FastComponents.Usage | Warning | Record with [GenerateParameterMethods] has manual implementation of generated methods