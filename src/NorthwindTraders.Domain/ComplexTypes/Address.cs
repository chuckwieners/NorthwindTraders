using System.ComponentModel.DataAnnotations;

namespace NorthwindTraders.Domain.ComplexTypes
{
    public class Address
    {
        [MaxLength(60)]
        public string Street { get; set; }
        [MaxLength(15)]
        public string City { get; set; }
        [MaxLength(15)]
        public string Region { get; set; }
        [MaxLength(10)]
        [Display(Name = "Zip")]
        public string ZipCode { get; set; } 
        //<-- what is the business language? ZipCode or PostalCode
        [MaxLength(15)]
        public string Country { get; set; }
    }
}
