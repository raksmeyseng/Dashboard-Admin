using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using ArchtistStudio.Modules.Project;

namespace ArchtistStudio.Modules.ImageShow;

public class ImageShowController(
    IFileUploadService fileUploadService,
    IMapper mapper,
    IImageShowRepository repository,
    IProjectRepository projectRepository) : MyController
{
    // === Gets ====//
    [HttpGet]
    public IActionResult Gets()
    {
        var iQueryable = repository.FindBy(e => e.DeletedAt == null)
            .AsNoTracking();
        var results = mapper.ProjectTo<ListImageShowResponse>(iQueryable).ToList();
        return View(results);
    }

    [HttpGet]
    public IActionResult Insert(Guid id)
    {
        var project = projectRepository.FindBy(e => e.Id == id).FirstOrDefault();
        if (project == null)
        {
            return NotFound("Project not found.");
        }
        var model = new InsertImageShowRequest
        {
            ProjectId = id
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult Insert([FromForm] InsertImageShowRequest request)
    {
        var project = projectRepository.FindBy(e => e.Id == request.ProjectId).FirstOrDefault();

        if (project == null)
        {
            ModelState.AddModelError("ProjectId", "Invalid Project ID.");
            return View(request);
        }
        if (request.ImagePath == null || request.ImagePath.Length == 0)
        {
            ModelState.AddModelError("ImagePath", "ImageShow file is required.");
            return View(request);
        }
        string ImageShow = fileUploadService.UploadFileAsync(request.ImagePath, "image");

        var item = mapper.Map<ImageShow>(request);
        item.ImagePath = ImageShow;
        item.CreatedAt = DateTime.UtcNow;
        item.CreatedBy = Guid.NewGuid();

        project.ImageShows ??= [];
        project.ImageShows.Add(item);

        repository.Add(item);
        repository.Commit();

        return RedirectToAction("gets", "imageshow", new { id = request.ProjectId });
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
            ProjectId = ImageShow.ProjectId,
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
        var project = projectRepository.FindBy(e => e.Id == request.ProjectId).FirstOrDefault();
        if (project == null)
        {
            ModelState.AddModelError("ProjectId", "Invalid Project ID.");
            return View(request);
        }
        if (request.ImagePath == null || request.ImagePath.Length == 0)
        {
            ModelState.AddModelError("Images", "Image file is required.");
            return View(request);
        }
        string Image = fileUploadService.UploadFileAsync(request.ImagePath, "image");

        ImageShow.Description = request.Description ?? ImageShow.Description;
        ImageShow.UpdatedAt = DateTime.UtcNow;

        repository.Update(ImageShow);
        repository.Commit();

        return RedirectToAction("gets", "imageshow", new { id = request.ProjectId });
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
    public IActionResult Gets()
    {
        var iQueryable = repository.FindBy(e => e.DeletedAt == null)
            .AsNoTracking();
        var results = mapper.ProjectTo<DatailImageShowResponse>(iQueryable).ToList();
        return Ok(results);
    }
}