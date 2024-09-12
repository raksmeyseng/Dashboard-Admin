using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;

namespace ArchtistStudio.Modules.History;


public class HistoryController(
    IMapper mapper,
    IHistoryRepository repository) : MyController
{
     // === Post ====//
    public IActionResult Insert()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Insert([FromForm] InsertHistoryRequest request)
    {
        var item = mapper.Map<History>(request);
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

        var results = mapper.Map<UpdateHistoryRequest>(iQueryable);
        return View(results);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(Guid id, UpdateHistoryRequest request)
    {

        var item = repository.GetSingle(e => e.Id == id && e.DeletedAt == null);
        if (item == null) return NotFound();
        item.Name = request.Name ?? item.Name;
        item.Description = request.Description ?? item.Description;
        item.UpdatedAt = DateTime.UtcNow;

        repository.Update(item);
        repository.Commit();

        return RedirectToAction("profile", "contact");
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

       return RedirectToAction("profile", "contact");
    }

}