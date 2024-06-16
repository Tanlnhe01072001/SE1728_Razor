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
    public class LogoutModel : PageModel
    {
        
        public LogoutModel()
        {
           
        }

        public IActionResult OnGet()
        {
            Console.WriteLine("test");
            HttpContext.Session.Remove("Name");
            HttpContext.Session.Remove("Role");
            return RedirectToPage("/Index");
        }

       
    }
}
