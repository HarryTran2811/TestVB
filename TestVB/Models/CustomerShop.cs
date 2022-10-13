using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestVB.Models
{
    public class CustomerShop
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }

        [Display(Name = "Customer Name")]
        [Required(ErrorMessage = "This field is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 chars")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Address")]
        [Required(ErrorMessage = "This field is required")]
        public string Address { get; set; }

        public virtual Customer Customer { get; set; }
    }
}