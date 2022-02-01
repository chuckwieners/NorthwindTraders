using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NorthwindTraders.Domain.ComplexTypes;

namespace NorthwindTraders.Domain.Entities
{
    public class Employee : Entity<int>
    {
        public Employee()
        {
            Subordinates = new HashSet<Employee>();
            //Orders = new HashSet<Order>();
            //Territories = new HashSet<Territory>();
        }

        [Required]
        [MaxLength(10)]
        [Display(Name = "First Name")]
        public string LastName { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name = "Last Name")]
        public string FirstName { get; set; }
        [MaxLength(30)]
        public string Title { get; set; }
        [MaxLength(25)]
        [Display(Name = "Title of Courtesy")]
        public string TitleOfCourtesy { get; set; }
        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = false)]
        public DateTime? BirthDate { get; set; }
        [Display(Name = "Hire Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = false)]
        public DateTime? HireDate { get; set; }
        public Address Address { get; set; }
        [MaxLength(24)]
        [Display(Name = "Home Phone")]
        public string HomePhone { get; set; }
        [MaxLength(4)]
        public string Extension { get; set; }
        public byte[] Photo { get; set; }
        public string Notes { get; set; }
        public int? SupervisorId { get; set; }
        [MaxLength(255)]
        [Display(Name = "Image Path")]
        public string PhotoPath { get; set; }

        [Display(Name = "Name")]
        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

        public virtual Employee Supervisor { get; set; }

        public virtual ICollection<Employee> Subordinates { get; set; }
        //TODO: uncomment these out when the other entities have been added and mapped
        //public virtual ICollection<Order> Orders { get; set; }
        //public virtual ICollection<Territory> Territories { get; set; }
    }
}