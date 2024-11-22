using Invoici.Data;
using Invoici.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Invoici.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Invoice
        public IActionResult Index()
        {
            var invoices = _context.Invoices.ToList();
            return View(invoices);
        }


        //Create
        public IActionResult Create()
        {
            // Initialize an Invoice with one default InvoiceItem
            var invoice = new Invoice
            {
                Date = DateTime.Now,
                InvoiceItems = new List<InvoiceItem>
                {
                    new InvoiceItem()
                },

            };
            invoice.InvoiceItems[0].InvoiceId = "1";
            return View(invoice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Invoice invoice)
        {
            // Set the InvoiceId for all related InvoiceItems
            foreach (var item in invoice.InvoiceItems)
            {
                item.InvoiceId = invoice.InvoiceId;
                Console.WriteLine("InvoiceId is " + item.InvoiceId);

            }
            if (ModelState.IsValid)
            {
                // Add the invoice, including related items
                _context.Invoices.Add(invoice); 
                foreach (var item in invoice.InvoiceItems)
                {
                    item.InvoiceId = invoice.InvoiceId;
                    Console.WriteLine("InvoiceId is " + item.InvoiceId);
                    _context.InvoiceItems.Add(item);
                }
                _context.SaveChanges();

                return RedirectToAction("Index"); // Redirect to the invoice list after saving
            }

            else
            {
                foreach (var state in ModelState)
                {
                    Console.WriteLine($"{state.Key}: {string.Join(", ", state.Value.Errors.Select(e => e.ErrorMessage))}");
                }
                return View(invoice);
            }
        }

        //Delete
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var invoice = _context.Invoices.FirstOrDefault(i => i.InvoiceId == id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice); // Optional: Show a confirmation page
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var invoice = _context.Invoices.Include(i => i.InvoiceItems)
                                           .FirstOrDefault(i => i.InvoiceId == id);

            if (invoice != null)
            {
                // Remove the invoice along with its items
                _context.Invoices.Remove(invoice);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        //Update/edit
        [HttpGet]
        public IActionResult Edit(string id)
        {
            // Find the invoice by ID, including its related items
            var invoice = _context.Invoices
                                  .Include(i => i.InvoiceItems)
                                  .FirstOrDefault(i => i.InvoiceId == id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, Invoice invoice)
        {
            if (id != invoice.InvoiceId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var existingInvoice = _context.Invoices
                                              .Include(i => i.InvoiceItems)
                                              .FirstOrDefault(i => i.InvoiceId == id);

                if (existingInvoice == null)
                {
                    return NotFound();
                }

                // Update the existing invoice properties
                existingInvoice.PartyName = invoice.PartyName;
                existingInvoice.Date = invoice.Date;
                existingInvoice.Description = invoice.Description;
                existingInvoice.GSTNumber = invoice.GSTNumber;

                // Remove existing invoice items
                _context.InvoiceItems.RemoveRange(existingInvoice.InvoiceItems);

                // Ensure InvoiceId is set for each new InvoiceItem
                foreach (var item in invoice.InvoiceItems)
                {
                    item.InvoiceId = invoice.InvoiceId;
                }

                // Add updated invoice items
                _context.InvoiceItems.AddRange(invoice.InvoiceItems);

                // Save changes
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            // Log validation errors for debugging
            foreach (var state in ModelState)
            {
                Console.WriteLine($"{state.Key}: {string.Join(", ", state.Value.Errors.Select(e => e.ErrorMessage))}");
            }

            return View(invoice);
        }

    }
}
