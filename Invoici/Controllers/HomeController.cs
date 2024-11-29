using Invoici.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Invoici.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Dummy data for dashboard
            var dashboardData = new DashboardViewModel
            {
                TotalInvoices = 25,
                TotalRevenue = 50000.75M,
                TotalUsers = 10,
                OverdueInvoices = 3,
                MonthlyInvoices = new List<int> { 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60 },
                MonthlyRevenue = new List<decimal> { 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000, 11000, 12000 },
                AccountantChat = new List<string> { "How to add a new invoice?", "Can you clarify this payment?" },
                AdminChat = new List<string> { "Complaint about login issue.", "Feedback: Please add export functionality." }
            };

            return View(dashboardData);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
