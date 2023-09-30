using MediatR;
public class FacheInvalidationHandler : INotificationHandler<ProductAddedNotification>
{
    private FakeDataStore _fakeDataStore;
    public FacheInvalidationHandler(FakeDataStore fakeDataStore)
    {
        _fakeDataStore = fakeDataStore;
    }

    public async Task Handle(ProductAddedNotification notification, CancellationToken cancellationToken)
    {
        // sleep for 1 second
        Task.Delay(10000);
        await _fakeDataStore.EventOccured(notification.Product.Id, "CacheInvalidated");
        await Task.CompletedTask;
    }
}
