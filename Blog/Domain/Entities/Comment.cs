using Blog.Validation;

namespace Blog.Models;
public class Comment : BaseEntity
{
    //should me more than 10 words
    //count the words sperated by space and should be more than ten
    [Required]
    [MinLength(10)]
    [Abusive("https://api.tisane.ai/parse", "bd757276953f412681eff13086ec3d2f")]
    public string Content { get; set; }

    public int PostId { get; set; }

    public Post? Post { get; set; }

    public int UserId { get; set; }

    public User? User { get; set; }

}
