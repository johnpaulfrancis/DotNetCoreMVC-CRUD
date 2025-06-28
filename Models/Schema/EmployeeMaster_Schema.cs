using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCoreMVC_CRUD.Models.Schema
{
    public class EmployeeMaster_Schema
    {
        [Key] //Primary Key for the Employee table
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incrementing primary key
        public int EmployeeId { get; set; }

        [Required] // NOT NULL in DB
        [MaxLength(50)] // Maximum length for EmployeeName in DB
        public string EmployeeName { get; set; }

        [Required]
        [MaxLength(150)]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        public string Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
