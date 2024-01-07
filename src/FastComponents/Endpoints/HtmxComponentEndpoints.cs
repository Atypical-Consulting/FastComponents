using FastEndpoints;

namespace FastComponents;

public abstract class HtmxComponentEndpoint<TComponent>
    : EndpointWithoutRequest<string>, IHtmxComponentEndpoint
    where TComponent : HtmxComponentBase
{
    public ComponentHtmlResponseService ComponentHtmlResponseService { get; set; } = null!;
    
    public override async Task HandleAsync(CancellationToken ct)
        => await SendHtmlResultAsync();

    private async Task SendHtmlResultAsync()
    {
        var response = await ComponentHtmlResponseService.RenderAsHtmlContent<TComponent>();
        await SendResultAsync(response);
    }
}

public abstract class HtmxComponentEndpoint<TComponent, TParameters>
    : Endpoint<TParameters, string>, IHtmxComponentEndpoint
    where TComponent : HtmxComponentBase<TParameters>
    where TParameters : class, new()
{
    public ComponentHtmlResponseService ComponentHtmlResponseService { get; set; } = null!;
    
    public override async Task HandleAsync(TParameters req, CancellationToken ct)
    {
        Dictionary<string, object?> parameters = new() { [nameof(HtmxComponentBase<TParameters>.Parameters)] = req };
        await SendHtmlResultAsync(parameters);
    }
    
    protected async Task SendHtmlResultAsync(Dictionary<string, object?>? parameters = null)
    {
        var response = await ComponentHtmlResponseService.RenderAsHtmlContent<TComponent>(parameters);
        await SendResultAsync(response);
    }
}

file interface IHtmxComponentEndpoint
{
    /// <summary>
    /// This service is injected by the FastComponents builder
    /// </summary>
    ComponentHtmlResponseService ComponentHtmlResponseService { get; set; }
}