using Microsoft.AspNetCore.Mvc;
using NTier.API.DTOs;
using NTier.Business.Services;
using NTier.DataAccess.Repositories;
using NTier.Entities.Models;

namespace NTier.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(OrderService orderService, ProductRepository productRepository) : ControllerBase
{
    private readonly OrderService _orderService = orderService;
    private readonly ProductRepository _productRepository = productRepository;

    [HttpGet]
    public ActionResult<IEnumerable<GetOrderDto>> GetAll()
    {
        return Ok(_orderService.GetAll().Select(MapOrder));
    }

    [HttpGet("{id:guid}")]
    public ActionResult<GetOrderDto> GetById(Guid id)
    {
        var order = _orderService.GetById(id);
        return order is null ? NotFound() : Ok(MapOrder(order));
    }

    [HttpPost]
    public ActionResult<GetOrderDto> Create(CreateOrderDto dto)
    {
        var order = new Order
        {
            OrderDate = dto.OrderDate,
            TotalAmount = dto.TotalAmount,
            CustomerId = dto.CustomerId,
            Products = _productRepository.GetAll().Where(p => dto.ProductIds.Contains(p.ID)).ToList()
        };

        _orderService.Create(order);
        return CreatedAtAction(nameof(GetById), new { id = order.ID }, MapOrder(order));
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, UpdateOrderDto dto)
    {
        var order = _orderService.GetById(id);
        if (order is null)
            return NotFound();

        order.OrderDate = dto.OrderDate;
        order.TotalAmount = dto.TotalAmount;
        order.CustomerId = dto.CustomerId;
        order.Products = _productRepository.GetAll().Where(p => dto.ProductIds.Contains(p.ID)).ToList();
        _orderService.Update(order);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        if (_orderService.GetById(id) is null)
            return NotFound();

        _orderService.Delete(id);
        return NoContent();
    }

    private static GetOrderDto MapOrder(Order order) => new()
    {
        OrderDate = order.OrderDate,
        TotalAmount = order.TotalAmount,
        CustomerId = order.CustomerId,
        ProductIds = order.Products.Select(p => p.ID).ToList()
    };
}