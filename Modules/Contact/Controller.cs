﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using ArchtistStudio.Modules.Social;
using ArchtistStudio.Modules.About;
using ArchtistStudio.Modules.TopManagement;
using ArchtistStudio.Modules.History;


namespace ArchtistStudio.Modules.Contact;


public class ContactController(
    IMapper mapper,
    IAboutRepository aboutRepository,
    ISocialRepository socialRepository,
    IHistoryRepository historyRepository,
    ITopManagementRepository topManagementRepository,
    IContactRepository repository,
    IFileUploadService fileUploadService) : MyController
{
    // === Gets ====//
    [HttpGet]
    public IActionResult Profile()
    {
        var contactQueryable = repository.GetSingle(e => e.DeletedAt == null);
        var contactLink = contactQueryable == null ? null : mapper.Map<DetailContactResponse>(contactQueryable);

        var socialQueryable = socialRepository.FindBy(e => e.DeletedAt == null).AsNoTracking();
        var socialLinks = mapper.ProjectTo<ListSocialResponse>(socialQueryable).ToList();

        var aboutQueryable = aboutRepository.GetSingle(e => e.DeletedAt == null);
        var aboutLink = contactQueryable == null ? null : mapper.Map<ListAboutResponse>(aboutQueryable);

        var topmanagemetnQueryable = topManagementRepository.FindBy(e => e.DeletedAt == null).AsNoTracking();
        var topmanagemetnLinks = mapper.ProjectTo<ListTopManagementResponse>(topmanagemetnQueryable).ToList();

        var historyQueryable = historyRepository.FindBy(e => e.DeletedAt == null).AsNoTracking();
        var historyLinks = mapper.ProjectTo<ListHistoryResponse>(historyQueryable).ToList();

        var response = new Tuple<DetailContactResponse, List<ListSocialResponse>, ListAboutResponse, List<ListTopManagementResponse>, List<ListHistoryResponse>>(
            contactLink ?? new DetailContactResponse(),
            socialLinks ?? [],
            aboutLink ?? new ListAboutResponse(),
            topmanagemetnLinks ?? [],
            historyLinks ?? []
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
        if (request.ImagePath == null || request.ImagePath.Length == 0)
        {
            ModelState.AddModelError("Image", "Image file is required.");
            return View(request);
        }
        string Image = fileUploadService.UploadFileAsync(request.ImagePath, "contact/image");

        var item = mapper.Map<Contact>(request);
        item.ImagePath = Image;
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

        var item = repository.GetSingle(e => e.Id == id && e.DeletedAt == null);
        if (item == null) return NotFound();
        if (request.ImagePath != null && request.ImagePath.Length > 0)
        {
            string Image = fileUploadService.UploadFileAsync(request.ImagePath, "contact/image");
            item.ImagePath = Image;
        }

        item.Name = request.Name ?? item.Name;
        item.PhoneNumber = request.PhoneNumber ?? item.PhoneNumber;
        item.Email = request.Email ?? item.Email;
        item.Purpose = request.Purpose ?? item.Purpose;
        item.Message = request.Message ?? item.Message;
        item.UpdatedAt = DateTime.UtcNow;

        repository.Update(item);
        repository.Commit();

        return RedirectToAction("profile");
    }

}