﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    Localisation = restaurant.Localisation,
                    RestaurantTypeTags = restaurant.RestaurantTypeTags
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
                restaurantInDb.RestaurantTypeTags = restaurant.RestaurantTypeTags;
            }
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var userId = User.Identity.GetUserId();
            float oldrate;
            if (User.Identity.IsAuthenticated)
            {
                oldrate = GetUsersOldRate(userId, id);
            }
            else
            {
                oldrate = 0;
            }
            var restaurantViewModel = new RestaurantViewModel()
            {
                Restaurant = _context.Restaurants.FirstOrDefault(r => r.Id == id),
                Comments = GetComments(id),
                NewComment = new Comment(),
                Rating = GetRestaurantRating(id),
                OldRate = oldrate,
                Tags = GetRestaurantsTypeTags(id)
            };

            foreach (var comment in restaurantViewModel.Comments)
            {
                comment.Replies = _context.Replies.Where(r => r.CommentId == comment.Id && r.ParentReplyId == 0).ToList();
                foreach (var parentReply in comment.Replies)
                {
                    parentReply.ChildReplies = GetChildReplies(parentReply);
                }
            }

            if (restaurantViewModel.Restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurantViewModel);
        }

        //Helper method, should be moved to a different file
        public List<Comment> GetComments(int id)
        {
            var Comments = _context.Comments.Where(c => c.RestaurantId == id).ToList();
            var comments = new List<Comment>();
            foreach (var c in Comments)
            {
                var parentReplies = GetParentReplies(c);
                comments.Add(new Comment() { AuthorId = c.AuthorId, AuthorName = c.AuthorName, Body = c.Body, DateTime = c.DateTime,
                Id = c.Id, Replies = parentReplies});
            }
            return comments;
        }

        //Helper method, should be moved to a different file
        public List<Reply> GetParentReplies(Comment comment)
        {
            var parentReplies = _context.Replies.Where(r => r.CommentId == comment.Id && r.ParentReplyId == 0).ToList();
            var parReplies = new List<Reply>();
            foreach (var pr in parentReplies)
            {
                var childRep = GetChildReplies(pr);
                parReplies.Add(new Reply() { AuthorId = pr.AuthorId, AuthorName = pr.AuthorName, Body = pr.Body, CreateDate = pr.CreateDate,
                    ParentReplyId = pr.ParentReplyId, Id = pr.Id, ChildReplies = pr.ChildReplies });
            }
            return parReplies;
        }

        //Helper method, should be moved to a different file
        public List<Reply> GetChildReplies(Reply reply)
        {
            var childReplies = _context.Replies.Where(r => r.ParentReplyId == reply.Id).ToList();
            var chReplies = new List<Reply>();
            foreach (var chr in childReplies)
            {
                var chRep = GetChildReplies(chr);
                chReplies.Add(new Reply() { AuthorId = chr.AuthorId, AuthorName = chr.AuthorName, Body = chr.Body, CreateDate = chr.CreateDate,
                    ParentReplyId = chr.ParentReplyId, Id = chr.Id, ChildReplies = chRep});
            }
            return chReplies;
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

        public ActionResult AddComment(RestaurantViewModel model, Comment newComment)
        {
            var userId = User.Identity.GetUserId();
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            newComment.RestaurantId = model.Restaurant.Id;
            newComment.AuthorId = Guid.Parse(userId);
            newComment.AuthorName = user.UserName;
            newComment.DateTime = DateTime.Now;

            _context.Comments.Add(newComment);
            _context.SaveChanges();

            return RedirectToAction("Details", "Restaurants", new { id = model.Restaurant.Id});
        }

        public ActionResult AddParentReply(int restaurantId, int commentId, Reply newParentReply)
        {
            var userId = User.Identity.GetUserId();
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            newParentReply.ParentReplyId = 0;
            newParentReply.CommentId = commentId;
            newParentReply.AuthorId = Guid.Parse(userId);
            newParentReply.AuthorName = user.UserName;
            newParentReply.CreateDate = DateTime.Now;

            _context.Replies.Add(newParentReply);
            _context.SaveChanges();

            return RedirectToAction("Details", "Restaurants", new { id = restaurantId });
        }

        public ActionResult AddChildReply(int restaurantId, int commentId, int parentReplyId, Reply newChildReply)
        {
            var userId = User.Identity.GetUserId();
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            newChildReply.ParentReplyId = parentReplyId;
            newChildReply.CommentId = commentId;
            newChildReply.AuthorId = Guid.Parse(userId);
            newChildReply.AuthorName = user.UserName;
            newChildReply.CreateDate = DateTime.Now;

            _context.Replies.Add(newChildReply);
            _context.SaveChanges();

            return RedirectToAction("Details", "Restaurants", new { id = restaurantId });
        }

        [AllowAnonymous]
        public ActionResult RestaurantsByTag(int id, string tagBody)
        {
            var restsAndTags = _context.RestaurantTypeTags.Include("Restaurant")
                .Where(r => r.TypeTagId == id)
                .ToList();

            var restaurants = new List<Restaurant>();

            foreach (var r in restsAndTags)
            {
                if(r.Restaurant != null)
                {
                    restaurants.Add(r.Restaurant);
                }
            }

            var viewModel = new RestaurantsByTagViewModel { Restaurants = restaurants, TagBody = tagBody }; 

            return View(viewModel);
        }

        //Helper method, should be moved to a different file
        public float GetRestaurantRating(int id)
        {
            float rating;
            var ratings = _context.Ratings.Where(r => r.RestaurantId == id).ToList();
            float sumOfRatings = 0;
            int countOfRatings = 0;
            if (ratings.Count != 0)
            {
                foreach (var r in ratings)
                {
                    sumOfRatings += r.Rating;
                    countOfRatings += 1;
                }

                rating = (sumOfRatings / countOfRatings);
                return rating;
            }
            else
            {
                return 0;
            }
        }

        //Helper method, should be moved to a different file
        public float GetUsersOldRate (string userId, int restaurantId)
        {
            var guidUserId = new Guid(userId);
            var rate = _context.Ratings.FirstOrDefault(r => r.UserId == guidUserId && r.RestaurantId == restaurantId);
            if (rate == null)
            {
                return 0;
            }
            return rate.Rating;
        }

        public ActionResult RateRestaurant(int newRate, int restaurantId)
        {
            var stringUserId = User.Identity.GetUserId();
            var userId = new Guid(stringUserId);
            var rate = new Rate() { Rating = newRate, UserId = userId, RestaurantId = restaurantId };

            _context.Ratings.Add(rate);
            _context.SaveChanges();

            return RedirectToAction("Details", "Restaurants", new { id = restaurantId });
        }

        public ActionResult UpdateRateOfRestaurant(int newRate, int restaurantId)
        {
            var stringUserId = User.Identity.GetUserId();
            var userId = new Guid(stringUserId);
            var rateFromDB = _context.Ratings.FirstOrDefault(r => r.UserId == userId && r.RestaurantId == restaurantId);
            rateFromDB.Rating = newRate;

            _context.SaveChanges();

            return RedirectToAction("Details", "Restaurants", new { id = restaurantId });
        }

        //Refactor this!
        public List<TypeTag> GetRestaurantsTypeTags(int id)
        {
            var restaurantTypeTags = _context.RestaurantTypeTags.Where(r => r.RestaurantId == id).ToList();
            var typeTags = new List<TypeTag>();

            foreach (var r in restaurantTypeTags)
            {
                var tag = _context.TypeTags.FirstOrDefault(t => t.Id == r.TypeTagId);
                if (tag != null)
                {
                    typeTags.Add(tag);
                }
            }
            return typeTags;
        }
    }
}