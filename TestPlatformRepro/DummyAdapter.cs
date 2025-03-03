using Microsoft.Testing.Platform.Extensions.Messages;
using Microsoft.Testing.Platform.Extensions.TestFramework;
using Microsoft.Testing.Platform.TestHost;

internal class DummyAdapter : ITestFramework, IDataProducer
{
    public string Uid => nameof(DummyAdapter);

    public string Version => string.Empty;

    public string DisplayName => string.Empty;

    public string Description => string.Empty;

    public Type[] DataTypesProduced => [typeof(TestNodeUpdateMessage)];

    public Task<CloseTestSessionResult> CloseTestSessionAsync(CloseTestSessionContext context) => Task.FromResult(new CloseTestSessionResult { IsSuccess = true });

    public Task<CreateTestSessionResult> CreateTestSessionAsync(CreateTestSessionContext context) => Task.FromResult(new CreateTestSessionResult { IsSuccess = true });

    public async Task ExecuteRequestAsync(ExecuteRequestContext context)
    {
        for (int i = 0; i < 15; i++)
        {
            await context.MessageBus.PublishAsync(this, new TestNodeUpdateMessage(new SessionUid("1"), new TestNode
            {
                Uid = $"{i}",
                DisplayName = $"Test {i}",
                Properties = new PropertyBag(InProgressTestNodeStateProperty.CachedInstance)
            }));
        }

        await Task.Delay(TimeSpan.FromSeconds(30));
     
        context.Complete();
    }

    public Task<bool> IsEnabledAsync() => Task.FromResult(true);
}