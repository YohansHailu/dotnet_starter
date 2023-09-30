using MediatR;
public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product>
{
    public FakeDataStore _dataStore;
    public GetProductByIdHandler(FakeDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    public async Task<Product> Handle(GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _dataStore.GetProductById(request.id);

    }

}
