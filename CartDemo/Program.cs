using System;
using CartDemo.DAL;
using CartDemo.Models;
using CartDemo.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

class Program
{
	protected static string basePath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
	public static void Main() {
		//Console.WriteLine(basePath);
		ConfigurationBuilder builder = new ConfigurationBuilder();
		setConfig(builder);

		Log.Logger = new LoggerConfiguration()
			.ReadFrom.Configuration(builder.Build())
			.WriteTo.Console()
			.Enrich.FromLogContext()
			.CreateLogger();

		var host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
		{
			//Add db connector
			services.AddSingleton<DatabaseConnector>();
			services.AddSingleton<ConsoleService>();
			services.AddSingleton<Cart>();
			//services.AddScoped<ProductsControllerDAL>();

		}).UseSerilog().Build();
		ConsoleService svc = ActivatorUtilities.CreateInstance<ConsoleService>(host.Services);
		svc.Run();

	}

	public static void setConfig(ConfigurationBuilder builder) {
		builder.SetBasePath(basePath)
			.AddJsonFile("appsettings.json")
			.AddEnvironmentVariables();
	}
}