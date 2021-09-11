using Market_system.Models;
using Market_system.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Market_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService, ILogger<ProductsController> logger)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [HttpGet]
        public ActionResult<List<Order>> Get()
        {
            _logger.LogInformation("Call controller - Get all orders");
            return _orderService.GetAll();
        }

        [HttpGet("{id:length(24)}", Name = "GetOrder")]
        public ActionResult<Order> GetById(string id)
        {
            _logger.LogInformation($"Call controller - Get order by Id - ID {id}");
            var order = _orderService.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpPost]
        public ActionResult<Order> Create(Order order)
        {
            _logger.LogInformation("Call controller - Post new order");
            _orderService.Create(order);

            return CreatedAtRoute("GetOrder", new { id = order.Id.ToString() }, order);
        }

        [HttpPost]
        [Route("CreateWithProductsName")]
        public ActionResult<Order> Create(List<string> products)
        {
            _logger.LogInformation("Call controller - Post new order");
            var result = _orderService.CreateWithProductsName(products);

            if(result == null) {
                return Accepted();
            }
            
            return CreatedAtRoute("GetOrder", new { id = result.Id.ToString() }, result);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Order orderIn)
        {
            _logger.LogInformation("Call controller - Put to update order");
            var order = _orderService.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            _orderService.Update(id, orderIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            _logger.LogInformation($"Call controller - Delete order by Id - ID {id}");
            var order = _orderService.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            _orderService.RemoveById(order.Id);

            return NoContent();
        }
    }
}