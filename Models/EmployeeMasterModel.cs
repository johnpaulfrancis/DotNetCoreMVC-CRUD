using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCoreMVC_CRUD.Models
{
    public class EmployeeMasterModel
    {
        public int? EmployeeId { get; set; }

        [Required(ErrorMessage = "Please enter employee name")]// NOT NULL in DB and message in view
        [StringLength(50, ErrorMessage = "Employee name must be a string with a minimum length of 3 and a maximum length of 50", MinimumLength = 3)] //Max and Min length (Model)
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Please enter email address")]
        [StringLength(150, ErrorMessage = "Email must be a string with a minimum length of 5 and a maximum length of 150", MinimumLength = 5)]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")] // Email validation
        public string Email { get; set; }

        [Required(ErrorMessage = "Please select a gender")]
        public string Gender { get; set; }

        [DataType(DataType.Date)] // Date type for date of birth
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)] // Display format for date of birth
        public DateTime? DateOfBirth { get; set; } // Nullable DateTime
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
