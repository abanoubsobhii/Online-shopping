using BookStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        IBookRepository repository;
        public NavController(IBookRepository rep)
        {
            repository = rep;
        }
        public PartialViewResult Menu(string specialization = null)
        {
            ViewBag.selectedspec = specialization;
            IEnumerable<string> spec = repository.Books
                .Select(b=>b.Specialization).Distinct();
            //string viewname = mobilelayout ? "Menuhorizntial": "Menu";
            return PartialView("FlexMenu",spec);
        }
    }
}