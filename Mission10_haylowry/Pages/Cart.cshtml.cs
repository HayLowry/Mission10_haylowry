using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mission10_haylowry.Infrastructure;
using Mission10_haylowry.Models;

namespace Mission10_haylowry.Pages
{
    public class CartModel : PageModel
    {
        //Build Instance of Database
        private IBookstoreRepository repo { get; set; }
        public CartModel (IBookstoreRepository temp)
        {
            repo = temp;
        }
        public Cart cart { get; set; }
        public string ReturnUrl { get; set; }
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl;
            cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }

        public IActionResult OnPost(int bookId, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookId);

            cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            cart.AddItem(b, 1);

            HttpContext.Session.SetJson("cart", cart);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
