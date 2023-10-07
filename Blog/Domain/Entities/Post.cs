using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blog.Validation;
using Newtonsoft.Json;

namespace Blog.Models;

public class Post : BaseEntity
{


    [MinLength(10)]
    public string Title { get; set; }

    [MinWords(10)]
    public string Body { get; set; }
    public List<Comment>? Comments { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }

}
