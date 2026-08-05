using Microsoft.AspNetCore.Mvc;
using NTier.API.DTOs;
using NTier.Business.Services;
using NTier.DataAccess.Context;
using NTier.DataAccess.Repositories;
using NTier.Entities.Models;

namespace NTier.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ProductController : ControllerBase
{

    private readonly ADBContext _context;
    private readonly ProductService _productService;

    public ProductController()
    {
        _context = new ADBContext();
        var productRepo = new ProductRepository(_context);
        _productService = new ProductService(productRepo);
    }

    [HttpGet]
    public List<GetProductDto> GetAll()
    {
        var products = new List<GetProductDto>();
        foreach (var product in _productService.GetAll())
        {
            products.Add(new GetProductDto()
            { Name = product.Name, UnitPrice = product.UnitPrice, UnitInStock = product.UnitInStock });
        }
        return products;
    }

    [HttpPost]
    public void Create([FromBody] CreateProductDto dto)
    {
        var products = new Product() { Name = dto.Name, UnitPrice = dto.UnitPrice, UnitInStock = dto.UnitInStock };
        _productService.Create(products);
    }

    [HttpDelete("{ID}")]
    public void Delete([FromRoute] string ID)
    {
        _productService.Delete(Guid.Parse(ID));
    }

    [HttpPut("{id}")]
    public void Update([FromRoute] string id, [FromBody] UpdateProductDto dto)
    {
        _productService.Update(
            new Product() { ID = Guid.Parse(id), Name = dto.Name, UnitPrice = dto.UnitPrice, UnitInStock = dto.UnitInStock });
    }

    [HttpGet("{ID}")]
    public GetProductDto GetByID([FromRoute] string ID)
    {
        var product = _productService.GetById(Guid.Parse(ID));
        return new GetProductDto() { Name = product.Name, UnitInStock = product.UnitInStock, UnitPrice = product.UnitPrice };

    }


    [HttpGet("GetAllWCategory")]
    public List<GetProductsWCategoryDto> GetAllWCategory()
    {
        var products = new List<GetProductsWCategoryDto>();
        foreach (var product in _productService?.GetAll())
        {
            products.Add(new GetProductsWCategoryDto()
            { Name = product.Name, UnitPrice = product.UnitPrice, UnitInStock = product.UnitInStock , CategoryName=product?.Category?.Name});
        }
        return products;
    }
}