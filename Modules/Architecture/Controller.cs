using ArchtistStudio.Core;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Modules.Project;
using ArchtistStudio.Modules.CategoryArchitecture;
using Microsoft.EntityFrameworkCore;
using ArchtistStudio.Modules.Image;

namespace ArchtistStudio.Modules.Architecture;


public class ArchitectureController(
    IArchitectureRepository repository,
    IProjectRepository projectrepository,
    ICategoryArchitectureRepository categoryArchitecturerepository
    ) : MyAdminController
{ 

    [HttpGet("all/project")]
    public IActionResult Gets()
    {

        var projects = projectrepository
            .FindBy(e => e.DeletedAt == null)
            .AsNoTracking()
            .Include(p => p.Images)
            .Select(s => new GetCategoryArchitectureByArchitectureResponse
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
    public IActionResult GetByCategoryArchitectureId(Guid id, Guid projectId)
    {
        var CategoryArchitecture = categoryArchitecturerepository.FindBy(c => c.Id == id).FirstOrDefault();
        if (CategoryArchitecture == null)
        {
            return ItemNotFound();
        }

        var CategoryArchitectureType = CategoryArchitecture.Name?.ToLower() ?? string.Empty;

        var allProjects = projectrepository
            .FindBy(e => e.InActive != true && e.DeletedAt == null)
            .Include(p => p.Images)
            .ToList();

        if (allProjects == null)
        {
            return NotFound("No projects found.");
        }

        var filteredProjects = allProjects
            .Where(p => p.Id == projectId && p.ProjectType != null && p.ProjectType.Contains(CategoryArchitectureType, StringComparison.OrdinalIgnoreCase))
            .Select(s => new GetCategoryArchitectureByArchitectureResponse
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

        var CategoryArchitectureProjectIds = repository
            .FindBy(e => e.CategoryArchitectureId == id)
            .Select(s => s.ProjectId)
            .ToList() ?? [];

        if (CategoryArchitectureProjectIds == null)
        {
            return NotFound("CategoryArchitecture project IDs not found.");
        }

        foreach (var tag in filteredProjects)
        {
            tag.Checked = CategoryArchitectureProjectIds.Contains(tag.ProjectId);
        }

        return Ok(filteredProjects);
    }

    [HttpGet("CategoryArchitecture/{id:guid}")]
    public IActionResult GetByCategoryArchitectureId(Guid id)
    {
        var CategoryArchitecture = categoryArchitecturerepository.FindBy(c => c.Id == id).FirstOrDefault();
        if (CategoryArchitecture == null) return ItemNotFound();
        var CategoryArchitectureType = CategoryArchitecture.Name?.ToLower() ?? string.Empty;

        var allProjects = projectrepository
            .FindBy(e => e.InActive != true && e.DeletedAt == null)
            .Include(p => p.Images)
            .ToList();

        if (allProjects == null)
        {
            return NotFound("No projects found.");
        }

        var projects = allProjects
            .Where(p => p.ProjectType != null && p.ProjectType.Contains(CategoryArchitectureType, StringComparison.OrdinalIgnoreCase))
            .Select(s => new GetCategoryArchitectureByArchitectureResponse
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

        var CategoryArchitectureProjectIds = repository
            .FindBy(e => e.CategoryArchitectureId == id)
            .Select(s => s.ProjectId)
            .ToList() ?? [];

        if (CategoryArchitectureProjectIds == null)
        {
            return NotFound("CategoryArchitecture project IDs not found.");
        }

        foreach (var tag in projects)
        {
            tag.Checked = CategoryArchitectureProjectIds.Contains(tag.ProjectId);
        }

        return Ok(projects);
    }
}