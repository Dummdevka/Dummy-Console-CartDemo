using System;
using CartDemo.Controllers;
using CartDemo.DAL;
using CartDemo.Models;

namespace CartDemo.Services
{
	public class ConsoleService
	{
		public delegate void ProductAddedHandler(Product product, int number);
		public event ProductAddedHandler ProductAddedEvent;
		public ProductController productService;
		//protected Cart cart;

		public ConsoleService(Cart cart, DatabaseConnector connector)
		{
			productService = new ProductController(this, connector);
			new CartController(cart, this);
			//cart = new Cart(this);
		}

		public void Run() {
			string exit = String.Empty;
			Console.WriteLine("Welcome to our cart! Type \"exit\" to exit the application");

			while (exit != "exit") {
				//Init Cart 
				List<Product> products = (List<Product>)productService.getProducts();
				listProducts(products);
				Product product = getProduct(products);
				int numberOfItems = getItemsNumber(product.Id);
				ProductAddedEvent?.Invoke(product, numberOfItems);
				//Console.WriteLine("Your selection is " + productId);
				exit = Console.ReadLine();
			}
			
		}

		protected void listProducts(List<Product> products) {
			var productsToList = products.Where(product => product.Availability > 0);
			foreach (var product in productsToList) {
				Console.WriteLine($"ID: {product.Id} Title: \"{product.Title}\" Price: \"{product.Price}\"");
			}
		}

		protected Product getProduct(List<Product> products) {
			int productId = 0;
			bool isValidProduct = false;
			while (!isValidProduct) {
				Console.WriteLine("Select product id");
				try {
					productId = Convert.ToInt32(Console.ReadLine());
				} catch(FormatException e) {
					Console.WriteLine("Only number are allowed!");
					continue;
				}
				
				isValidProduct = products.Where(product => product.Id == productId).Any();

			}

			return productService.getProductById(productId);
		}

		protected int getItemsNumber(int productId) {
			int number = 0;
			int limit = productService.getProductById(productId).Availability;
			while (number == 0 || number > limit) {
				Console.WriteLine($"How many items of this do you want? (max {limit})");
				number = Convert.ToInt32(Console.ReadLine());
			}
			return number;
		}
	}
}

