using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Shouldly;

namespace FastComponents.UnitTests;

public class HtmxRequestHeadersTests
{
    [Fact]
    public void Constructor_WithHttpContext_InitializesHeaders()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Request.Headers["HX-Request"] = "true";
        
        // Act
        var headers = new Http.HtmxRequestHeaders(context);
        
        // Assert
        headers.IsHtmxRequest.ShouldBeTrue();
    }
    
    [Fact]
    public void Constructor_WithHeaderDictionary_InitializesHeaders()
    {
        // Arrange
        var headerDict = new HeaderDictionary
        {
            ["HX-Request"] = "true"
        };
        
        // Act
        var headers = new Http.HtmxRequestHeaders(headerDict);
        
        // Assert
        headers.IsHtmxRequest.ShouldBeTrue();
    }
    
    [Fact]
    public void IsBoosted_WhenHeaderPresent_ReturnsTrue()
    {
        // Arrange
        var headerDict = new HeaderDictionary
        {
            ["HX-Boosted"] = "true"
        };
        var headers = new Http.HtmxRequestHeaders(headerDict);
        
        // Act & Assert
        headers.IsBoosted.ShouldBeTrue();
    }
    
    [Fact]
    public void IsBoosted_WhenHeaderAbsent_ReturnsFalse()
    {
        // Arrange
        var headers = new Http.HtmxRequestHeaders(new HeaderDictionary());
        
        // Act & Assert
        headers.IsBoosted.ShouldBeFalse();
    }
    
    [Fact]
    public void CurrentUrl_WhenHeaderPresent_ReturnsValue()
    {
        // Arrange
        var headerDict = new HeaderDictionary
        {
            ["HX-Current-URL"] = "https://example.com/page"
        };
        var headers = new Http.HtmxRequestHeaders(headerDict);
        
        // Act & Assert
        headers.CurrentUrl.ShouldBe("https://example.com/page");
    }
    
    [Fact]
    public void IsHistoryRestoreRequest_WhenTrue_ReturnsTrue()
    {
        // Arrange
        var headerDict = new HeaderDictionary
        {
            ["HX-History-Restore-Request"] = "true"
        };
        var headers = new Http.HtmxRequestHeaders(headerDict);
        
        // Act & Assert
        headers.IsHistoryRestoreRequest.ShouldBeTrue();
    }
    
    [Fact]
    public void Prompt_WhenHeaderPresent_ReturnsValue()
    {
        // Arrange
        var headerDict = new HeaderDictionary
        {
            ["HX-Prompt"] = "User input text"
        };
        var headers = new Http.HtmxRequestHeaders(headerDict);
        
        // Act & Assert
        headers.Prompt.ShouldBe("User input text");
    }
    
    [Fact]
    public void IsHtmxRequest_WithVariousCasings_ReturnsTrue()
    {
        // Arrange
        var testCases = new[] { "true", "True", "TRUE" };
        
        foreach (var value in testCases)
        {
            var headerDict = new HeaderDictionary
            {
                ["HX-Request"] = value
            };
            var headers = new Http.HtmxRequestHeaders(headerDict);
            
            // Act & Assert
            headers.IsHtmxRequest.ShouldBeTrue();
        }
    }
    
    [Fact]
    public void Target_WhenHeaderPresent_ReturnsValue()
    {
        // Arrange
        var headerDict = new HeaderDictionary
        {
            ["HX-Target"] = "my-target-id"
        };
        var headers = new Http.HtmxRequestHeaders(headerDict);
        
        // Act & Assert
        headers.Target.ShouldBe("my-target-id");
    }
    
    [Fact]
    public void TriggerName_WhenHeaderPresent_ReturnsValue()
    {
        // Arrange
        var headerDict = new HeaderDictionary
        {
            ["HX-Trigger-Name"] = "submit-button"
        };
        var headers = new Http.HtmxRequestHeaders(headerDict);
        
        // Act & Assert
        headers.TriggerName.ShouldBe("submit-button");
    }
    
    [Fact]
    public void Trigger_WhenHeaderPresent_ReturnsValue()
    {
        // Arrange
        var headerDict = new HeaderDictionary
        {
            ["HX-Trigger"] = "trigger-element-id"
        };
        var headers = new Http.HtmxRequestHeaders(headerDict);
        
        // Act & Assert
        headers.Trigger.ShouldBe("trigger-element-id");
    }
    
    [Fact]
    public void AllProperties_WhenHeadersAbsent_ReturnNullOrFalse()
    {
        // Arrange
        var headers = new Http.HtmxRequestHeaders(new HeaderDictionary());
        
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