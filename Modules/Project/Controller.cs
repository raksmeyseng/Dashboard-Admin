using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ArchtistStudio.Modules.Project;


public class ProjectController(
    IService imageUploadService,
    IMapper mapper,
    IProjectRepository repository) : MyController
{
    // === Get All ====//
    [HttpGet]
    public IActionResult Gets()
    {
        var iQueryable = repository.FindBy(e => e.DeletedAt == null)
            .AsNoTracking();
        var results = mapper.ProjectTo<ListProjectResponse>(iQueryable).ToList();
        return View(results);
    }


    // === Post ====//
    public IActionResult Insert()
    {
        return View();
    }
   [HttpPost]
    public async Task<IActionResult> Insert([FromForm] InsertProjectRequest request)
    {
        if (request.ImagePath == null || request.ImagePath .Length == 0)
        {
            ModelState.AddModelError("Image", "Image file is required.");
            return View(request);
        }

        if (!ModelState.IsValid)
        {
            return View(request);
        }

        string imagePath = await imageUploadService.UploadImageAsync(request.ImagePath);
        var item = mapper.Map<Project>(request);
        item.ImagePath = imagePath;
        item.CreatedAt = DateTime.UtcNow;
        item.CreatedBy = Guid.NewGuid();

        repository.Add(item);
        repository.Commit();

        return RedirectToAction("gets"); 
    }


    // === Update Project === //
    [HttpGet]
    public ActionResult Update(Guid id)
    {
        var iQueryable = repository.GetSingle(e => e.Id == id && e.DeletedAt == null);
        if (iQueryable == null) return View();

        var results = mapper.Map<UpdateProjectRequest>(iQueryable);
        return View(results);
    }
     [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Guid id, UpdateProjectRequest request)
    {
        var iQueryable = repository.GetSingle(e => e.Id == id && e.DeletedAt == null);
        if (iQueryable == null) return NotFound();

        iQueryable.ProjectName = request.ProjectName ?? iQueryable.ProjectName;
        iQueryable.Location = request.Location ?? iQueryable.Location;
        iQueryable.Description = request.Description ?? iQueryable.Description;
        iQueryable.ProjectType = request.ProjectType ?? iQueryable.ProjectType;
        iQueryable.InActive = request.InActive ?? iQueryable.InActive;
        iQueryable.UpdatedAt = DateTime.UtcNow;

        if (request.ImagePath != null && request.ImagePath.Length > 0)
        {
            string imagePath = await imageUploadService.UploadImageAsync(request.ImagePath);
            iQueryable.ImagePath = imagePath; 
        }
        repository.Update(iQueryable);
        repository.Commit();

        return RedirectToAction("gets"); 
    }

    public IActionResult Delete()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Guid id)
    {
        var item = repository.GetSingle(e =>
            e.Id == id &&
            e.DeletedAt == null
        );
        if (item == null)
        {
            return BadRequest("Item Not Found");
        }

        item.DeletedAt = DateTime.UtcNow;
        repository.Remove(item);
        repository.Commit();

        return RedirectToAction("gets");
    }


}

