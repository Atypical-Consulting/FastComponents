namespace FastComponents;

/// <summary>
/// Htmx attributes (core and additional)
/// </summary>
public interface IHxAttributes
    : IHxCoreAttributes, IHxAdditionalAttributes;
    
    
public interface IHxSseAttributes
    : IHxAttributes
{
    // TODO: this extension will be used by a future component HtmxSseTag
}

public interface IHxWsAttributes
    : IHxAttributes
{
    // TODO: this extension will be used by a future component HtmxWsTag
}