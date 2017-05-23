﻿using BookStore.Domain.Abstract;
using BookStore.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        private IBookRepository repository;
        public AdminController(IBookRepository rep)
        {
            repository = rep;
        }
        public ActionResult Index()
        {
            return View(repository.Books);
        }
        public ViewResult Edit(int ISBN)
        {
            Book book = repository.Books.FirstOrDefault(b => b.ISBN == ISBN);
            return View(book);
        }
        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
               repository.SaveBook(book);
               TempData["message"] = book.Title+"has been saved";
               return RedirectToAction("Index");
            }
            else
            {
                //not complete
                return View(book);
            }

        }
        public ViewResult Create()
        {
            return View("Edit", new Book());
        }
        [HttpGet]
        public ActionResult Delete(int isbn)
        {
            Book deletedBook = repository.DeleteBook(isbn);
            if (deletedBook != null)
            {
                TempData["message"] = deletedBook.Title + " has been deleted";
            }
            return RedirectToAction("Index");

        }
    }
}