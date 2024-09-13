using AutoMapper;
using ArchtistStudio.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArchtistStudio.Modules.Social;


public class SocialController(
    IMapper mapper,
    ISocialRepository repository) : MyController
{
    // === Post ====//
    public IActionResult Insert()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Insert([FromForm] InsertSocialRequest request)
    {
         if (!ModelState.IsValid)
        {
            return View(request);
        }
        var item = mapper.Map<Social>(request);
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

        var results = mapper.Map<UpdateSocialRequest>(iQueryable);
        return View(results);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(Guid id, UpdateSocialRequest request)
    {
        var item = repository.GetSingle(e => e.Id == id && e.DeletedAt == null);
        if (item == null) return NotFound();


        item.Platform = request.Platform ?? item.Platform;
        item.DisplayText = request.DisplayText ?? item.DisplayText;
        item.URL = request.URL ?? item.URL;
        item.UpdatedAt = DateTime.UtcNow;

        repository.Update(item);
        repository.Commit();

        return RedirectToAction("profile", "contact");
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

        return RedirectToAction("profile", "contact");
    }
}

public class ApiSocialController(
    IMapper mapper, 
    ISocialRepository repository) : MyAdminController
{
    [HttpGet]
    public IActionResult Gets()
    {
        var iQueryable = repository.FindBy(e => e.DeletedAt == null)
            .AsNoTracking();
        var results = mapper.ProjectTo<ListSocialResponse>(iQueryable).ToList();
        return Ok(results);
    }
}