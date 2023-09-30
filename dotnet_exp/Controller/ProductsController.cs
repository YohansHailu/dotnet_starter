using Microsoft.AspNetCore.Mvc;
using MediatR;
[Route("api/products")]
[ApiController]
public class ProductsController : Controller
{
    public IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}")]
    public IResult getbyId([FromRoute] int id)
    {
        try
        {
            var result = _mediator.Send(new GetProductByIdQuery(id));
            return Results.Ok(result);
        }
        catch (Exception)
        {
            return Results.NotFound();
        }
    }

    [HttpGet("id")]
    public IResult FromRoute([FromQuery] int id)
    {
        try
        {
            var result = _mediator.Send(new GetProductByIdQuery(id));
            return Results.Ok(result);
        }
        catch (Exception)
        {
            return Results.NotFound();
        }
    }

    [HttpGet("all")]
    public IResult getAll()
    {
        var result = _mediator.Send(new GetProductsQuery());
        return Results.Ok(result);
    }

    [HttpPost("add")]
    public async Task<IResult> postProduct([FromBody] Product product)
    {
        var productToReturn = await _mediator.Send(new AddProductCommand(product));

        await _mediator.Publish(new ProductAddedNotification(productToReturn));

        return await Task.FromResult(getbyId(productToReturn.Id));
    }
}
