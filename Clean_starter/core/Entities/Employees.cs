using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 EmployeeId
        {
            get;
            set;
        }
        
        [Required]
        public string FirstName
        {
            get;
            set;
        }

        [Required]
        public string LastName
        {
            get;
            set;
        }

        [Required]
        public DateTime DateOfBirth
        {
            get;
            set;
        }

        [Phone]
        [Required]
        public string PhoneNumber
        {
            get;
            set;
        }

        [Required]
        [EmailAddress]
        public string Email
        {
            get;
            set;
        }
    }
}
