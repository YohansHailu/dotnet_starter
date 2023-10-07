using Blog.Validation;
using System.Text.Json.Serialization;
namespace Blog.Models;
public class Post : BaseEntity
{


    [MinLength(10)]
    public string Title { get; set; }

    [MinWords(10)]
    public string Body { get; set; }

    [JsonIgnore]
    public List<Comment>? Comments { get; set; }
    public int UserId { get; set; }

    [JsonIgnore]
    public User? User { get; set; }

}
