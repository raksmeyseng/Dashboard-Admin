using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;

namespace ArchtistStudio.Modules.TopManagement;


public class TopManagementController(
    IMapper mapper,
    ITopManagementRepository repository,
    IFileUploadService fileUploadService) : MyController
{
    // === Post ====//
    public IActionResult Insert()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Insert([FromForm] InsertTopManagementRequest request)
    {
        if (request.ImagePath == null || request.ImagePath.Length == 0)
        {
            ModelState.AddModelError("Image", "Image file is required.");
            return View(request);
        }
        string Image = fileUploadService.UploadFileAsync(request.ImagePath, "image");

        var item = mapper.Map<TopManagement>(request);
        item.ImagePath = Image;
        item.CreatedAt = DateTime.UtcNow;
        item.CreatedBy = Guid.NewGuid();

        repository.Add(item);
        repository.Commit();

        return RedirectToAction("profile", "contact");
    }

    // === Update ====//
    [HttpGet]
    public ActionResult Update(Guid id)
    {
        var iQueryable = repository.GetSingle(e => e.Id == id && e.DeletedAt == null);
        if (iQueryable == null) return View();

        var results = mapper.Map<UpdateTopManagementRequest>(iQueryable);
        return View(results);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(Guid id, UpdateTopManagementRequest request)
    {

        var item = repository.GetSingle(e => e.Id == id && e.DeletedAt == null);
        if (item == null) return NotFound();

        if (request.ImagePath != null && request.ImagePath.Length > 0)
        {
            string Image = fileUploadService.UploadFileAsync(request.ImagePath, "image");
            item.ImagePath = Image;
        }

        item.Name = request.Name ?? item.Name;
        item.Description = request.Description ?? item.Description;
        item.UpdatedAt = DateTime.UtcNow;

        repository.Update(item);
        repository.Commit();

        return RedirectToAction("profile", "contact");
    }

    // === Delete ====//
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

         return RedirectToAction("profile", "contact");
    }

}

public class ApiTopManagementController(
    IMapper mapper, 
    ITopManagementRepository repository) : MyAdminController
{
    [HttpGet]
    public IActionResult Gets()
    {
        var iQueryable = repository.FindBy(e => e.DeletedAt == null)
            .AsNoTracking();
        var results = mapper.ProjectTo<ListTopManagementResponse>(iQueryable).ToList();
        return Ok(results);
    }
}