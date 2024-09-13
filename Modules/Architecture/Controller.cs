using ArchtistStudio.Core;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Modules.Project;
using ArchtistStudio.Modules.Category;
using Microsoft.EntityFrameworkCore;

namespace ArchtistStudio.Modules.Architecture;


public class ArchitectureController(
    IArchitectureRepository repository,
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
              .Select(s => new GetCategoryByArchitectureResponse
              {
                  ProjectId = s.Id,
                  Project = s,
              })
          .ToList();

        return Ok(projects);
    }

    [HttpGet("Project/{id:guid}")]
    public IActionResult GetByCategoryId(Guid id, Guid projectId)
    {

        var category = categoryrepository.FindBy(c => c.Id == id).FirstOrDefault();
        if (category == null) return ItemNotFound();
        var categoryType = category.Name.ToLower();

        var allProjects = projectrepository
            .FindBy(e => e.InActive != true && e.DeletedAt == null)
            .ToList();


        var filteredProjects = allProjects
            .Where(p => p.Id == projectId && p.ProjectType.Contains(categoryType, StringComparison.OrdinalIgnoreCase))
            .Select(s => new GetCategoryByArchitectureResponse
            {
                ProjectId = s.Id,
                Project = s,
            }).ToList();

        var categoryProjectIds = repository
            .FindBy(e => e.CategoryId == id)
            .Select(s => s.ProjectId)
            .ToList();

        foreach (var tag in filteredProjects)
        {
            tag.Checked = categoryProjectIds.Contains(tag.ProjectId);
        }

        return Ok(filteredProjects);
    }




    [HttpGet("Category/{id:guid}")]
    public IActionResult GetByCategoryId(Guid id, [FromQuery] string? projectName)
    {
        var category = categoryrepository.FindBy(c => c.Id == id).FirstOrDefault();
        if (category == null) return ItemNotFound();
        var categoryType = category.Name.ToLower();

        var allprojects = projectrepository
            .FindBy(e => e.InActive != true && e.DeletedAt == null)
            .ToList();

        var projects = allprojects
          .Where(p => (string.IsNullOrEmpty(projectName) ||
                     p.ProjectName.Contains(projectName, StringComparison.OrdinalIgnoreCase)) &&
                    p.ProjectType.Contains(categoryType, StringComparison.OrdinalIgnoreCase))
            .Select(s => new GetCategoryByArchitectureResponse
            {
                ProjectId = s.Id,
                Project = s,
            }).ToList();

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