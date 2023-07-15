using System;
using CartDemo.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace CartDemo.DAL
{
	public class ProductsControllerDAL
	{
		private readonly DatabaseConnector __connector;

		public ProductsControllerDAL(DatabaseConnector connector)
		{
			__connector = connector;
		}

		public IEnumerable<Product> getProducts() {
			using (SqlConnection connection = __connector.getConnection()) {
				List<Product> products = (List<Product>)connection.Query<Product>("select * from Products");
				return products;
			}
		}

		public Product getProductById(int id) {
			using (SqlConnection connection = __connector.getConnection()) {
				List<Product> products = (List<Product>)connection.Query<Product>("select * from Products where Id=@id",
					new {
						id = id
					});
				return products.First();
			}
		}

		public void updateProduct(Product product) {
			using (SqlConnection connection = __connector.getConnection()) {
				connection.Query<Product>($"update Products set Title=@title, Price=@price, Availability=@availability where Id=@id",
					new {
					title = product.Title,
					price = product.Price,
					availability = product.Availability,
					id = product.Id
				});
			}
		}
	}
}

