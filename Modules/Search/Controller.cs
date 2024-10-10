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
    public IActionResult GetByCategorySearchId([FromQuery] string? ProjectName, int pageNumber = 1, int pageSize = 10)
    {
        if (string.IsNullOrWhiteSpace(ProjectName))
        {
            return Ok(new List<GetSearchByProjectResponse>());
        }

        var allProjects = projectrepository
            .FindBy(e => e.InActive != true && e.DeletedAt == null)
            .Include(p => p.Images)
            .ToList();

        if (allProjects == null || allProjects.Count == 0)
        {
            return Ok(new List<GetSearchByProjectResponse>());
        }

        var filteredProjects = allProjects
            .Where(p => p.ProjectName.Contains(ProjectName, StringComparison.OrdinalIgnoreCase));

        var pagedProjects = filteredProjects
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
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
                    }).ToList() ?? []
                }
            })
            .ToList();

        return Ok(pagedProjects);
    }


}