using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blog.Context;
using Blog.Models;
using Newtonsoft.Json.Linq;

namespace Blog.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PostController : ControllerBase
{
    private readonly BlogDbContext _context;
    public PostController(BlogDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
    {
        var posts = await _context.Posts.ToListAsync();
        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPost(int id)
    {
        var post = await _context.Posts.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == id);

        if (post == null)
        {
            return NotFound();
        }

        return Ok(post);
    }

    [HttpPost]
    public async Task<ActionResult<Post>> PostPost(Post post)
    {
        try
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPost(int id, Post updated_post)
    {

        //check if id exists 
        var old_post = await _context.Posts.FindAsync(id);
        if (old_post == null || old_post.Id != id)
        {
            return NotFound();
        }
        try
        {
            old_post.Title = updated_post.Title ?? old_post.Title;
            old_post.Body = updated_post.Body ?? old_post.Body;
            _context.Entry(old_post).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var post = await _context.Posts.FindAsync(id);

        if (post == null)
        {
            return NotFound();
        }

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("{id}/comments")]
    public async Task<ActionResult<IEnumerable<Comment>>> GetComments(int id)
    {
        var post = await _context.Posts.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == id);

        if (post == null)
        {
            return NotFound();
        }

        return Ok(post.Comments);
    }


}
