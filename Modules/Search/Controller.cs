using ArchtistStudio.Core;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Modules.Project;
using Microsoft.EntityFrameworkCore;
using ArchtistStudio.Modules.Image;

namespace ArchtistStudio.Modules.Search;


public class SearchController(
    IProjectRepository projectrepository) : MyAdminController
{
    [HttpGet("project")]
    public IActionResult GetByCategorySearchId([FromQuery] string? ProjectName)
    {
        var allProjects = projectrepository
            .FindBy(e => e.InActive != true && e.DeletedAt == null)
            .Include(p => p.Images)
            .ToList();

        if (allProjects == null || allProjects.Count == 0)
        {
            return Ok(new List<GetSearchByProjectResponse>());
        }

        var projects = allProjects
            .Where(p => string.IsNullOrEmpty(ProjectName) ||
                        p.ProjectName.Contains(ProjectName, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(p => p.ProjectName.Contains(ProjectName ?? string.Empty, StringComparison.OrdinalIgnoreCase))
            .Select(s => new GetSearchByProjectResponse
            {
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
                    }).ToList() ?? new List<DatailImageResponse>()
                },
            })
            .ToList();

        return Ok(projects);
    }

}