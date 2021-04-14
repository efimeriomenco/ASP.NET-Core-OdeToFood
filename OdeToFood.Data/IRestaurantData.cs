using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static OdeToFood.Core.Restaurant;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        IEnumerable<Restaurant> GetRestaurantsByLocation(string location);

        Restaurant GetById(int id);

        Restaurant Update(Restaurant updatedRestaurant);
        Restaurant Add(Restaurant newRestaurant);

        int Commit();
    }
    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                 new Restaurant {Id = 1 , Name="Pizza Mania" , Location="Moldova" , Cuisine= CuisineType.Indian },
                 new Restaurant {Id = 2 , Name="Andys Pizza" , Location="Romania" , Cuisine= CuisineType.Italian },
                 new Restaurant {Id = 3 , Name="Domino's Pizza" , Location="Russia" , Cuisine= CuisineType.Italian }
            };
        }

        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }


        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
  //Daca Id-ul obiectului este egal cu Id-ul obiectului modificat atunci se va prelua toate datele si se va introduce in variabila restaurant 
            var restaurant = restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);
  //Daca obiectul preluat nu este null atunci aplicam modificarile noi.
            if(restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }
            return restaurant;
        }
        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name) 
                   orderby r.Name
                   select r;
            
        }
        public IEnumerable<Restaurant> GetRestaurantsByLocation(string location = null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(location) || r.Location.StartsWith(location)
                   orderby r.Location
                   select r;

        }

        public int Commit()
        {
            return 0;
        }
    }
}
