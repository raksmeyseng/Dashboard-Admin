using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Core;
using ArchtistStudio.Modules.Project;
using ArchtistStudio.Modules.CategoryProduct;
using Microsoft.EntityFrameworkCore;
using ArchtistStudio.Modules.Image;
using ArchtistStudio.Modules.ImageShow;

namespace ArchtistStudio.Modules.Product;


public class ProductController(
    IProductRepository repository,
    IProjectRepository projectrepository,
    ICategoryProductRepository categoryProductRepository
    ) : MyAdminController
{

    [HttpGet("all/project")]
    public IActionResult Gets()
    {
        var projects = projectrepository
            .FindBy(e => e.DeletedAt == null)
            .AsNoTracking()
            .Include(p => p.Images)
                .ThenInclude(img => img.ImageShows)
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
                        Description = img.Description,
                        ImageShows = img.ImageShows.Select(showImg => new DatailImageShowResponse
                        {
                            ImagePath = showImg.ImagePath,
                            Description = showImg.Description
                        }).ToList()
                    }).ToList(),
                }
            })
            .ToList();

        return Ok(projects);
    }


     [HttpGet("Project/{id:guid}")]
    public IActionResult GetByCategoryProductId(Guid id, Guid projectId)
    {
        var CategoryProduct = categoryProductRepository.FindBy(c => c.Id == id).FirstOrDefault();
        if (CategoryProduct == null)
        {
            return ItemNotFound();
        }

        var CategoryProductType = CategoryProduct.Name?.ToLower() ?? string.Empty;

        var allProjects = projectrepository
            .FindBy(e => e.InActive != true && e.DeletedAt == null)
            .Include(p => p.Images)
                .ThenInclude(img => img.ImageShows).ToList();

        if (allProjects == null)
        {
            return NotFound("No projects found.");
        }

        var filteredProjects = allProjects
            .Where(p => p.Id == projectId && p.ProjectType != null && p.ProjectType.Contains(CategoryProductType, StringComparison.OrdinalIgnoreCase))
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
                    Images = s.Images.Select(img => new DatailImageResponse
                    {
                        ImagePath = img.ImagePath ?? string.Empty,
                        Description = img.Description ?? string.Empty,
                        ImageShows = img.ImageShows?.Select(showImg => new DatailImageShowResponse
                        {
                            ImagePath = showImg.ImagePath ?? string.Empty,
                            Description = showImg.Description ?? string.Empty,
                        }).ToList() ?? []
                    }).ToList() ?? []
                },
                Checked = false
            })
        .ToList();

        var CategoryProductProjectIds = repository
            .FindBy(e => e.CategoryProductId == id)
            .Select(s => s.ProjectId)
            .ToList() ?? [];

        if (CategoryProductProjectIds == null)
        {
            return NotFound("CategoryProduct project IDs not found.");
        }

        foreach (var tag in filteredProjects)
        {
            tag.Checked = CategoryProductProjectIds.Contains(tag.ProjectId);
        }

        return Ok(filteredProjects);
    }

     [HttpGet("CategoryProduct/{id:guid}")]
    public IActionResult GetByCategoryProductId(Guid id)
    {
        var CategoryProduct = categoryProductRepository.FindBy(c => c.Id == id).FirstOrDefault();
        if (CategoryProduct == null) return ItemNotFound();
        var CategoryProductType = CategoryProduct.Name?.ToLower() ?? string.Empty;

        var allProjects = projectrepository
            .FindBy(e => e.InActive != true && e.DeletedAt == null)
            .Include(p => p.Images)
            .ThenInclude(img => img.ImageShows).ToList();

        if (allProjects == null)
        {
            return NotFound("No projects found.");
        }

        var projects = allProjects
            .Where(p => p.ProjectType != null && p.ProjectType.Contains(CategoryProductType, StringComparison.OrdinalIgnoreCase))
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
                    Images = s.Images.Select(img => new DatailImageResponse
                    {
                        ImagePath = img.ImagePath ?? string.Empty,
                        Description = img.Description ?? string.Empty,
                        ImageShows = img.ImageShows?.Select(showImg => new DatailImageShowResponse
                        {
                            ImagePath = showImg.ImagePath ?? string.Empty,
                            Description = showImg.Description ?? string.Empty,
                        }).ToList() ?? []
                    }).ToList() ?? []
                },
                Checked = false
            })
        .ToList();

        var CategoryProductProjectIds = repository
            .FindBy(e => e.CategoryProductId == id)
            .Select(s => s.ProjectId)
            .ToList() ?? [];

        if (CategoryProductProjectIds == null)
        {
            return NotFound("CategoryProduct project IDs not found.");
        }

        foreach (var tag in projects)
        {
            tag.Checked = CategoryProductProjectIds.Contains(tag.ProjectId);
        }

        return Ok(projects);
    }
}