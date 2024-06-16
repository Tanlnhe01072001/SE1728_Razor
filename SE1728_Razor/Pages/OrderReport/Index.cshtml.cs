using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SE1728_Razor.Models;

namespace SE1728_Razor.Pages.OrderReport
{
    public class IndexModel : PageModel
    {
        private readonly MyStoreContext _context;
        public List<Models.OrderReport> OrderReports = new List<Models.OrderReport>();
        [BindProperty]
        public DateTime dateFrom { get; set; }

        [BindProperty]
        public DateTime dateTo { get; set; }
        public IndexModel()
        {
            _context = new MyStoreContext();
        }

        public async Task OnGetAsync()
        {
            var staffs = await _context.Staffs.ToListAsync();
            var products =  await _context.Products.ToListAsync();
            var orders = await _context.Orders.Where(item => item.OrderDate > DateTime.Now.AddDays(-30)).ToListAsync();
            var orderDetails = await _context.OrderDetails.ToListAsync();
            var query = from s in staffs
                        join o in orders
                        on s.StaffId equals o.StaffId
                        join od in orderDetails on o.OrderId equals od.OrderId
                        join p in products on od.ProductId equals p.ProductId
                        select new { s, o, od, p };
            int numericalOrder = 0;
            foreach (var item in query)
            {
                numericalOrder++;
                OrderReports.Add(new Models.OrderReport
                {
                    NumericalOrder = numericalOrder,
                    StaffName = item.s.Name,
                    ProductName = item.p.ProductName,
                    OrderDate = item.o.OrderDate,
                    Quantity = item.od.Quantity,
                    UnitPrice = item.od.UnitPrice
                });
            }
        }

        public async Task OnPostAsync()
        {
            var staffs = await _context.Staffs.ToListAsync();
            var products = await _context.Products.ToListAsync();
            var orders = await _context.Orders.Where(item => item.OrderDate >= dateFrom && item.OrderDate <= dateTo).ToListAsync();
            var orderDetails = await _context.OrderDetails.ToListAsync();
            var query = from s in staffs
                        join o in orders
                        on s.StaffId equals o.StaffId
                        join od in orderDetails on o.OrderId equals od.OrderId
                        join p in products on od.ProductId equals p.ProductId
                        select new { s, o, od, p };
            int numericalOrder = 0;
            foreach (var item in query)
            {
                numericalOrder++;
                OrderReports.Add(new Models.OrderReport
                {
                    NumericalOrder = numericalOrder,
                    StaffName = item.s.Name,
                    ProductName = item.p.ProductName,
                    OrderDate = item.o.OrderDate,
                    Quantity = item.od.Quantity,
                    UnitPrice = item.od.UnitPrice
                });
            }
        }

    }
}
