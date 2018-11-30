﻿using System;
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

        public ActionResult Details(int id)
        {
            var restaurant = _context.Restaurants.FirstOrDefault(r => r.Id == id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
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
    }
}