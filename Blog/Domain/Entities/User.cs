namespace Blog.Models;
public class User : BaseEntity
{
    public string Email { get; set; }
    public string Password { get; set; }
    public ICollection<Post>? Posts { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    // make the Dates now
}
