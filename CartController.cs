using BookStore.Domain.Abstract;
using BookStore.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.WebUI.Models;

namespace BookStore.WebUI.Controllers
{
    public class CartController : Controller
    {
       private IBookRepository repository;
        private IOrderProcessor orderprocessor;
        public CartController(IBookRepository rep,IOrderProcessor pro)
        {
            repository = rep;
            orderprocessor = pro;
        }
        public RedirectToRouteResult AddToCard(Cart cart, int isbn, string returnUrl)
        {
            Book book=repository.Books.FirstOrDefault(b=>b.ISBN==isbn);
            if (book != null)
            {
                cart.AddItem(book);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToRouteResult RemoveFromCard(Cart cart, int isbn , string returnUrl)
        {
            Book book = repository.Books.FirstOrDefault(b => b.ISBN == isbn);
            if (book != null)
            {
                cart.RemoveLine(book);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        

        // GET: Cart
        public ViewResult Index(Cart cart,string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = cart,ReturnUrl =returnUrl });
        }
        public PartialViewResult summary(Cart cart)
        {
            return PartialView(cart);
        }
        public ViewResult CheckOut()
        {
            return View(new ShippingDetails());
        }
        [HttpPost]
        public ViewResult CheckOut(Cart cart , ShippingDetails shippingdetails)
        {
            if (cart.Lines.Count() == 0)
                ModelState.AddModelError("", "Sorry,your cart is empty");
            if (ModelState.IsValid)
            {
                orderprocessor.ProcessOrder(cart, shippingdetails);
                cart.Clear();
                return View("completed");
            }
            else
            {
                return View(shippingdetails);
            }
        }
    }
}