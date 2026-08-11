using Microsoft.AspNetCore.Mvc;
using NTier.API.DTOs;
using NTier.Business.Services;
using NTier.Entities.Models;

namespace NTier.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController(ProductService productService) : ControllerBase
{
    private readonly ProductService _productService = productService;

    [HttpGet]
    public ActionResult<IEnumerable<GetProductDto>> GetAll()
    {
        return Ok(_productService.GetAll().Select(MapProduct));
    }

    [HttpGet("{id:guid}")]
    public ActionResult<GetProductDto> GetById(Guid id)
    {
        var product = _productService.GetById(id);
        return product is null ? NotFound() : Ok(MapProduct(product));
    }

    [HttpPost]
    public ActionResult<GetProductDto> Create(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            UnitPrice = dto.UnitPrice,
            UnitInStock = dto.UnitInStock,
            Discontinued = dto.Discontinued,
            IsActive = !dto.Discontinued,
            CategoryID = dto.CategoryID
        };

        _productService.Create(product);
        return CreatedAtAction(nameof(GetById), new { id = product.ID }, MapProduct(product));
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, UpdateProductDto dto)
    {
        var product = _productService.GetById(id);
        if (product is null)
            return NotFound();

        product.Name = dto.Name;
        product.UnitPrice = dto.UnitPrice;
        product.UnitInStock = dto.UnitInStock;
        product.Discontinued = dto.Discontinued;
        product.IsActive = !dto.Discontinued;
        product.CategoryID = dto.CategoryID;
        _productService.Update(product);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        if (_productService.GetById(id) is null)
            return NotFound();

        _productService.Delete(id);
        return NoContent();
    }

    [HttpGet("with-category")]
    public ActionResult<IEnumerable<GetProductsWCategoryDto>> GetAllWithCategory()
    {
        return Ok(_productService.GetAll().Select(product => new GetProductsWCategoryDto
        {
            ID = product.ID,
            Name = product.Name,
            UnitPrice = product.UnitPrice,
            UnitInStock = product.UnitInStock,
            Discontinued = product.Discontinued,
            IsActive = product.IsActive,
            CategoryID = product.CategoryID,
            CategoryName = product.Category?.Name
        }));
    }

    internal static GetProductDto MapProduct(Product product) => new()
    {
        ID = product.ID,
        Name = product.Name,
        UnitPrice = product.UnitPrice,
        UnitInStock = product.UnitInStock,
        Discontinued = product.Discontinued,
        IsActive = product.IsActive,
        CategoryID = product.CategoryID
    };
}
