using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entites
{
    public class ShippingDetails
    {
        [Required(ErrorMessage ="Please Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter FirstLine")]
        [Display(Name = "Adress line 1")]
        public string Line1 { get; set; }
        [Display(Name = "Adress line 2")]
        public string Line2 { get; set; }
        [Required(ErrorMessage = "Please Enter city")]
        public string City { get; set; }
        public string State { get; set; }
        [Required(ErrorMessage = "Please Enter Country")]
        public string Country { get; set; }
        public bool GiftWrap { get; set; }
    }
}
