using System.Collections.Generic;
using System.Diagnostics;
using Xunit;

namespace Ben.Demystifier.Test;

public class InvokeTest
{
    [Fact]
    public void InvokeFramesWithoutException()
    {
        var type = GetType();
        var method = type.GetMethod(nameof(CallMeDynamically));
        var frames = method.Invoke(this, null);
        
        Assert.NotNull(frames);
    }

    public List<EnhancedStackFrame> CallMeDynamically()
    {
        var stackTrace = new StackTrace(true);
        var frames = EnhancedStackTrace.GetFrames(stackTrace);
        return frames;
    }
}
