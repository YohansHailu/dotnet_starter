using MediatR;
public record AddProductCommand(Product Product) : IRequest<Product>;
