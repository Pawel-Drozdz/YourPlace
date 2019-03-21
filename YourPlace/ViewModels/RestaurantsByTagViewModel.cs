using System.Collections.Generic;
using YourPlace.Models;

namespace YourPlace.ViewModels
{
    public class RestaurantsByTagViewModel
    {
        public List<RestaurantTypeTags> RestaurantTypeTags { get; set; }
        public TypeTag TypeTag { get; set; }
    }
}