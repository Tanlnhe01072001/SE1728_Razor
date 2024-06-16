namespace SE1728_Razor.Models
{
    public class OrderReport
    {
        public int? NumericalOrder { get; set; }
        public string StaffName { get; set; }
        public string ProductName { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public string CategoryName { get; set; }
    }
}
