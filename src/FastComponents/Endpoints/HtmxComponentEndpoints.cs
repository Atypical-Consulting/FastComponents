// Copyright (c) Atypical Consulting SRL. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace FastComponents;

public abstract class HtmxComponentEndpoint<TComponent>
    : EndpointWithoutRequest<string>, IHtmxComponentEndpoint
    where TComponent : HtmxComponentBase
{
    public ComponentHtmlResponseService ComponentHtmlResponseService { get; set; } = null!;

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendHtmlResultAsync();
    }

    private async Task SendHtmlResultAsync()
    {
        IResult response = await ComponentHtmlResponseService.RenderAsHtmlContent<TComponent>();
        await response.ExecuteAsync(HttpContext);
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
        Dictionary<string, object?> parameters = new() { [nameof(HtmxComponentBase<>.Parameters)] = req };
        await SendHtmlResultAsync(parameters);
    }

    protected async Task SendHtmlResultAsync(Dictionary<string, object?>? parameters = null)
    {
        IResult response = await ComponentHtmlResponseService.RenderAsHtmlContent<TComponent>(parameters);
        await response.ExecuteAsync(HttpContext);
    }
}

file interface IHtmxComponentEndpoint
{
    /// <summary>
    /// This service is injected by the FastComponents builder
    /// </summary>
    ComponentHtmlResponseService ComponentHtmlResponseService { get; set; }
}
