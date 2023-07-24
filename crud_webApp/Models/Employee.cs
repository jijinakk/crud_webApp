using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace crud_webApp.Models
{
    public class Employee
    {

        [Key]
        public int employeeID { get; set; }
        [Required]
        [DisplayName("EmployeeName")]
        public string employeeName { get; set; }
        [Required]
        [DisplayName("Date of birth")]

        public DateTime dateOfBirth { get; set; }
        [Required]
        [DisplayName("Phone number")]

        public string phoneNumber { get; set; }
        [Required]
        [DisplayName("Email")]

        public string email { get; set; }


    }
}