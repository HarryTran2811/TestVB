using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace TestVB.Models
{
    [DebuggerDisplay("({Id}) {Name}")]
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "This field is required")]
        public string ProfilePictureURL { get; set; }

        [Display(Name = "Customer Name")]
        [Required(ErrorMessage = "This field is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 chars")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "This field is required")]
        public string EmailAddress { get; set; }
        public ICollection<CustomerShop> Shops { get; set; }


    }
}