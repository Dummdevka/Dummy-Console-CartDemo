using System;
using CartDemo.DAL;
using CartDemo.Models;
using CartDemo.Services;

namespace CartDemo.Controllers
{
	public class ProductController
	{
		protected readonly ProductsControllerDAL DAL;
		public ProductController(ConsoleService consoleService, DatabaseConnector connector)
		{
			DAL = new ProductsControllerDAL(connector);
			consoleService.ProductAddedEvent += updateAvailability;
		}

		public void updateAvailability(Product product, int number) {
			product.Availability -= number;
			DAL.updateProduct(product);
		}

		public IEnumerable<Product> getProducts() {
			return DAL.getProducts();
		}

		public Product getProductById(int id) {
			return DAL.getProductById(id);
		}
	}
}

