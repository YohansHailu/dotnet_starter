using Newtonsoft.Json;
namespace Blog.Models;
public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    [JsonIgnore]
    public ICollection<Post>? Posts { get; set; }


    public ICollection<Comment>? Comments { get; set; }


    // make the Dates now
    public User()
    {
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }
}
