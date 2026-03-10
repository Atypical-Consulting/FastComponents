using System.Text.Json.Serialization;
using HtmxAppServer.Components.Blocks;
using HtmxAppServer.Components;

namespace HtmxAppServer;

[JsonSerializable(typeof(FormValidationParameters))]
[JsonSerializable(typeof(LiveSearchParameters))]
[JsonSerializable(typeof(ModalParameters))]
[JsonSerializable(typeof(TabsParameters))]
[JsonSerializable(typeof(InfiniteScrollParameters))]
[JsonSerializable(typeof(EditInPlaceParameters))]
[JsonSerializable(typeof(DebugDashboardParameters))]
[JsonSerializable(typeof(CounterParameters))]
[JsonSerializable(typeof(AppParameters))]
[JsonSerializable(typeof(MovieCharactersRowsParameters))]
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    WriteIndented = false,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
public partial class AppJsonSerializerContext : JsonSerializerContext;
