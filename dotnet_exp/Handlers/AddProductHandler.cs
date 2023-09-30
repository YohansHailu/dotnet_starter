using MediatR;
public class AddProductHandler : IRequestHandler<AddProductCommand, Product>
{
    public FakeDataStore _fakeDataStore;
    public AddProductHandler(FakeDataStore fakeDataStore)
    {
        _fakeDataStore = fakeDataStore;
    }

    public async Task<Product> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        await _fakeDataStore.addProduct(request.Product);

        return request.Product;
    }
}
