namespace Invoici.Models
{
    public class DashboardViewModel
    {
        public int TotalInvoices { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalUsers { get; set; }
        public int OverdueInvoices { get; set; }

        // Graph Data
        public List<int> MonthlyInvoices { get; set; } // E.g., invoices per month
        public List<decimal> MonthlyRevenue { get; set; }

        // Chat Data
        public List<string> AccountantChat { get; set; }
        public List<string> AdminChat { get; set; }
    }
}
