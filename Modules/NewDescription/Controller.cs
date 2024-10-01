using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;

namespace ArchtistStudio.Modules.NewDescription;

public class NewDescriptionController(
    IMapper mapper,
    INewDescriptionRepository repository) : MyController
{
    // === Gets ====//
    [HttpGet]
    public IActionResult Gets()
    {
        var iQueryable = repository.FindBy(e => e.DeletedAt == null)
            .AsNoTracking();
        var results = mapper.ProjectTo<ListNewDescriptionResponse>(iQueryable).FirstOrDefault();
        return View(results);
    }

    // === Post ====//
    public IActionResult Insert()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Insert([FromForm] InsertNewDescriptionRequest request)
    {
         var Queryable = repository.GetSingle(e => e.DeletedAt == null);
         if (Queryable != null)
        {
            TempData["Message"] = "Data is available. Redirecting to update.";
            return RedirectToAction("error", "error");
        }

        var item = mapper.Map<NewDescription>(request);
        item.CreatedAt = DateTime.UtcNow;
        item.CreatedBy = Guid.NewGuid();
        repository.Add(item);
        repository.Commit();

        return RedirectToAction("gets", "new");
    }

    // === Update === //
    [HttpGet]
    public ActionResult Update(Guid id)
    {
        var iQueryable = repository.GetSingle(e => e.Id == id && e.DeletedAt == null);
        if (iQueryable == null) return View();

        var results = mapper.Map<UpdateNewDescriptionRequest>(iQueryable);
        return View(results);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(Guid id, UpdateNewDescriptionRequest request)
    {
        var item = repository.GetSingle(e => e.Id == id && e.DeletedAt == null);
        // if (item != null)
        // {
        //     TempData["Message"] = "Data not found. Redirecting to insert.";
        //     return RedirectToAction("error", "error");
        // }
        if(item == null) 
        {
            TempData["Message"] = "Data not found. Redirecting to insert.";
            return RedirectToAction("error", "error");
        }
        item.Description = request.Description ?? item.Description;
        item.UpdatedAt = DateTime.UtcNow;

        repository.Update(item);
        repository.Commit();

          return RedirectToAction("gets", "new");
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

           return RedirectToAction("gets", "new");
    }

}


public class ApiNewDescriptionController(
    IMapper mapper,
    INewDescriptionRepository repository) : MyAdminController
{
    [HttpGet]
    public IActionResult Gets()
    {
        var iQueryable = repository.FindBy(e => e.DeletedAt == null)
            .AsNoTracking();
        var results = mapper.ProjectTo<ListNewDescriptionResponse>(iQueryable).FirstOrDefault();
        return Ok(results);
    }
}