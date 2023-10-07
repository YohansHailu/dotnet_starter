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
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Comment>> GetComment(int id)
    {
        var comment = await _context.Comments.Include(c => c.Post).FirstOrDefaultAsync(c => c.Id == id);

        if (comment == null)
        {
            return NotFound();
        }
        // return post object of comment
        return comment;

    }

    [HttpPost]
    public async Task<ActionResult<Comment>> PostComment(Comment comment)
    {
        var post = await _context.Posts.FindAsync(comment.PostId);
        if (post == null)
        {
            return NotFound("post is not found");
        }
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
    }

    //delete 
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            return NotFound();
        }

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}
