using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using ArchtistStudio.Modules.Social;
using ArchtistStudio.Modules.About;


namespace ArchtistStudio.Modules.Contact;


public class ContactController(
    IService imageUploadService,
    IMapper mapper,
    IAboutRepository aboutRepository,
    ISocialRepository socialRepository,
    IContactRepository repository

    ) : MyController
{

    [HttpGet]
    public IActionResult Profile()
    {
        var contactEntity = repository.GetSingle(e => e.DeletedAt == null);
        var contactLink = contactEntity == null ? null : mapper.Map<ListContactResponse>(contactEntity);

        var socialQueryable = socialRepository.FindBy(e => e.DeletedAt == null).AsNoTracking();
        var socialLinks = mapper.ProjectTo<DetailSocialResponse>(socialQueryable).ToList();

        var aboutQueryable = aboutRepository.GetSingle(e => e.DeletedAt == null);
        var aboutLink = contactEntity == null ? null : mapper.Map<ListAboutResponse>(aboutQueryable);

        var model = new Tuple<ListContactResponse, List<DetailSocialResponse>,  ListAboutResponse>(
            contactLink ?? new ListContactResponse(),
            socialLinks ?? [],
            aboutLink ?? new ListAboutResponse()
        );

        return View(model);
    }

    public IActionResult Insert()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Insert([FromForm] InsertContactRequest request)
    {
        if (request.ImagePath == null || request.ImagePath.Length == 0)
        {
            ModelState.AddModelError("Image", "Image file is required.");
            return View(request);
        }
        string imagePath = await imageUploadService.UploadImageAsync(request.ImagePath);

        var item = mapper.Map<Contact>(request);
        item.ImagePath = imagePath;
        item.CreatedAt = DateTime.UtcNow;
        item.CreatedBy = Guid.NewGuid();

        repository.Add(item);
        repository.Commit();

        return RedirectToAction("profile");
    }

    // === Update Contact === //
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
    public async Task<IActionResult> Update(Guid id, UpdateContactRequest request)
    {

        var iQueryable = repository.GetSingle(e => e.Id == id && e.DeletedAt == null);
        if (iQueryable == null) return NotFound();

        iQueryable.Name = request.Name ?? iQueryable.Name;
        iQueryable.PhoneNumber = request.PhoneNumber ?? iQueryable.PhoneNumber;
        iQueryable.Email = request.Email ?? iQueryable.Email;
        iQueryable.Purpose = request.Purpose ?? iQueryable.Purpose;
        iQueryable.Message = request.Message ?? iQueryable.Message;
        iQueryable.UpdatedAt = DateTime.UtcNow;

        if (request.ImagePath != null && request.ImagePath.Length > 0)
        {
            string imagePath = await imageUploadService.UploadImageAsync(request.ImagePath);
            iQueryable.ImagePath = imagePath;
        }

        repository.Update(iQueryable);
        repository.Commit();

        return RedirectToAction("profile");
    }

}