using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SE1728_Razor.Models;

namespace SE1728_Razor.Pages.Staffs
{
    public class IndexModel : PageModel
    {
        private readonly SE1728_Razor.Models.MyStoreContext _context;

        public IndexModel(SE1728_Razor.Models.MyStoreContext context)
        {
            _context = context;
        }

        public IList<Staff> Staff { get;set; } = default!;
        public string SearchString { get; set; }
        public async Task OnGetAsync(string searchString)
        {
            SearchString = searchString;

            var staffs = from s in _context.Staffs
                         select s;

            if (!string.IsNullOrEmpty(SearchString))
            {
                staffs = staffs.Where(s => s.Name.Contains(SearchString));
            }

            Staff = await staffs.ToListAsync();
        }
    }
}
