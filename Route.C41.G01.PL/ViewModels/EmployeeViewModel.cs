using Route.C41.G01.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System;

namespace Route.C41.G01.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(5)]
        public string Name { get; set; }

        [Range(22, 30)]
        public int? Age { get; set; }

        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }

        public Gender Gender { get; set; }
        public EmpType EmployeeType { get; set; }



        //Foreign Key Column
        //[ForeignKey("Department")]
        public int? DepartmentId { get; set; }

        /// int? --> Will Mapped to { NoAction Delete } in Database

        // Navigational Property [One]
        //[InverseProperty(nameof(Models.Department.Employees))]
        public Department Department { get; set; }
    }
}
