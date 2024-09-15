using ArchtistStudio.Core;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Modules.Project;
using ArchtistStudio.Modules.Category;
using Microsoft.EntityFrameworkCore;
using ArchtistStudio.Modules.Image;


namespace ArchtistStudio.Modules.Engineeing;


public class EngineeingController(
    IEngineeingRepository repository,
    IProjectRepository projectrepository,
    ICategoryRepository categoryrepository
    ) : MyAdminController
{

    [HttpGet("all/project")]
    public IActionResult Gets()
    {

        var projects = projectrepository
            .FindBy(e => e.DeletedAt == null)
            .AsNoTracking()
            .Include(p => p.Images)
            .Select(s => new GetCategoryByEngineeingResponse
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
    public IActionResult GetByCategoryId(Guid id, Guid projectId)
    {
        var category = categoryrepository.FindBy(c => c.Id == id).FirstOrDefault();
        if (category == null)
        {
            return ItemNotFound();
        }

        var categoryType = category.Name?.ToLower() ?? string.Empty;

        var allProjects = projectrepository
            .FindBy(e => e.InActive != true && e.DeletedAt == null)
            .Include(p => p.Images) // Ensure images are included
            .ToList();

        if (allProjects == null)
        {
            return NotFound("No projects found.");
        }

        var filteredProjects = allProjects
            .Where(p => p.Id == projectId && p.ProjectType != null && p.ProjectType.Contains(categoryType, StringComparison.OrdinalIgnoreCase))
            .Select(s => new GetCategoryByEngineeingResponse
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

        var categoryProjectIds = repository
            .FindBy(e => e.CategoryId == id)
            .Select(s => s.ProjectId)
            .ToList() ?? [];

        if (categoryProjectIds == null)
        {
            return NotFound("Category project IDs not found.");
        }

        foreach (var tag in filteredProjects)
        {
            tag.Checked = categoryProjectIds.Contains(tag.ProjectId);
        }

        return Ok(filteredProjects);
    }

    [HttpGet("Category/{id:guid}")]
    public IActionResult GetByCategoryId(Guid id)
    {
        var category = categoryrepository.FindBy(c => c.Id == id).FirstOrDefault();
        if (category == null) return ItemNotFound();
        var categoryType = category.Name.ToLower();

        var allprojects = projectrepository
            .FindBy(e => e.InActive != true && e.DeletedAt == null)
            .ToList();

        var projects = allprojects
          .Where(p => p.ProjectType.Contains(categoryType, StringComparison.OrdinalIgnoreCase))
           .Select(s => new GetCategoryByEngineeingResponse
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

        var ids = repository
            .FindBy(e => e.CategoryId == id)
            .Select(s => s.ProjectId)
            .ToList();

        foreach (var tag in projects)
        {
            tag.Checked = ids.Contains(tag.ProjectId);
        }
        return Ok(projects);
    }
}