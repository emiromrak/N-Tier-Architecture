using Microsoft.AspNetCore.Mvc;
using NTier.API.DTOs;
using NTier.Business.Services;
using NTier.Entities.Models;

namespace NTier.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController(CustomerService CustomerService) : ControllerBase
{
    private readonly CustomerService _CustomerService = CustomerService;

    [HttpGet]
    public ActionResult<IEnumerable<GetCustomerDto>> GetAll()
    {
        return Ok(_CustomerService.GetAll().Select(MapCustomer));
    }

    [HttpGet("{id:guid}")]
    public ActionResult<GetCustomerDto> GetById(Guid id)
    {
        var Customer = _CustomerService.GetById(id);
        return Customer is null ? NotFound() : Ok(MapCustomer(Customer));
    }

    [HttpPost]
    public ActionResult<GetCustomerDto> Create(CreateCustomerDto dto)
    {
        var Customer = new Customer
        {
            Name = dto.Name
        };

        _CustomerService.Create(Customer);
        return CreatedAtAction(nameof(GetById), new { id = Customer.ID }, MapCustomer(Customer));
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, UpdateCustomerDto dto)
    {
        var Customer = _CustomerService.GetById(id);
        if (Customer is null)
            return NotFound();

        Customer.Name = dto.Name;
        _CustomerService.Update(Customer);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        if (_CustomerService.GetById(id) is null)
            return NotFound();

        _CustomerService.Delete(id);
        return NoContent();
    }

    
    private static GetCustomerDto MapCustomer(Customer Customer) => new()
    {
        Id = Customer.ID,
        Name = Customer.Name
    };
}