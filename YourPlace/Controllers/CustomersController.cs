using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourPlace.Models;
using YourPlace.ViewModels;

namespace YourPlace.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        
        public ActionResult Index()
        {
            var customers = new AllCustomersViewModel()
            {
                Customers = _context.Customers.ToList()
            };
            return View(customers);
        }

        public ActionResult NewCustomer()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        
    }
}