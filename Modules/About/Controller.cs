using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;

namespace ArchtistStudio.Modules.About;


public class AboutController(
    IFileUploadService fileUploadService,
    IMapper mapper,
    IAboutRepository repository) : MyController
{
    // === Gets ====//
    [HttpGet]
    public IActionResult Gets()
    {
        var Queryable = repository.GetSingle(e => e.DeletedAt == null);
        if (Queryable == null)
        {
            return RedirectToAction("insert");
        }
        var results = Queryable == null ? null : mapper.Map<DetailAboutResponse>(Queryable);
        return View(results);
    }

    // === Post ====//
    public IActionResult Insert()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Insert([FromForm] InsertAboutRequest request)
    {
        var Queryable = repository.GetSingle(e => e.DeletedAt == null);
        if (Queryable != null)
        {
            TempData["Message"] = "Data is available. Redirecting to update.";
            return RedirectToAction("error", "error");
        }
        if (request.ImagePath == null || request.ImagePath.Length == 0)
        {
            ModelState.AddModelError("Image", "Image file is required.");
            return View(request);
        }
        string Image = fileUploadService.UploadFileAsync(request.ImagePath, "image");

        var item = mapper.Map<About>(request);
        item.ImagePath = Image;
        item.CreatedAt = DateTime.UtcNow;
        item.CreatedBy = Guid.NewGuid();

        repository.Add(item);
        repository.Commit();

        return RedirectToAction("gets");
    }

    // === Update ====//
    [HttpGet]
    public ActionResult Update(Guid id)
    {
        var iQueryable = repository.GetSingle(e => e.Id == id && e.DeletedAt == null);
        if (iQueryable == null) return View();

        var results = mapper.Map<UpdateAboutRequest>(iQueryable);
        return View(results);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(Guid id, UpdateAboutRequest request)
    {

        var item = repository.GetSingle(e => e.Id == id && e.DeletedAt == null);
        if (item == null) return NotFound();

        if (request.ImagePath != null && request.ImagePath.Length > 0)
        {
            string Image = fileUploadService.UploadFileAsync(request.ImagePath, "image");
            item.ImagePath = Image;
        }

        item.Expert = request.Expert ?? item.Expert;
        item.Service = request.Service ?? item.Service;
        item.ChooseUs = request.ChooseUs ?? item.ChooseUs;
        item.Construction = request.Construction ?? item.Construction;
        item.UpdatedAt = DateTime.UtcNow;

        repository.Update(item);
        repository.Commit();

        return RedirectToAction("gets");
    }
        public IActionResult Error()
    {
        return View();
    }
}


public class ApiAboutController(
    IMapper mapper,
    IAboutRepository repository) : MyAdminController
{
    [HttpGet]
    public IActionResult Gets()
    {
        var iQueryable = repository.FindBy(e => e.DeletedAt == null)
            .AsNoTracking();
        var results = mapper.ProjectTo<ListAboutResponse>(iQueryable).ToList();
        return Ok(results);
    }
}