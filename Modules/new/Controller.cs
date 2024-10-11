using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using ArchtistStudio.Modules.NewDescription;

namespace ArchtistStudio.Modules.New;

public class NewController(
    IFileUploadService fileUploadService,
    IMapper mapper,
    INewDescriptionRepository newDescriptionRepository,
    INewRepository repository) : MyController
{

    // === Gets ====//
    [HttpGet]
    public IActionResult Gets([FromQuery] string? Title, int pageNumber = 1, int pageSize = 10)
    {
        pageNumber = pageNumber < 1 ? 1 : pageNumber;

        var iQueryable = repository.FindBy(e => e.DeletedAt == null)
            .AsNoTracking();

        if (!string.IsNullOrEmpty(Title))
        {
            var searchTerm = Title.ToLower();
            iQueryable = iQueryable.Where(e => e.Title.ToLower().Contains(searchTerm));
        }

        var totalItems = iQueryable.Count();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        var pagedData = iQueryable
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var newLink = mapper.ProjectTo<ListNewResponse>(pagedData.AsQueryable()).ToList();
        var newDescriptionQueryable = newDescriptionRepository.GetSingle(e => e.DeletedAt == null);
        var newDescriptionLink = mapper.Map<ListNewDescriptionResponse>(newDescriptionQueryable);

        var response = new Tuple<List<ListNewResponse>, ListNewDescriptionResponse>(
            newLink ?? [],
            newDescriptionLink ?? new ListNewDescriptionResponse()
        );

        ViewBag.TotalPages = totalPages;
        ViewBag.CurrentPage = pageNumber;
        ViewBag.SearchQuery = Title;

        return View(response);
    }

    // === Post ====//
    public IActionResult Insert()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Insert([FromForm] InsertNewRequest request)
    {
        if (request.ImagePath == null || request.ImagePath.Length == 0)
        {
            ModelState.AddModelError("Image", "Image file is required.");
            return View(request);
        }
        string Image = fileUploadService.UploadFileAsync(request.ImagePath);

        var item = mapper.Map<New>(request);
        item.ImagePath = Image;
        item.CreatedAt = DateTime.UtcNow;
        item.CreatedBy = Guid.NewGuid();
        repository.Add(item);
        repository.Commit();

        return RedirectToAction("gets");
    }

    // === Update === //
    [HttpGet]
    public ActionResult Update(Guid id)
    {
        var iQueryable = repository.GetSingle(e => e.Id == id && e.DeletedAt == null);
        if (iQueryable == null) return View();

        var results = mapper.Map<UpdateNewRequest>(iQueryable);
        return View(results);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(Guid id, UpdateNewRequest request)
    {
        var item = repository.GetSingle(e => e.Id == id && e.DeletedAt == null);
        if (item == null) return NotFound();

        if (request.ImagePath != null && request.ImagePath.Length > 0)
        {
            string Image = fileUploadService.UploadFileAsync(request.ImagePath);
            item.ImagePath = Image;
        }

        item.Title = request.Title ?? item.Title;
        item.Description = request.Description ?? item.Description;
        item.UpdatedAt = DateTime.UtcNow;

        repository.Update(item);
        repository.Commit();

        return RedirectToAction("gets");
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

        return RedirectToAction("gets");
    }

}



public class ApiNewController(
    IMapper mapper,
    INewRepository repository) : MyAdminController
{
    [HttpGet]
    public IActionResult Gets(int pageNumber = 1, int pageSize = 10)
    {
        pageNumber = pageNumber < 1 ? 1 : pageNumber;
        var iQueryable = repository.FindBy(e => e.DeletedAt == null).AsNoTracking();
        var pagedData = iQueryable
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        var results = mapper.ProjectTo<ListNewResponse>(pagedData).ToList();

        return Ok(results);
    }


    // === Get only ====//
    [HttpGet("{id:guid}")]
    public IActionResult Get(Guid id)
    {
        var iQueryable = repository.FindBy(e =>
            e.Id == id &&
            e.DeletedAt == null
        ).AsNoTracking();
        var result = mapper.ProjectTo<DetailNewResponse>(iQueryable).FirstOrDefault();
        return result == null ? ItemNotFound() : Ok(result);
    }
}