using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using ArchtistStudio.Modules.Social;
using ArchtistStudio.Modules.About;
using ArchtistStudio.Modules.TopManagement;
using ArchtistStudio.Modules.History;
using ArchtistStudio.Modules.Recommend;
using ArchtistStudio.Modules.PhoneNumber;
using ArchtistStudio.Modules.Email;


namespace ArchtistStudio.Modules.Contact;


public class ContactController(
    IMapper mapper,
    ISocialRepository socialRepository,
    IHistoryRepository historyRepository,
    ITopManagementRepository topManagementRepository,
    IPhoneNumberRepository phoneNumberRepository,
    IEmailRepository emailRepository,
    IContactRepository repository,
    IRecommendRepository recommendRepository) : MyController
{
    // === Gets ====//
    [HttpGet]
    public IActionResult Profile()
    {
        var contactQueryable = repository.GetSingle(e => e.DeletedAt == null);
        var contactLink = contactQueryable == null ? null : mapper.Map<DetailContactResponse>(contactQueryable);

        var socialQueryable = socialRepository.FindBy(e => e.DeletedAt == null).AsNoTracking();
        var socialLinks = mapper.ProjectTo<ListSocialResponse>(socialQueryable).ToList();

        var topmanagemetnQueryable = topManagementRepository.FindBy(e => e.DeletedAt == null).AsNoTracking();
        var topmanagemetnLinks = mapper.ProjectTo<ListTopManagementResponse>(topmanagemetnQueryable).ToList();

        var phoneNumberQueryable = phoneNumberRepository.FindBy(e => e.DeletedAt == null).AsNoTracking();
        var phoneNumberLinks = mapper.ProjectTo<ListPhoneNumberResponse>(phoneNumberQueryable).ToList();

        var emailQueryable = emailRepository.FindBy(e => e.DeletedAt == null).AsNoTracking();
        var emailLinks = mapper.ProjectTo<ListEmailResponse>(emailQueryable).ToList();

        var historyQueryable = historyRepository.FindBy(e => e.DeletedAt == null).AsNoTracking();
        var historyLinks = mapper.ProjectTo<ListHistoryResponse>(historyQueryable).ToList();

        var recommendiQueryable = recommendRepository.FindBy(e => e.DeletedAt == null).AsNoTracking();
        var recommendLinks = mapper.ProjectTo<ListRecommendResponse>(recommendiQueryable).ToList();

        var response = new Tuple<DetailContactResponse, List<ListSocialResponse>, List<ListTopManagementResponse>,List<ListPhoneNumberResponse>,List<ListEmailResponse>, List<ListHistoryResponse>, List<ListRecommendResponse>>(
            contactLink ?? new DetailContactResponse(),
            socialLinks ?? [],
            topmanagemetnLinks ?? [],
            phoneNumberLinks ?? [],
            emailLinks ?? [],
            historyLinks ?? [],
            recommendLinks ?? []
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