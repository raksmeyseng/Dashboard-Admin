using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;

namespace ArchtistStudio.Modules.Recommend;

public class RecommendController( IRecommendRepository repository) : MyController
{
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

public class ApiRecommendController(
    IMapper mapper, 
    IRecommendRepository repository) : MyAdminController
{
    // === Gets ====//
    [HttpGet]
    public IActionResult Gets()
    {
        var iQueryable = repository.FindBy(e => e.DeletedAt == null)
            .AsNoTracking();
        var results = mapper.ProjectTo<ListRecommendResponse>(iQueryable).ToList();
        return Ok(results);
    }
     // === Post ====//
    [HttpPost]
    public IActionResult Insert([FromBody] InsertRecommendRequest request)
    {
        var item = mapper.Map<Recommend>(request);
        item.CreatedAt = DateTime.UtcNow;
        item.CreatedBy = new Guid();
        repository.Add(item);
        repository.Commit();
        return Created();
    }
}