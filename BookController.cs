using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Domain.Abstract;
using BookStore.Domain.Entites;
using BookStore.WebUI.Models;
namespace BookStore.WebUI.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository Repository;
        public int PageSize = 4;
        public BookController(IBookRepository bookrep)
        {
            Repository = bookrep;
        }
        public ViewResult List(string specialization ,int Pageno = 1)
        {
            BookListViewModel model = new BookListViewModel {

                Books = Repository.Books
                .Where(b => specialization == null || b.Specialization == specialization)
                        .OrderBy(b => b.ISBN)
                        .Skip((Pageno - 1) * PageSize)
                        .Take(PageSize),
                paginginfo = new PagingInfo
                { CurrentPage = Pageno,
                    ItemPerPage = PageSize,
                    TotalItems = specialization == null ? Repository.Books.Count() :
                                       Repository.Books.Where(b => b.Specialization == specialization).Count()
                        } ,
                CurrentSpecialilzation = specialization
                      
            };
            
            return View(model);

        }
        
        public ViewResult ListAll()
        {
            return View(Repository.Books);
        }
    }
}