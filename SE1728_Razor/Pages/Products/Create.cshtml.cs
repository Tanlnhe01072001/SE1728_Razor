using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SE1728_Razor.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SE1728_Razor.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly MyStoreContext _context;

        public CreateModel(MyStoreContext context)
        {
            _context = context;
            Product = new Product();
        }

        [BindProperty]
        public Product Product { get; set; }
        public SelectList CategorySelectList { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadCategoriesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {                    

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private async Task LoadCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            if (categories == null || !categories.Any())
            {
                ModelState.AddModelError(string.Empty, "No categories available.");
            }
            CategorySelectList = new SelectList(categories, "CategoryId", "CategoryName");
        }
    }
}
