using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SE1728_Razor.Models;

namespace SE1728_Razor.Pages.Staffs
{
    public class LoginModel : PageModel
    {
        private readonly SE1728_Razor.Models.MyStoreContext _context;

        public LoginModel(SE1728_Razor.Models.MyStoreContext context)
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string name, pass;
            var conf = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            name = conf.GetSection("Admin").GetSection("Name").Value;
            pass = conf.GetSection("Admin").GetSection("Password").Value;
            if(Staff.Name == name && Staff.Password == pass)
            {
                HttpContext.Session.SetString("Name", name);
                HttpContext.Session.SetInt32("Role", 1);
            }    
            else
            {
                Staff staff = _context.Staffs
                    .Where(s => s.Name == Staff.Name && s.Password == Staff.Password)
                    .FirstOrDefault();
                if (staff == null)
                {
                    ModelState.AddModelError("message", "Invalid login information");
                    return Page();

                }
                else
                {
                    HttpContext.Session.SetString("Id", staff.StaffId.ToString());
                    HttpContext.Session.SetString("Name", staff.Name);
                    HttpContext.Session.SetInt32("Role", staff.Role);
                }    

            }  
          
            return RedirectToPage("/Index");
        }
    }
}
