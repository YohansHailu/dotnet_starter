using MediatR;
public class EmailHandler : INotificationHandler<ProductAddedNotification>
{
    private FakeDataStore _fakeDataStore;
    public EmailHandler(FakeDataStore fakeDataStore)
    {
        _fakeDataStore = fakeDataStore;
    }


    public async Task Handle(ProductAddedNotification notification, CancellationToken cancellationToken)
    {
        await _fakeDataStore.EventOccured(notification.Product.Id, "Email Sent");
        await Task.CompletedTask;
    }
}
