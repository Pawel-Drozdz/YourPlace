using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourPlace.Models;
using YourPlace.ViewModels;

namespace YourPlace.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RestaurantsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var viewModel = new RestaurantsViewModel()
            {
                Restaurants = _context.Restaurants.ToList()
            };
            return View(viewModel);
        }

        public ActionResult NewRestaurant()
        {
            return View();
        }

        public ActionResult Save(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}