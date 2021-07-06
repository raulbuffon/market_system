using Market_system.Models;
using Market_system.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Market_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public ActionResult<List<Order>> Get()
        {
            return _orderService.GetAll();
        }

        [HttpGet("{id:length(24)}", Name = "GetOrder")]
        public ActionResult<Order> GetById(string id)
        {
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
            _orderService.Create(order);

            return CreatedAtRoute("GetOrder", new { id = order.Id.ToString() }, order);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Order orderIn) // problems to update an order
        {
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