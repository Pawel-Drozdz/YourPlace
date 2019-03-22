using System.Collections.Generic;
using YourPlace.Models;

namespace YourPlace.ViewModels
{
    public class RestaurantsByTagViewModel
    {
        public List<Restaurant> Restaurants { get; set; }
        public string TagBody { get; set; }
    }
}