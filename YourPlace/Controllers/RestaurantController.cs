using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourPlace.Models;
using YourPlace.ViewModels;

namespace YourPlace.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RestaurantController()
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
    }
}