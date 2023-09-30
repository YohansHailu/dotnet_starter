using System.ComponentModel.DataAnnotations;

namespace MVC.Models.option
{
    public class Woption
    {
        [MaxLength(4)]
        public string? City { get; set; }
        public string? State { get; set; }
        public int? Weather { get; set; } = null;
        public string? Summary { get; set; }

    }

}
