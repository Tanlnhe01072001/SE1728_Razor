using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SE1728_Razor.Models;


namespace SE1728_Razor.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly SE1728_Razor.Models.MyStoreContext _context;

        public DetailsModel(SE1728_Razor.Models.MyStoreContext context)
        {
            _context = context;
        }

      public Order Order { get; set; } = default!;
        public Staff Staff { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            Order = await _context.Orders
                 .Include(o => o.Staff) // Ensure Staff is loaded
                 .FirstOrDefaultAsync(m => m.OrderId == id);

            if (Order == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
