using System.Text.Json.Serialization;

namespace Blog.Models;
public class Comment : BaseEntity
{
    [Required]
    [MinLength(10)]
    public string Content { get; set; }

    public int PostId { get; set; }
    public int UserId { get; set; }

    [JsonIgnore]
    public Post? Post { get; set; }
    [JsonIgnore]
    public User? User { get; set; }

}
