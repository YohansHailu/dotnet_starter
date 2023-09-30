using MediatR;
public class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
{
    public FakeDataStore _dataStore;
    public GetProductsHandler(FakeDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    public async Task<IEnumerable<Product>> Handle(GetProductsQuery getProductsQuery,
        CancellationToken cancellationToken)
    {

        return await _dataStore.GetAllProducts();

    }

}
