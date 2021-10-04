using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetOrdersByCustomerId")]
        [Authorize(Policy = "OnlyNonBlockedCustomer")]
        public async Task<IActionResult> GetOrders()
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;

            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
            {
                return Unauthorized("Invalid customer");
            }

            var orders = await _orderService.GetOrdersByCustomerId(int.Parse(claim.Value));

            if (orders == null || !orders.Any())
            {
                return BadRequest($"No order was found");
            }

            return Ok(orders);
        }
    }
}
