using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Data;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    [Route("api/suppliers")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private SportsStoreDbContext sportsStoreDb;

        public SupplierController(SportsStoreDbContext sportsStoreDb)
        {
            this.sportsStoreDb = sportsStoreDb;
        }

        [HttpGet]
        public IEnumerable<Supplier> GetSuppliers()
        {
            return sportsStoreDb.Suppliers;
        }

        [HttpPost]
        public IActionResult CreateSupplier([FromBody] Supplier supplier )
        {
            if (ModelState.IsValid)
            {
                Supplier data = supplier;        
                sportsStoreDb.Add(data);
                return Ok(data.SupplierId);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
