using Microsoft.AspNetCore.Mvc;
using Blog.Context;
using Blog.Models;
using MediatR;

namespace Blog.Controllers;
[ApiController]
[Route("/api/comments")]
public class CommentController : ControllerBase
{
    public BlogDbContext _context;
    private readonly IMediator _mediator;

    public CommentController(BlogDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
    {
        var result = await _mediator.Send(new GetComments());
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Comment>> GetComment(int id)
    {
        var result = await _mediator.Send(new GetComments());
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);

    }

    [HttpPost]
    public async Task<ActionResult<Comment>> PostComment(Comment comment)
    {
        try
        {
            await _mediator.Send(new AddComment(comment));
        }
        catch (Exception e)
        {
            return NotFound(e);
        }

        return Ok(CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment));
    }

    //delete 
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        try
        {
            await _mediator.Send(new DeleteComment(id));
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }

        return NoContent();
    }

}
