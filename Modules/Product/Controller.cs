using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Core;
using ArchtistStudio.Modules.Project;
using ArchtistStudio.Modules.CategoryProduct;
using Microsoft.EntityFrameworkCore;
using ArchtistStudio.Modules.Image;

namespace ArchtistStudio.Modules.Product;


public class ProductController(
    IProductRepository repository,
    IProjectRepository projectrepository,
    ICategoryProductRepository categoryPCategoryProductrepository
    ) : MyAdminController
{

    [HttpGet("all/project")]
    public IActionResult Gets()
    {

        var projects = projectrepository
            .FindBy(e => e.DeletedAt == null)
            .AsNoTracking()
            .Include(p => p.Images)
            .Select(s => new GetCategoryProductByProductResponse
            {
                ProjectId = s.Id,
                Project = new ListProjectResponse
                {
                    ProjectType = s.ProjectType,
                    ProjectName = s.ProjectName,
                    Client = s.Client,
                    Size = s.Size,
                    Status = s.Status,
                    Location = s.Location,
                    Images = s.Images.Select(img => new DatailImageResponse
                    {
                        ImagePath = img.ImagePath,
                        Description = img.Description
                    }).ToList(),
                }
            })
            .ToList();

        return Ok(projects);
    }


    [HttpGet("Project/{id:guid}")]
    public IActionResult GetByCategoryProductId(Guid id, Guid projectId)
    {
        var categoryPCategoryProduct = categoryPCategoryProductrepository.FindBy(c => c.Id == id).FirstOrDefault();
        if (categoryPCategoryProduct == null)
        {
            return ItemNotFound();
        }

        var categoryPCategoryProductType = categoryPCategoryProduct.Name?.ToLower() ?? string.Empty;

        var allProjects = projectrepository
            .FindBy(e => e.InActive != true && e.DeletedAt == null)
            .Include(p => p.Images) // Ensure images are included
            .ToList();

        if (allProjects == null)
        {
            return NotFound("No projects found.");
        }

        var filteredProjects = allProjects
            .Where(p => p.Id == projectId && p.ProjectType != null && p.ProjectType.Contains(categoryPCategoryProductType, StringComparison.OrdinalIgnoreCase))
            .Select(s => new GetCategoryProductByProductResponse
            {
                ProjectId = s.Id,
                Project = new ListProjectResponse
                {
                    ProjectType = s.ProjectType ?? string.Empty,
                    ProjectName = s.ProjectName ?? string.Empty,
                    Client = s.Client ?? string.Empty,
                    Size = s.Size ?? string.Empty,
                    Status = s.Status ?? string.Empty,
                    Location = s.Location ?? string.Empty,
                    Images = s.Images?.Select(img => new DatailImageResponse
                    {
                        ImagePath = img.ImagePath ?? string.Empty,
                        Description = img.Description ?? string.Empty
                    }).ToList() ?? []
                },
                Checked = false
            })
        .ToList();

        var categoryPCategoryProductProjectIds = repository
            .FindBy(e => e.CategoryProductId == id)
            .Select(s => s.ProjectId)
            .ToList() ?? [];

        if (categoryPCategoryProductProjectIds == null)
        {
            return NotFound("CategoryProduct project IDs not found.");
        }

        foreach (var tag in filteredProjects)
        {
            tag.Checked = categoryPCategoryProductProjectIds.Contains(tag.ProjectId);
        }

        return Ok(filteredProjects);
    }

    [HttpGet("CategoryProduct/{id:guid}")]
    public IActionResult GetByCategoryProductId(Guid id)
    {
        var categoryPCategoryProduct = categoryPCategoryProductrepository.FindBy(c => c.Id == id).FirstOrDefault();
        if (categoryPCategoryProduct == null) return ItemNotFound();
        var categoryPCategoryProductType = categoryPCategoryProduct.Name?.ToLower() ?? string.Empty;

        var allProjects = projectrepository
            .FindBy(e => e.InActive != true && e.DeletedAt == null)
            .Include(p => p.Images)
            .ToList();

        if (allProjects == null)
        {
            return NotFound("No projects found.");
        }

        var projects = allProjects
            .Where(p => p.ProjectType != null && p.ProjectType.Contains(categoryPCategoryProductType, StringComparison.OrdinalIgnoreCase))
            .Select(s => new GetCategoryProductByProductResponse
            {
                ProjectId = s.Id,
                Project = new ListProjectResponse
                {
                    ProjectType = s.ProjectType ?? string.Empty,
                    ProjectName = s.ProjectName ?? string.Empty,
                    Client = s.Client ?? string.Empty,
                    Size = s.Size ?? string.Empty,
                    Status = s.Status ?? string.Empty,
                    Location = s.Location ?? string.Empty,
                    Images = s.Images?.Select(img => new DatailImageResponse
                    {
                        ImagePath = img.ImagePath ?? string.Empty,
                        Description = img.Description ?? string.Empty
                    }).ToList() ?? []
                },
                Checked = false
            })
        .ToList();

        var categoryPCategoryProductProjectIds = repository
            .FindBy(e => e.CategoryProductId == id)
            .Select(s => s.ProjectId)
            .ToList() ?? [];

        if (categoryPCategoryProductProjectIds == null)
        {
            return NotFound("CategoryProduct project IDs not found.");
        }

        foreach (var tag in projects)
        {
            tag.Checked = categoryPCategoryProductProjectIds.Contains(tag.ProjectId);
        }

        return Ok(projects);
    }
}