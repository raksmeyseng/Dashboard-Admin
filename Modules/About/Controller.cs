using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;

namespace ArchtistStudio.Modules.About;


public class AboutController(
    IService imageUploadService,
    IMapper mapper,
    IAboutRepository repository) : MyController
{
    // === Post ====//
     public IActionResult Insert()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Insert([FromForm] InsertAboutRequest request)
    {
        if (request.ImagePath == null || request.ImagePath.Length == 0)
        {
            ModelState.AddModelError("Image", "Image file is required.");
            return View(request);
        }
        string imagePath = await imageUploadService.UploadImageAsync(request.ImagePath);

        var item = mapper.Map<About>(request);
        item.ImagePath = imagePath;
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

        var results = mapper.Map<UpdateAboutRequest>(iQueryable);
        return View(results);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Guid id, UpdateAboutRequest request)
    {

        var iQueryable = repository.GetSingle(e => e.Id == id && e.DeletedAt == null);
        if (iQueryable == null) return NotFound();

        iQueryable.Expert = request.Expert ?? iQueryable.Expert;
        iQueryable.Service = request.Service ?? iQueryable.Service;
        iQueryable.ChooseUs = request.ChooseUs ?? iQueryable.ChooseUs;
        iQueryable.Construction = request.Construction ?? iQueryable.Construction;
        iQueryable.UpdatedAt = DateTime.UtcNow;

        if (request.ImagePath != null && request.ImagePath.Length > 0)
        {
            string imagePath = await imageUploadService.UploadImageAsync(request.ImagePath);
            iQueryable.ImagePath = imagePath;
        }

        repository.Update(iQueryable);
        repository.Commit();

     return RedirectToAction("profile", "contact");
    }

    // === Delete ====//
    [HttpDelete("{id:guid}")]
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
        return NoContent();
    }

}