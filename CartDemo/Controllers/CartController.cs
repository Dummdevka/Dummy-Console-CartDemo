using System;
using CartDemo.Models;
using CartDemo.Services;

namespace CartDemo.Controllers
{
	public class CartController
	{
		protected Cart __cart;
		public CartController(Cart cart, ConsoleService consoleService)
		{
			__cart = cart;
			consoleService.ProductAddedEvent += updateSum;
		}

		public void updateSum(Product product, int number) {
			__cart.Sum += product.Price*number;
			Console.WriteLine("Subtotal is: " + __cart.Sum);
		}
	}
}

