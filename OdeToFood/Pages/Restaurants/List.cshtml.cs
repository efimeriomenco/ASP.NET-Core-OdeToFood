using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        private readonly IRestaurantData restaurantData;

        public IEnumerable<Restaurant> Restaurants { get; set; }

        public ListModel(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;

        }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public void OnGet(string searchTerm)
        {
            Restaurants = restaurantData.GetRestaurantsByName(searchTerm);
            Restaurants = restaurantData.GetRestaurantsByLocation(searchTerm);
        }

    }
}
