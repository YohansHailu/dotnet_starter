using MediatR;
public record GetProductByIdQuery(int id) : IRequest<Product>;
