using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using ArchtistStudio.Modules.CategoryArchitecture;
using ArchtistStudio.Modules.CategoryEngineering;
using ArchtistStudio.Modules.CategoryProduct;

namespace ArchtistStudio.Modules.Category;


public class CategoryController(
    IMapper mapper,
    ICategoryArchitectureRepository categoryArchitectureRepository,
    ICategoryEngineeringRepository categoryEngineeringRepository,
    ICategoryProductRepository categoryProductRepository
    ) : MyController
{
    // === Gets ====//
    [HttpGet]
    public IActionResult Gets()
    {
        var categoryArchitectureQueryable = categoryArchitectureRepository.FindBy(e => e.DeletedAt == null).AsNoTracking();
        var categoryArchitectureLinks = mapper.ProjectTo<ListCategoryArchitectureResponse>(categoryArchitectureQueryable).ToList();

        var categoryEngineeringQueryable = categoryEngineeringRepository.FindBy(e => e.DeletedAt == null).AsNoTracking();
        var categoryEngineeringLinks = mapper.ProjectTo<ListCategoryEngineeringResponse>(categoryEngineeringQueryable).ToList();

        var categoryProductiQueryable = categoryProductRepository.FindBy(e => e.DeletedAt == null).AsNoTracking();
        var categoryProductLinks = mapper.ProjectTo<ListCategoryProductResponse>(categoryProductiQueryable).ToList();

        var response = new Tuple<List<ListCategoryArchitectureResponse>, List<ListCategoryEngineeringResponse>, List<ListCategoryProductResponse>>(
            categoryArchitectureLinks ?? [],
            categoryEngineeringLinks ?? [],
            categoryProductLinks ?? []
        );

        return View(response);
    }
}