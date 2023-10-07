using System.Text.Json.Serialization;
namespace Blog.Models;
public class User : BaseEntity
{
    public string Email { get; set; }
    public string Password { get; set; }

    [JsonIgnore]
    public ICollection<Post>? Posts { get; set; }

    [JsonIgnore]
    public ICollection<Comment>? Comments { get; set; }
    // make the Dates now
}
