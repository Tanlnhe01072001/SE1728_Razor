using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SE1728_Razor.Models;

namespace SE1728_Razor.Pages.Staffs
{
    public class ProfileModel : PageModel
    {
        private readonly SE1728_Razor.Models.MyStoreContext _context;

        public ProfileModel(SE1728_Razor.Models.MyStoreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Staff Staff { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Staff.Password");
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("message", "Nothing changed2");
                
                return Page();
            }

            String id;
            string name;
            int role;
            id = HttpContext.Session.GetString("Id") ?? "";
            name = HttpContext.Session.GetString("Name") ?? "";
            role = HttpContext.Session.GetInt32("Role") ?? 0;
            if (Staff.Name == name && Staff.StaffId == Convert.ToInt32(id)
                && Staff.Role == Staff.Role)
            {
                ModelState.AddModelError("message", "Nothing changed");
                return Page();
            }
            else
            {
                Staff currentStaff = _context.Staffs.Where(x => x.StaffId == Convert.ToInt32(id)).FirstOrDefault() ?? null;
                if (currentStaff != null)
                {

                    currentStaff.Name = Staff.Name;
                    currentStaff.Role = Staff.Role;   
                    _context.SaveChanges();
                    ModelState.AddModelError("message", "Change successfully");
                    currentStaff = _context.Staffs.Where(x => x.StaffId == Convert.ToInt32(id)).FirstOrDefault() ?? null;
                    HttpContext.Session.SetString("Id", currentStaff.StaffId.ToString());
                    HttpContext.Session.SetString("Name", currentStaff.Name);
                    HttpContext.Session.SetInt32("Role", currentStaff.Role);
                    return Page();
                }
                else
                {
                    ModelState.AddModelError("message", "Some error occurs");
                    return Page();
                }

            }

            return RedirectToPage("/Index");
        }
    }
}
