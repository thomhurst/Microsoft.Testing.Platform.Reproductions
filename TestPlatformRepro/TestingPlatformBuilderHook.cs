using Microsoft.Testing.Platform.Builder;
using Microsoft.Testing.Platform.Capabilities.TestFramework;

namespace TestPlatformRepro;

public class TestingPlatformBuilderHook
{
    public static void AddExtensions(
        ITestApplicationBuilder builder,
        string[] _)
    {
        var testFramework = new DummyAdapter();

        builder.RegisterTestFramework(_ => new TestFrameworkCapabilities(), (_, _) => testFramework);
    }
}