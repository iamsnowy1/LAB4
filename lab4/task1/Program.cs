using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int Rating { get; set; }
    }
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Order> PurchaseHistory { get; set; } = new List<Order>();
    }
    public class Order
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public Dictionary<Product, int> ProductQuantities { get; set; } = new Dictionary<Product, int>();
        public decimal TotalPrice => CalculateTotalPrice();
        public string Status { get; set; }

        private decimal CalculateTotalPrice()
        {
            return ProductQuantities.Sum(entry => entry.Key.Price * entry.Value);
        }
    }
    public interface ISearchable
    {
        List<Product> SearchByPrice(decimal maxPrice);
        List<Product> SearchByCategory(string category);
        List<Product> SearchByRating(int minRating);
    }
    public class Store : ISearchable
    {
        private List<User> users = new List<User>();
        private List<Product> products = new List<Product>();
        private List<Order> orders = new List<Order>();

        public void AddUser(User user)
        {
            users.Add(user);
        }

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public void DisplayProducts()
        {
            Console.WriteLine("Available Products:");
            foreach (var product in products)
            {
                Console.WriteLine($"Name: {product.Name}, Price: {product.Price}, Category: {product.Category}, Rating: {product.Rating}");
            }
        }

        public void PlaceOrder(User user, Dictionary<Product, int> productsAndQuantities)
        {
            Order order = new Order
            {
                Products = productsAndQuantities.Keys.ToList(),
                ProductQuantities = productsAndQuantities,
                Status = "Pending"
            };

            user.PurchaseHistory.Add(order);
            orders.Add(order);

            Console.WriteLine("Order placed successfully!");
        }

        public List<Product> SearchByPrice(decimal maxPrice)
        {
            return products.Where(product => product.Price <= maxPrice).ToList();
        }

        public List<Product> SearchByCategory(string category)
        {
            return products.Where(product => product.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Product> SearchByRating(int minRating)
        {
            return products.Where(product => product.Rating >= minRating).ToList();
        }

        static void Main()
        {
            Store store = new Store();

            User user1 = new User { Username = "john_doe", Password = "password123" };
            User user2 = new User { Username = "jane_smith", Password = "securepass" };

            store.AddUser(user1);
            store.AddUser(user2);

            Product product1 = new Product { Name = "Laptop", Price = 1200, Category = "Electronics", Rating = 4 };
            Product product2 = new Product { Name = "Smartphone", Price = 800, Category = "Electronics", Rating = 5 };
            Product product3 = new Product { Name = "Headphones", Price = 100, Category = "Accessories", Rating = 3 };

            store.AddProduct(product1);
            store.AddProduct(product2);
            store.AddProduct(product3);

            store.DisplayProducts();

            Dictionary<Product, int> user1Order = new Dictionary<Product, int>
        {
            { product1, 1 },
            { product3, 2 }
        };

            store.PlaceOrder(user1, user1Order);

           
            Console.WriteLine("\nSearch results (Category: Electronics):");
            var searchResults = store.SearchByCategory("Electronics");
            foreach (var result in searchResults)
            {
                Console.WriteLine($"Name: {result.Name}, Price: {result.Price}, Category: {result.Category}, Rating: {result.Rating}");
            }
        }
    }
}
