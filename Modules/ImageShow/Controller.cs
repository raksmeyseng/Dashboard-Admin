using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using ArchtistStudio.Modules.Image;


namespace ArchtistStudio.Modules.ImageShow;

public class ImageShowController(
    IFileUploadService fileUploadService,
    IMapper mapper,
    IImageShowRepository repository,
    IImageRepository imageRepository) : MyController
{
    // === Gets ====//
      [HttpGet]
public IActionResult Gets([FromQuery] string? projectame, int pageNumber = 1, int pageSize = 13)
{
    pageNumber = pageNumber < 1 ? 1 : pageNumber;
    var iQueryable = repository
        .FindBy(e => e.DeletedAt == null)
        .Include(slideimage => slideimage.Image.Project) 
        .AsNoTracking();

      if (!string.IsNullOrEmpty(projectame))
        {
            var searchTerm = projectame.ToLower();
            iQueryable = iQueryable.Where(e => e.Image.Project.ProjectName.ToLower().Contains(searchTerm));
        }

    var totalItems = iQueryable.Count();
    var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

    var pagedData = iQueryable
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();

    var results = mapper.ProjectTo<ListImageShowResponse>(pagedData.AsQueryable()).ToList();

    ViewBag.TotalPages = totalPages;
    ViewBag.CurrentPage = pageNumber;
    ViewBag.SearchQuery = projectame;

    return View(results);
}
 
    [HttpGet]
    public IActionResult Insert(Guid id)
    {
        var image = imageRepository.FindBy(e => e.Id == id).FirstOrDefault();
        if (image == null)
        {
            return NotFound("Project not found.");
        }
        var model = new InsertImageShowRequest
        {
            ImageId = id
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult Insert([FromForm] InsertImageShowRequest request)
    {
        var image = imageRepository.FindBy(e => e.Id == request.ImageId).FirstOrDefault();

        if (image == null)
        {
            ModelState.AddModelError("ProjectId", "Invalid Project ID.");
            return View(request);
        }

        if (request.ImagePath == null || request.ImagePath.Length == 0)
        {
            ModelState.AddModelError("ImagePath", "ImageShow file is required.");
            return View(request);
        }

        string Image = fileUploadService.UploadFileAsync(request.ImagePath);

        var item = mapper.Map<ImageShow>(request);
        item.ImagePath = Image;
        item.CreatedAt = DateTime.UtcNow;
        item.CreatedBy = Guid.NewGuid();

        image.ImageShows ??= [];
        image.ImageShows.Add(item);

        repository.Add(item);
        repository.Commit();

        return RedirectToAction("gets", "imageshow", new { id = request.ImageId });
    }



    // === Update === //
    [HttpGet]
    public IActionResult Update(Guid id)
    {
        var ImageShow = repository.FindBy(e => e.Id == id).FirstOrDefault();
        if (ImageShow == null)
        {
            return NotFound("ImageShow not found.");
        }

        var model = new UpdateImageShowRequest
        {
            ImageId = ImageShow.ImageId,
            Description = ImageShow.Description,
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(Guid id, UpdateImageShowRequest request)
    {
        var ImageShow = repository.FindBy(e => e.Id == id).FirstOrDefault();
        if (ImageShow == null)
        {
            ModelState.AddModelError("", "Image not found.");
            return View(request);
        }
        var project = imageRepository.FindBy(e => e.Id == request.ImageId).FirstOrDefault();
        if (project == null)
        {
            ModelState.AddModelError("ProjectId", "Invalid Project ID.");
            return View(request);
        }
        if (request.ImagePath != null && request.ImagePath.Length > 0)
        {
            string Image = fileUploadService.UploadFileAsync(request.ImagePath);
            ImageShow.ImagePath = Image;
        }
        ImageShow.Description = request.Description ?? ImageShow.Description;
        ImageShow.UpdatedAt = DateTime.UtcNow;

        repository.Update(ImageShow);
        repository.Commit();

        return RedirectToAction("gets", "imageshow", new { id = request.ImageId });
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

        return RedirectToAction("gets", "imageshow");
    }

}

public class ApiImageShowController(
    IMapper mapper,
    IImageShowRepository repository) : MyAdminController
{
    [HttpGet]
    public IActionResult Gets(int pageNumber = 1, int pageSize = 10)
    {
        pageNumber = pageNumber < 1 ? 1 : pageNumber;
        var iQueryable = repository.FindBy(e => e.DeletedAt == null).AsNoTracking();
        var pagedData = iQueryable
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        var results = mapper.ProjectTo<DatailImageShowResponse>(pagedData).ToList();

        return Ok(results);
    }
}