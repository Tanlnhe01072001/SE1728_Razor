using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SE1728_Razor.Models;

namespace SE1728_Razor.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly SE1728_Razor.Models.MyStoreContext _context;

        public CreateModel(SE1728_Razor.Models.MyStoreContext context)
        {
            _context = context;
            Order = new Order();
        }

        [BindProperty]
        public Order Order { get; set; } = new Order();
        public SelectList StaffList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadStaffAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadStaffAsync();
                return Page();
            }

            _context.Orders.Add(Order);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private async Task LoadStaffAsync()
        {
            var staff = await _context.Staffs.ToListAsync();
            if (staff == null || !staff.Any())
            {
                ModelState.AddModelError(string.Empty, "No Staff available.");
            }
            StaffList = new SelectList(staff, "StaffId", "Name");
            ViewData["StaffList"] = StaffList;
        }
    }
}
