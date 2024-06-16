using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using SE1728_Razor.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SE1728_Razor.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly MyStoreContext _context;

        public EditModel(MyStoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public SelectList CategorySelectList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            Product = product;
            await LoadCategoriesAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
                     

            _context.Attach(Product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
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
