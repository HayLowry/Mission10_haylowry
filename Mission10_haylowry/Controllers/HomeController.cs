using Microsoft.AspNetCore.Mvc;
using Mission10_haylowry.Models;
using Mission10_haylowry.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission10_haylowry.Controllers
{
    public class HomeController : Controller
    {
        private IBookstoreRepository Repo { get; set; }
        public HomeController (IBookstoreRepository temp)
        {
            Repo = temp;
        }
        public IActionResult Index(string bookCategory, int pageNum = 1)
        {
            int pageSize = 5;

            var models = new BooksViewModel
            {
                Books = Repo.Books
                .Where(x => x.Category == bookCategory || bookCategory == null)
                .OrderBy(x => x.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumBooks = (bookCategory == null ? Repo.Books.Count() : Repo.Books.Where(x => x.Category == bookCategory).Count()),
                    BooksPerPage = pageSize,
                    CurrentPage = pageNum
                }

            };
 

            return View(models);
        }

    }
}
