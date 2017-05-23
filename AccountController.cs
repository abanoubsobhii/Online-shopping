using BookStore.WebUI.Infrastructre.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.WebUI.Models;

namespace BookStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthProvider authprovider;
        public AccountController(IAuthProvider autpro)
        {
            authprovider = autpro;
        }
        public ViewResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel loginviewmodel , string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (authprovider.Authenticate(loginviewmodel.Username, loginviewmodel.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                ModelState.AddModelError("", "Incorrect Username/Password");
                return View();
            }
            else
            return View();
        }
    }
}