using Microsoft.AspNetCore.Mvc;
using NTier.API.DTOs;
using NTier.Business.Services;
using NTier.DataAccess.Context;
using NTier.DataAccess.Repositories;
using NTier.Entities.Models;

namespace NTier.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ADBContext _context;
    private readonly CategoryService _categoryService;

    public CategoryController()
    {
        _context = new ADBContext();
        var categoryRepo = new CategoryRepository(_context);
        _categoryService = new CategoryService(categoryRepo);

    }
    //[HttpGet]
    //public List<Category> GetAll()
    //{
    //    return _categoryService.GetAll().ToList();
    //}

    //[HttpPost("{name}/{desc}/{isActive}")]
    //public void Create([FromRoute] string name, [FromRoute] string desc, [FromRoute] bool isActive)
    //{
    //    var category = new Category() { Name = name, Description = desc, IsActive = isActive };
    //    _categoryService.Create(category);
    //}

    [HttpGet]
    public List<GetCategoryDto> GetAll()
    {
        var categories = new List<GetCategoryDto>();
        foreach (var category in _categoryService.GetAll())
        {
            categories.Add(new GetCategoryDto() { Name = category.Name, Description = category.Description, IsActive = category.IsActive, CreatedDate = category.CreatedDate });
        }

        return categories;
    }


    [HttpPost]
    public void Create([FromBody] CreateCategoryDto dto)
    {
        var category = new Category() { Name = dto.Name, Description = dto.Description, IsActive = dto.IsActive };
        _categoryService.Create(category);
    }

    [HttpGet("{ID}")]
    public GetCategoryDto GetByID([FromRoute] string ID)
    {
        var category = _categoryService.GetById(Guid.Parse(ID));
        return
            new GetCategoryDto() { Name=category.Name,Description=category.Description,IsActive=category.IsActive,CreatedDate=category.CreatedDate};
    }

    [HttpDelete("{ID}")]
    public void Delete([FromRoute] string ID)
    {
        _categoryService.Delete(Guid.Parse(ID));
    }

    [HttpPut("{id}")]
    public void Update([FromRoute] string id, [FromBody] UpdateCategoryDto dto)
    {
        _categoryService.Update
            (new Category() { Name = dto.Name, Description = dto.Description, IsActive = dto.IsActive, ID = Guid.Parse(id) });
    }
}