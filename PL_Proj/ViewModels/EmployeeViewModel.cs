using DAL_Proj.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace PL_Proj.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [
         Required,
         MaxLength(50, ErrorMessage = "Max Length Is 50 Chars"),
         MinLength(5, ErrorMessage = "Min Length Is 5 Chars")
        ]
        public string Name { get; set; }
        [Range(18, 50, ErrorMessage = "Age Must Be In Range From 18 to 50")]
        public int? Age { get; set; }
        //[RegularExpression("^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4.10}-[a-zA-Z]{5,10}$",
        //    ErrorMessage = "Address Must Be Like 123-Street-City-Country")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public int Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public string ImageName { get; set; }
        public IFormFile Image { get; set; }
        [ForeignKey("Department")]
        public int? DeptId { get; set; }
        [InverseProperty("Employees")]
        public Department Department { get; set; }
    }
}
