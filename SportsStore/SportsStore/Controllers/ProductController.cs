using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Data;
using SportsStore.Models;

namespace SportsStore.Controllers
{
	[Route("api/products")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private SportsStoreDbContext sportsStoreDb;

		public ProductController(SportsStoreDbContext sportsStoreDb)
		{
			this.sportsStoreDb = sportsStoreDb;
		}

		[HttpGet("{id}")]
		public Product GetProduct(long id)
		{
			return sportsStoreDb.Products.Find(id);
		}
	}
}