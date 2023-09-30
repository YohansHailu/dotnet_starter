using MediatR;
public record GetProductsQuery() : IRequest<IEnumerable<Product>>;
