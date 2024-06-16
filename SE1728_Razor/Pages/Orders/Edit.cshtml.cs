using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SE1728_Razor.Models;

namespace SE1728_Razor.Pages.Orders
{
    public class EditModel : PageModel
    {
        private readonly SE1728_Razor.Models.MyStoreContext _context;

        public EditModel(SE1728_Razor.Models.MyStoreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? id) // Thêm tham số id để nhận OrderId từ query string
        {
            if (id == null)
            {
                return NotFound();
            }

            // Tìm Order theo OrderId
            Order = _context.Orders.FirstOrDefault(o => o.OrderId == id);

            if (Order == null)
            {
                return NotFound();
            }

            ViewData["StaffId"] = new SelectList(_context.Staffs, "StaffId", "StaffId");
            return Page();
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Kiểm tra xem Order có tồn tại trong database không
            var existingOrder = await _context.Orders.FindAsync(Order.OrderId);
            if (existingOrder == null)
            {
                return NotFound();
            }

            // Cập nhật thông tin của existingOrder từ Order được chỉnh sửa trên form
            existingOrder.OrderDate = Order.OrderDate;
            existingOrder.StaffId = Order.StaffId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(Order.OrderId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
