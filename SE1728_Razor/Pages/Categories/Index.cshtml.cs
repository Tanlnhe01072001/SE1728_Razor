using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SE1728_Razor.Models;

namespace SE1728_Razor.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly SE1728_Razor.Models.MyStoreContext _context;

        public IndexModel(SE1728_Razor.Models.MyStoreContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;
        
        [BindProperty]
        public string Name { get; set; }

        public async Task OnGetAsync()
        {
            Category = await _context.Categories.ToListAsync();
        }

        public async Task OnPostAsync()
        {
            
            Category = await _context.Categories
                .Where(c => c.CategoryName.Contains(Name??""))                
                .ToListAsync();
        }

    }
}
