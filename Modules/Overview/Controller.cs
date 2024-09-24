using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;

namespace ArchtistStudio.Modules.Overview;


public class OverviewController(
    IMapper mapper,
    IFileUploadService fileUploadService,
    IOverviewRepository repository) : MyController
{
    // === Gets ====//
    [HttpGet]
    public IActionResult Gets()
    {
        var iQueryable = repository.FindBy(e => e.DeletedAt == null)
            .AsNoTracking();
        var results = mapper.ProjectTo<ListOverviewResponse>(iQueryable).ToList();
        return View(results);
    }

    // === Post ====//
    public IActionResult Insert()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Insert([FromForm] InsertOverviewRequest request)
    {
        if (request.ImagePath == null || request.ImagePath.Length == 0)
        {
            ModelState.AddModelError("ImagePath", "Image file is required.");
            return View(request);
        }
        string Image = fileUploadService.UploadFileAsync(request.ImagePath);


        var item = mapper.Map<Overview>(request);
        item.ImagePath = Image;
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

        var results = mapper.Map<UpdateOverviewRequest>(iQueryable);
        return View(results);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(Guid id, UpdateOverviewRequest request)
    {
        var item = repository.GetSingle(e => e.Id == id && e.DeletedAt == null);
        if (item == null) return NotFound();

        if (request.ImagePath != null && request.ImagePath.Length > 0)
        {
            string Image = fileUploadService.UploadFileAsync(request.ImagePath);
            item.ImagePath = Image;
        }

        item.Description = request.Description ?? item.Description;
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

public class ApiOverviewController(
    IMapper mapper,
    IOverviewRepository repository) : MyAdminController
{
    [HttpGet]
    public IActionResult Gets()
    {
        var iQueryable = repository.FindBy(e => e.DeletedAt == null)
            .AsNoTracking();
        var results = mapper.ProjectTo<ListOverviewResponse>(iQueryable).ToList();
        return Ok(results);
    }
}