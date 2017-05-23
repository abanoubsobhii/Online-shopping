using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BookStore.Domain.Entites
{
    public class Book
    {
        [Key]
        public int ISBN { get; set; }
       [Required(ErrorMessage = "Enter Book Title ")]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Enter Book Description ")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Enter Book Price")]
        public decimal price { get; set; }
        [Required(ErrorMessage = "Enter Book Specialization")]
        public string Specialization { get; set; }
    }
}
