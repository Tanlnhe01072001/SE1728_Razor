using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SE1728_Razor.Models;

namespace SE1728_Razor.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly SE1728_Razor.Models.MyStoreContext _context;

        public IndexModel(SE1728_Razor.Models.MyStoreContext context)
        {
            _context = context;
        }
        public IList<Product> Product { get; set; } = default!;
        
        [BindProperty]
        public string Name { get; set; }

        public async Task OnGetAsync()
        {
            Product = await _context.Products.ToListAsync();
        }

        public async Task OnPostAsync()
        {

            Product = await _context.Products
                .Where(c => c.ProductName.Contains(Name ?? ""))
                .ToListAsync();
            
        }
    }
}
