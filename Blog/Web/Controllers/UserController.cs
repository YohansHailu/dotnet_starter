using Blog.Context;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    public BlogDbContext _context;
    public UserController(BlogDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var posts = await _context.User.ToListAsync();

        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        var post = await _context.User.FirstOrDefaultAsync(u => u.Id == id);


        if (post == null)
        {
            var users = await _context.User.ToListAsync();
            return NotFound();
        }
        return Ok(post);
    }

    [HttpGet("{id}/posts")]
    public async Task<ActionResult<IEnumerable<Post>>> GetUsersIncludingPosts(int id)
    {
        var userIncludingPosts = await _context.User
          .Include(u => u.Posts)
          .FirstOrDefaultAsync(u => u.Id == id);

        if (userIncludingPosts == null)
        {
            return NotFound();
        }

        return Ok(userIncludingPosts);
    }

    [HttpGet("{id}/comments")]
    public async Task<ActionResult<IEnumerable<Comment>>> GetUsersIncludingComments(int id)
    {
        var userIncludingComments = await _context.User
          .Include(u => u.Comments)
          .FirstOrDefaultAsync(u => u.Id == id);

        if (userIncludingComments == null)
        {
            return NotFound();
        }

        return Ok(userIncludingComments);
    }


    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User post)
    {
        _context.User.Add(post);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUserById), new { id = post.Id }, post);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, User post)
    {
        if (id != post.Id)
        {
            return NotFound();
        }
        _context.Entry(post).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var post = await _context.User.FindAsync(id);
        if (post == null)
        {
            return NotFound();
        }
        _context.User.Remove(post);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
