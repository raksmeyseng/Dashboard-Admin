﻿using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Core;
using ArchtistStudio.Modules.Project;
using ArchtistStudio.Modules.Category;
using Microsoft.EntityFrameworkCore;
using ArchtistStudio.Modules.Image;

namespace ArchtistStudio.Modules.Product;


public class ProductController(
    IProductRepository repository,
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
            .Select(s => new GetCategoryByProductResponse
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
                    Images = s.Images.Select(img => new ListImageResponse
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
        if (category == null) return ItemNotFound();
        var categoryType = category.Name.ToLower();

        var allProjects = projectrepository
            .FindBy(e => e.InActive != true && e.DeletedAt == null)
            .ToList();


        var filteredProjects = allProjects
            .Where(p => p.Id == projectId && p.ProjectType.Contains(categoryType, StringComparison.OrdinalIgnoreCase))
            .Select(s => new GetCategoryByProductResponse
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
                    Images = s.Images.Select(img => new ListImageResponse
                    {
                        ImagePath = img.ImagePath,
                        Description = img.Description
                    }).ToList(),
                }
            })
        .ToList();

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
           .Select(s => new GetCategoryByProductResponse
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
                   Images = s.Images.Select(img => new ListImageResponse
                   {
                       ImagePath = img.ImagePath,
                       Description = img.Description
                   }).ToList(),
               }
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