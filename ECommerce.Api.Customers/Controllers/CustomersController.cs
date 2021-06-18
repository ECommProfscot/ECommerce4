using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Controllers
{
    [ApiController]
    [Route("api/Customers")]
    public class CustomersController : ControllerBase
    {

        private readonly ICustomersProvider CustomersProvider;

        public CustomersController(ICustomersProvider CustomersProvider)
        {
            this.CustomersProvider = CustomersProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await CustomersProvider.GetCustomersAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Customers);
            }

            return NotFound();
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetProductAsync(int customerId)
        {
            var result = await CustomersProvider.GetCustomerAsync(customerId);
            if (result.IsSuccess)
            {
                return Ok(result.Customer);
            }

            return NotFound();
        }


    }
}
