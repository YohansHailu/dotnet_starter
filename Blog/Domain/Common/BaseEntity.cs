using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
// create abstract class BaseEntity
namespace Blog.Models
{
    public abstract class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
