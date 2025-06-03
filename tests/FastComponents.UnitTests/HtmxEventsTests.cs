using Shouldly;

namespace FastComponents.UnitTests;

public class HtmxEventsTests
{
    [Fact]
    public void EventConstants_HaveCorrectValues()
    {
        // Core lifecycle events
        Events.HtmxEvents.Abort.ShouldBe("htmx:abort");
        Events.HtmxEvents.AfterOnLoad.ShouldBe("htmx:afterOnLoad");
        Events.HtmxEvents.AfterProcessNode.ShouldBe("htmx:afterProcessNode");
        Events.HtmxEvents.AfterRequest.ShouldBe("htmx:afterRequest");
        Events.HtmxEvents.AfterSettle.ShouldBe("htmx:afterSettle");
        Events.HtmxEvents.AfterSwap.ShouldBe("htmx:afterSwap");
        Events.HtmxEvents.BeforeCleanupElement.ShouldBe("htmx:beforeCleanupElement");
        Events.HtmxEvents.BeforeOnLoad.ShouldBe("htmx:beforeOnLoad");
        Events.HtmxEvents.BeforeProcessNode.ShouldBe("htmx:beforeProcessNode");
        Events.HtmxEvents.BeforeRequest.ShouldBe("htmx:beforeRequest");
        Events.HtmxEvents.BeforeSwap.ShouldBe("htmx:beforeSwap");
        Events.HtmxEvents.BeforeTransition.ShouldBe("htmx:beforeTransition");
        
        // User interaction events
        Events.HtmxEvents.Cancel.ShouldBe("htmx:cancel");
        Events.HtmxEvents.ConfigRequest.ShouldBe("htmx:configRequest");
        Events.HtmxEvents.Confirm.ShouldBe("htmx:confirm");
        Events.HtmxEvents.Prompt.ShouldBe("htmx:prompt");
        
        // History events
        Events.HtmxEvents.HistoryCacheMiss.ShouldBe("htmx:historyCacheMiss");
        Events.HtmxEvents.HistoryCacheMissError.ShouldBe("htmx:historyCacheMissError");
        Events.HtmxEvents.HistoryCacheMissLoad.ShouldBe("htmx:historyCacheMissLoad");
        Events.HtmxEvents.HistoryRestore.ShouldBe("htmx:historyRestore");
        Events.HtmxEvents.BeforeHistorySave.ShouldBe("htmx:beforeHistorySave");
        Events.HtmxEvents.PushedIntoHistory.ShouldBe("htmx:pushedIntoHistory");
        Events.HtmxEvents.ReplacedInHistory.ShouldBe("htmx:replacedInHistory");
        
        // Load event
        Events.HtmxEvents.Load.ShouldBe("htmx:load");
        
        // Out of band events
        Events.HtmxEvents.NoSseSourceError.ShouldBe("htmx:noSSESourceError");
        Events.HtmxEvents.OobAfterSwap.ShouldBe("htmx:oobAfterSwap");
        Events.HtmxEvents.OobBeforeSwap.ShouldBe("htmx:oobBeforeSwap");
        Events.HtmxEvents.OobErrorNoTarget.ShouldBe("htmx:oobErrorNoTarget");
        
        // Error events
        Events.HtmxEvents.ResponseError.ShouldBe("htmx:responseError");
        Events.HtmxEvents.SendError.ShouldBe("htmx:sendError");
        Events.HtmxEvents.SwapError.ShouldBe("htmx:swapError");
        Events.HtmxEvents.TargetError.ShouldBe("htmx:targetError");
        Events.HtmxEvents.Timeout.ShouldBe("htmx:timeout");
        
        // SSE events
        Events.HtmxEvents.SseError.ShouldBe("htmx:sseError");
        Events.HtmxEvents.SseMessage.ShouldBe("htmx:sseMessage");
        Events.HtmxEvents.SseOpen.ShouldBe("htmx:sseOpen");
        
        // Validation events
        Events.HtmxEvents.ValidationValidate.ShouldBe("htmx:validation:validate");
        Events.HtmxEvents.ValidationFailed.ShouldBe("htmx:validation:failed");
        Events.HtmxEvents.ValidationHalted.ShouldBe("htmx:validation:halted");
        
        // XHR events
        Events.HtmxEvents.XhrAbort.ShouldBe("htmx:xhr:abort");
        Events.HtmxEvents.XhrLoadEnd.ShouldBe("htmx:xhr:loadend");
        Events.HtmxEvents.XhrLoadStart.ShouldBe("htmx:xhr:loadstart");
        Events.HtmxEvents.XhrProgress.ShouldBe("htmx:xhr:progress");
    }
    
    [Fact]
    public void EventConstants_AreUnique()
    {
        // Get all event constants via reflection
        var eventType = typeof(Events.HtmxEvents);
        var fields = eventType.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.FlattenHierarchy);
        
        var eventValues = fields
            .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string))
            .Select(f => f.GetValue(null) as string)
            .ToList();
        
        // Check for duplicates
        var duplicates = eventValues.GroupBy(x => x)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();
        
        duplicates.ShouldBeEmpty();
        eventValues.Count.ShouldBeGreaterThan(40); // We have many events defined
    }
}