using Microsoft.AspNetCore.Mvc;
using NTier.API.DTOs;
using NTier.Business.Services;
using NTier.Entities.Models;

namespace NTier.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(CategoryService categoryService) : ControllerBase
{
    private readonly CategoryService _categoryService = categoryService;

    [HttpGet]
    public ActionResult<IEnumerable<GetCategoryDto>> GetAll()
    {
        return Ok(_categoryService.GetAll().Select(MapCategory));
    }

    [HttpGet("{id:guid}")]
    public ActionResult<GetCategoryDto> GetById(Guid id)
    {
        var category = _categoryService.GetById(id);
        return category is null ? NotFound() : Ok(MapCategory(category));
    }

    [HttpPost]
    public ActionResult<GetCategoryDto> Create(CreateCategoryDto dto)
    {
        var category = new Category
        {
            Name = dto.Name,
            Description = dto.Description,
            IsActive = dto.IsActive
        };

        _categoryService.Create(category);
        return CreatedAtAction(nameof(GetById), new { id = category.ID }, MapCategory(category));
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, UpdateCategoryDto dto)
    {
        var category = _categoryService.GetById(id);
        if (category is null)
            return NotFound();

        category.Name = dto.Name;
        category.Description = dto.Description;
        category.IsActive = dto.IsActive;
        _categoryService.Update(category);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        if (_categoryService.GetById(id) is null)
            return NotFound();

        _categoryService.Delete(id);
        return NoContent();
    }

    [HttpGet("{id:guid}/products")]
    public ActionResult<IEnumerable<GetProductDto>> GetProducts(Guid id)
    {
        var category = _categoryService.GetById(id);
        if (category is null)
            return NotFound();

        return Ok(category.Products.Select(ProductController.MapProduct));
    }

    private static GetCategoryDto MapCategory(Category category) => new()
    {
        ID = category.ID,
        Name = category.Name,
        Description = category.Description,
        IsActive = category.IsActive,
        CreatedDate = category.CreatedDate,
        UpdatedDate = category.UpdatedDate
    };
}
