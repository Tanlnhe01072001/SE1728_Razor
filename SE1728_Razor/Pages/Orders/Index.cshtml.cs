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
    public class IndexModel : PageModel
    {
        private readonly SE1728_Razor.Models.MyStoreContext _context;

        public IndexModel(SE1728_Razor.Models.MyStoreContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var order = from s in _context.Orders
                         select s;
            Order = await order.ToListAsync();
        }
    }
}
