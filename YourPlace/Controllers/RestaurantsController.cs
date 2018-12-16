using Microsoft.AspNet.Identity;
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
        [AllowAnonymous]
        public ActionResult Index()
        {
            var viewModel = new AllRestaurantsViewModel()
            {
                Restaurants = _context.Restaurants.ToList()
            };
            return View(viewModel);
        }

        public ActionResult NewRestaurant()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new Restaurant()
                {
                    Name = restaurant.Name,
                    RestaurantType = restaurant.RestaurantType,
                    Localisation = restaurant.Localisation
                };

                if (restaurant.Id == 0)
                {
                    return View("NewRestaurant", viewModel);
                }
                else
                {
                    return View("Edit", viewModel);
                }
            }

            if (restaurant.Id == 0)
            {
                _context.Restaurants.Add(restaurant);
            }
            else
            {
                var restaurantInDb = _context.Restaurants.Single(r => r.Id == restaurant.Id);
                restaurantInDb.Name = restaurant.Name;
                restaurantInDb.RestaurantType = restaurant.RestaurantType;
                restaurantInDb.Localisation = restaurant.Localisation;
            }
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
                var restaurantViewModel = new RestaurantViewModel()
                {
                    Restaurant = _context.Restaurants.FirstOrDefault(r => r.Id == id),
                    Comments = _context.Comments.Where(c => c.RestaurantId == id).ToList(),
                    NewComment = new Comment()
                    {
                        
                    }
                };

                if (restaurantViewModel.Restaurant == null)
                {
                    return HttpNotFound();
                }
                return View(restaurantViewModel);
        }
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var restaurant = _context.Restaurants.Single(r => r.Id == id);
            return View(restaurant);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var restaurant = _context.Restaurants.SingleOrDefault(r => r.Id == id);

            if (restaurant == null)
            {
                return HttpNotFound();
            }

            _context.Restaurants.Remove(restaurant);
            _context.SaveChanges();

            return RedirectToAction("Index", "Restaurants");
        }

        public ActionResult AddComment(int id, Comment newComment)
        {
            var userId = User.Identity.GetUserId();
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            newComment.RestaurantId = id;
            newComment.AuthorId = Guid.Parse(userId);
            newComment.AuthorName = user.UserName;

            _context.Comments.Add(newComment);
            _context.SaveChanges();

            return RedirectToAction("Details", "Restaurants", new { Id = id});
        }
    }
}