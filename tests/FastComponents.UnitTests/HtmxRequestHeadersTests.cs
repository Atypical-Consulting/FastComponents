using FastComponents.Http;
using Microsoft.AspNetCore.Http;
using Shouldly;

namespace FastComponents.UnitTests;

public class HtmxRequestHeadersTests
{
    [Fact]
    public void Constructor_WithHttpContext_InitializesHeaders()
    {
        // Arrange
        DefaultHttpContext context = new();
        context.Request.Headers["HX-Request"] = "true";

        // Act
        HtmxRequestHeaders headers = new(context);

        // Assert
        headers.IsHtmxRequest.ShouldBeTrue();
    }

    [Fact]
    public void Constructor_WithHeaderDictionary_InitializesHeaders()
    {
        // Arrange
        HeaderDictionary headerDict = new()
        {
            ["HX-Request"] = "true"
        };

        // Act
        HtmxRequestHeaders headers = new(headerDict);

        // Assert
        headers.IsHtmxRequest.ShouldBeTrue();
    }

    [Fact]
    public void IsBoosted_WhenHeaderPresent_ReturnsTrue()
    {
        // Arrange
        HeaderDictionary headerDict = new()
        {
            ["HX-Boosted"] = "true"
        };
        HtmxRequestHeaders headers = new(headerDict);

        // Act & Assert
        headers.IsBoosted.ShouldBeTrue();
    }

    [Fact]
    public void IsBoosted_WhenHeaderAbsent_ReturnsFalse()
    {
        // Arrange
        HtmxRequestHeaders headers = new(new HeaderDictionary());

        // Act & Assert
        headers.IsBoosted.ShouldBeFalse();
    }

    [Fact]
    public void CurrentUrl_WhenHeaderPresent_ReturnsValue()
    {
        // Arrange
        HeaderDictionary headerDict = new()
        {
            ["HX-Current-URL"] = "https://example.com/page"
        };
        HtmxRequestHeaders headers = new(headerDict);

        // Act & Assert
        headers.CurrentUrl.ShouldBe("https://example.com/page");
    }

    [Fact]
    public void IsHistoryRestoreRequest_WhenTrue_ReturnsTrue()
    {
        // Arrange
        HeaderDictionary headerDict = new()
        {
            ["HX-History-Restore-Request"] = "true"
        };
        HtmxRequestHeaders headers = new(headerDict);

        // Act & Assert
        headers.IsHistoryRestoreRequest.ShouldBeTrue();
    }

    [Fact]
    public void Prompt_WhenHeaderPresent_ReturnsValue()
    {
        // Arrange
        HeaderDictionary headerDict = new()
        {
            ["HX-Prompt"] = "User input text"
        };
        HtmxRequestHeaders headers = new(headerDict);

        // Act & Assert
        headers.Prompt.ShouldBe("User input text");
    }

    [Fact]
    public void IsHtmxRequest_WithVariousCasings_ReturnsTrue()
    {
        // Arrange
        string[] testCases = ["true", "True", "TRUE"];

        foreach (string value in testCases)
        {
            HeaderDictionary headerDict = new()
            {
                ["HX-Request"] = value
            };
            HtmxRequestHeaders headers = new(headerDict);

            // Act & Assert
            headers.IsHtmxRequest.ShouldBeTrue();
        }
    }

    [Fact]
    public void Target_WhenHeaderPresent_ReturnsValue()
    {
        // Arrange
        HeaderDictionary headerDict = new()
        {
            ["HX-Target"] = "my-target-id"
        };
        HtmxRequestHeaders headers = new(headerDict);

        // Act & Assert
        headers.Target.ShouldBe("my-target-id");
    }

    [Fact]
    public void TriggerName_WhenHeaderPresent_ReturnsValue()
    {
        // Arrange
        HeaderDictionary headerDict = new()
        {
            ["HX-Trigger-Name"] = "submit-button"
        };
        HtmxRequestHeaders headers = new(headerDict);

        // Act & Assert
        headers.TriggerName.ShouldBe("submit-button");
    }

    [Fact]
    public void Trigger_WhenHeaderPresent_ReturnsValue()
    {
        // Arrange
        HeaderDictionary headerDict = new()
        {
            ["HX-Trigger"] = "trigger-element-id"
        };
        HtmxRequestHeaders headers = new(headerDict);

        // Act & Assert
        headers.Trigger.ShouldBe("trigger-element-id");
    }

    [Fact]
    public void AllProperties_WhenHeadersAbsent_ReturnNullOrFalse()
    {
        // Arrange
        HtmxRequestHeaders headers = new(new HeaderDictionary());

        // Act & Assert
        headers.IsBoosted.ShouldBeFalse();
        headers.CurrentUrl.ShouldBeNull();
        headers.IsHistoryRestoreRequest.ShouldBeFalse();
        headers.Prompt.ShouldBeNull();
        headers.IsHtmxRequest.ShouldBeFalse();
        headers.Target.ShouldBeNull();
        headers.TriggerName.ShouldBeNull();
        headers.Trigger.ShouldBeNull();
    }

    [Fact]
    public void HeaderNames_Constants_HaveCorrectValues()
    {
        // Assert
        Http.HtmxRequestHeaders.Names.HxBoosted.ShouldBe("HX-Boosted");
        Http.HtmxRequestHeaders.Names.HxCurrentUrl.ShouldBe("HX-Current-URL");
        Http.HtmxRequestHeaders.Names.HxHistoryRestoreRequest.ShouldBe("HX-History-Restore-Request");
        Http.HtmxRequestHeaders.Names.HxPrompt.ShouldBe("HX-Prompt");
        Http.HtmxRequestHeaders.Names.HxRequest.ShouldBe("HX-Request");
        Http.HtmxRequestHeaders.Names.HxTarget.ShouldBe("HX-Target");
        Http.HtmxRequestHeaders.Names.HxTriggerName.ShouldBe("HX-Trigger-Name");
        Http.HtmxRequestHeaders.Names.HxTrigger.ShouldBe("HX-Trigger");
    }
}