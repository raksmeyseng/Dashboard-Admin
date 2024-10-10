using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;

namespace ArchtistStudio.Modules.Project;


public class ProjectController(
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
    public IActionResult Insert([FromForm] InsertProjectRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }

        var item = mapper.Map<Project>(request);
        item.CreatedAt = DateTime.UtcNow;
        item.CreatedBy = Guid.NewGuid();
        repository.Add(item);
        repository.Commit();

        return RedirectToAction("gets");
    }

    // === Update === //
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
    public IActionResult Update(Guid id, UpdateProjectRequest request)
    {
        var item = repository.GetSingle(e => e.Id == id && e.DeletedAt == null);
        if (item == null) return NotFound();

        item.ProjectType = request.ProjectType ?? item.ProjectType;
        item.ProjectName = request.ProjectName ?? item.ProjectName;
        item.Client = request.Client ?? item.Client;
        item.Size = request.Size ?? item.Size;
        item.Status = request.Status ?? item.Status;
        item.Location = request.Location ?? item.Location;
        item.InActive = request.InActive ?? item.InActive;
        item.UpdatedAt = DateTime.UtcNow;

        repository.Update(item);
        repository.Commit();

        return RedirectToAction("gets");
    }

    // === Delete === //
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

public class ApiProjectController(
    IMapper mapper,
    IProjectRepository repository) : MyAdminController
{
    [HttpGet]
    public IActionResult Gets(int pageNumber = 1, int pageSize = 10)
    {
        pageNumber = pageNumber < 1 ? 1 : pageNumber;
        var iQueryable = repository.FindBy(e => e.DeletedAt == null).AsNoTracking();
        var pagedData = iQueryable
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        var results = mapper.ProjectTo<ListProjectResponse>(pagedData).ToList();

        return Ok(results);
    }
}