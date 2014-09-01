using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NorthWindUpdate.Models
{
    [MetadataType(typeof(EmployeeMetaData))]
    public partial class Employee
    {

    }
    public class EmployeeMetaData
    {
        [ScaffoldColumn(false)]
        public int EmployeeID { get; set; }
        [Required]
        [MaxLength(20)]
        public String LastName { get; set; }
        [Required]
        [MaxLength(10)]
        public String FirstName { get; set; }
        [DisplayName("Company Title")]
        [MaxLength(30)]
        public String Title { get; set; }
        [DisplayName("Title of Courtesy")]
        [MaxLength(25)]
        public String TitleOfCourtesy { get; set; }
        [DisplayName("Birth Date")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        [DisplayName("Hire Date")]
        [DataType(DataType.Date)]
        public DateTime? HireDate { get; set; }
        [MaxLength(60)]
        public String Address { get; set; }
        [MaxLength(15)]
        public String City { get; set; }
        [MaxLength(15)]
        public String Region { get; set; }
        [DisplayName("Postal Code")]
        [MaxLength(10)]
        public String PostalCode { get; set; }
        [MaxLength(15)]
        public String Country { get; set; }
        [DisplayName("Home Phone")]
        [MaxLength(24)]
        public String HomePhone { get; set; }
        [MaxLength(4)]
        public String Extension { get; set; }
        public Byte[] Photo { get; set; }
        [DataType(DataType.MultilineText)]
        public String Notes { get; set; }
        [DisplayName("Report To")]
        public int? ReportsTo { get; set; }
        [DisplayName("Photo Path")]
        [MaxLength(255)]
        public String PhotoPath { get; set; }

  }
}