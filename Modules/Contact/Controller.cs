using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using ArchtistStudio.Modules.Social;
using ArchtistStudio.Modules.PhoneNumber;
using ArchtistStudio.Modules.Email;


namespace ArchtistStudio.Modules.Contact;


public class ContactController(
    IMapper mapper,
    ISocialRepository socialRepository,
    IPhoneNumberRepository phoneNumberRepository,
    IEmailRepository emailRepository,
    IContactRepository repository) : MyController
{
    // === Gets ====//
    [HttpGet]
    public IActionResult Profile()
    {
        var contactQueryable = repository.GetSingle(e => e.DeletedAt == null);
        var contactLink = contactQueryable == null ? null : mapper.Map<DetailContactResponse>(contactQueryable);

        var socialQueryable = socialRepository.FindBy(e => e.DeletedAt == null).AsNoTracking();
        var socialLinks = mapper.ProjectTo<ListSocialResponse>(socialQueryable).ToList();
        var phoneNumberQueryable = phoneNumberRepository.FindBy(e => e.DeletedAt == null).AsNoTracking();
        var phoneNumberLinks = mapper.ProjectTo<ListPhoneNumberResponse>(phoneNumberQueryable).ToList();
        var emailQueryable = emailRepository.FindBy(e => e.DeletedAt == null).AsNoTracking();
        var emailLinks = mapper.ProjectTo<ListEmailResponse>(emailQueryable).ToList();
        var response = new Tuple<DetailContactResponse, List<ListSocialResponse>,List<ListPhoneNumberResponse>,List<ListEmailResponse>>(
            contactLink ?? new DetailContactResponse(),
            socialLinks ?? [],
            phoneNumberLinks ?? [],
            emailLinks ?? []
        );

        return View(response);
    }

    // === Post ====//
    public IActionResult Insert()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Insert([FromForm] InsertContactRequest request)
    {
        var Queryable = repository.GetSingle(e => e.DeletedAt == null);
        if (Queryable != null)
        {
            TempData["Message"] = "Data is available. Redirecting to update.";
            return RedirectToAction("error", "error");
        }

        var item = mapper.Map<Contact>(request);
        item.CreatedAt = DateTime.UtcNow;
        item.CreatedBy = Guid.NewGuid();

        repository.Add(item);
        repository.Commit();

        return RedirectToAction("profile");
    }

    // === Update === //
    [HttpGet]
    public ActionResult Update(Guid id)
    {
        var iQueryable = repository.GetSingle(e => e.Id == id && e.DeletedAt == null);
        if (iQueryable == null) return View();

        var results = mapper.Map<UpdateContactRequest>(iQueryable);
        return View(results);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(Guid id, UpdateContactRequest request)
    {
        // if(ModelState.IsValid){
        //     return View(request);
        // }

        var item = repository.GetSingle(e => e.Id == id && e.DeletedAt == null);
        if (item == null)
        {
              TempData["Message"] = "Data is available. Redirecting to insert.";
            return RedirectToAction("error", "error");
        }
        item.Location = request.Location ?? item.Location;
        item.UpdatedAt = DateTime.UtcNow;

        repository.Update(item);
        repository.Commit();

        return RedirectToAction("profile");
    }

}

public class ApiContactController(
    IMapper mapper,
    IContactRepository repository) : MyAdminController
{
    // === Gets ====//
    [HttpGet]
    public IActionResult Gets()
    {
        var iQueryable = repository.FindBy(e => e.DeletedAt == null)
            .AsNoTracking();
        var results = mapper.ProjectTo<ListContactResponse>(iQueryable).FirstOrDefault();
        return Ok(results);
    }
}