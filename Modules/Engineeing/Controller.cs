using ArchtistStudio.Core;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Modules.Project;
using ArchtistStudio.Modules.CategoryEngineering;
using Microsoft.EntityFrameworkCore;
using ArchtistStudio.Modules.Image;
using ArchtistStudio.Modules.ImageShow;


namespace ArchtistStudio.Modules.Engineeing;


public class EngineeingController(
    IEngineeingRepository repository,
    IProjectRepository projectrepository,
    ICategoryEngineeringRepository categoryEngineeringrepository
    ) : MyAdminController
{


   [HttpGet("all/project")]
    public IActionResult Gets(int pageNumber = 1, int pageSize = 10)
    {
        var projects = projectrepository
            .FindBy(e => e.DeletedAt == null)
            .AsNoTracking()
            .Include(p => p.Images)
                .ThenInclude(img => img.ImageShows)
            .Select(s => new GetCategoryEngineeringByEngineeingResponse
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
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return Ok(projects);
    }


    [HttpGet("Project/{id:guid}")]
    public IActionResult GetByCategoryArchitectureId(Guid id, Guid projectId, int pageNumber = 1, int pageSize = 10)
    {
        var CategoryArchitecture = categoryEngineeringrepository.FindBy(c => c.Id == id).FirstOrDefault();
        if (CategoryArchitecture == null)
        {
            return ItemNotFound();
        }

        var CategoryArchitectureType = CategoryArchitecture.Name?.ToLower() ?? string.Empty;

        var allProjects = projectrepository
            .FindBy(e => e.InActive != true && e.DeletedAt == null)
            .Include(p => p.Images)
                .ThenInclude(img => img.ImageShows)
            .ToList();

        if (allProjects == null || !allProjects.Any())
        {
            return NotFound("No projects found.");
        }

        var filteredProjects = allProjects
            .Where(p => p.Id == projectId &&
                        p.ProjectType != null &&
                        p.ProjectType.Contains(CategoryArchitectureType, StringComparison.OrdinalIgnoreCase))
            .Select(s => new GetCategoryEngineeringByEngineeingResponse
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

        var CategoryArchitectureProjectIds = repository
            .FindBy(e => e.CategoryEngineeringId == id)
            .Select(s => s.ProjectId)
            .ToList();

        if (CategoryArchitectureProjectIds == null)
        {
            return NotFound("CategoryArchitecture project IDs not found.");
        }

        foreach (var tag in filteredProjects)
        {
            tag.Checked = CategoryArchitectureProjectIds.Contains(tag.ProjectId);
        }

        var paginatedProjects = filteredProjects
            .Skip((pageNumber - 1) * pageSize) 
            .Take(pageSize)
            .ToList();

        return Ok(paginatedProjects);
    }

    [HttpGet("CategoryEngineering/{id:guid}")]
    public IActionResult GetByCategoryArchitectureId(Guid id, int pageNumber = 1, int pageSize = 10)
    {
        var CategoryArchitecture = categoryEngineeringrepository.FindBy(c => c.Id == id).FirstOrDefault();
        if (CategoryArchitecture == null) return ItemNotFound();

        var CategoryArchitectureType = CategoryArchitecture.Name?.ToLower() ?? string.Empty;

        var allProjects = projectrepository
            .FindBy(e => e.InActive != true && e.DeletedAt == null)
            .Include(p => p.Images)
            .ThenInclude(img => img.ImageShows)
            .ToList();

        if (allProjects == null || !allProjects.Any())
        {
            return NotFound("No projects found.");
        }

        var projects = allProjects
            .Where(p => p.ProjectType != null &&
                        p.ProjectType.Contains(CategoryArchitectureType, StringComparison.OrdinalIgnoreCase))
            .Select(s => new GetCategoryEngineeringByEngineeingResponse
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

        var CategoryArchitectureProjectIds = repository
            .FindBy(e => e.CategoryEngineeringId == id)
            .Select(s => s.ProjectId)
            .ToList();

        if (CategoryArchitectureProjectIds == null)
        {
            return NotFound("CategoryArchitecture project IDs not found.");
        }

        foreach (var tag in projects)
        {
            tag.Checked = CategoryArchitectureProjectIds.Contains(tag.ProjectId);
        }

        var paginatedProjects = projects
            .Skip((pageNumber - 1) * pageSize) 
            .Take(pageSize) 
            .ToList();

        return Ok(paginatedProjects);
    }
}