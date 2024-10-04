using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;

namespace ArchtistStudio.Modules.PhoneNumber;


public class PhoneNumberController(
    IMapper mapper,
    IPhoneNumberRepository repository) : MyController
{

    // === Post ====//
    public IActionResult Insert()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Insert([FromForm] InsertPhoneNumberRequest request)
    {
         if(!ModelState.IsValid){
            return View(request);
        }
        var item = mapper.Map<PhoneNumber>(request);
        item.CreatedAt = DateTime.UtcNow;
        item.CreatedBy = Guid.NewGuid();

        repository.Add(item);
        repository.Commit();

      return RedirectToAction("profile", "contact");
    }

    // === Update === //
    [HttpGet]
    public ActionResult Update(Guid id)
    {
        var iQueryable = repository.GetSingle(e => e.Id == id && e.DeletedAt == null);
        if (iQueryable == null) return View();

        var results = mapper.Map<UpdatePhoneNumberRequest>(iQueryable);
        return View(results);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(Guid id, UpdatePhoneNumberRequest request)
    {
        if(!ModelState.IsValid){
            return View(request);
        }

        var item = repository.GetSingle(e => e.Id == id && e.DeletedAt == null);
        if (item == null)
        {
            return RedirectToAction("insert");
        }
        item.Phone = request.Phone ?? item.Phone;

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

public class ApiPhoneNumberController(
    IMapper mapper,
    IPhoneNumberRepository repository) : MyAdminController
{
    // === Gets ====//
    [HttpGet]
    public IActionResult Gets()
    {
        var iQueryable = repository.FindBy(e => e.DeletedAt == null)
            .AsNoTracking();
        var results = mapper.ProjectTo<ListPhoneNumberResponse>(iQueryable).FirstOrDefault();
        return Ok(results);
    }
}