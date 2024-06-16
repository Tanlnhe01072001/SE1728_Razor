using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SE1728_Razor.Models;

namespace SE1728_Razor.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly SE1728_Razor.Models.MyStoreContext _context;

        public CreateModel(SE1728_Razor.Models.MyStoreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["StaffId"] = new SelectList(_context.Staffs, "StaffId", "StaffId");
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Categories.Add(Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
