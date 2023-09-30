using MediatR;
public record ProductAddedNotification(Product Product) : INotification;
