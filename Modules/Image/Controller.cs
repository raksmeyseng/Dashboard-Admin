using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using ArchtistStudio.Modules.Project;

namespace ArchtistStudio.Modules.Image;

public class ImageController(
    IFileUploadService fileUploadService,
    IMapper mapper,
    IImageRepository repository,
    IProjectRepository projectRepository) : MyController
{
    // === Gets ====//
    [HttpGet]
    public IActionResult Gets()
    {
        var iQueryable = repository.FindBy(e => e.DeletedAt == null)
            .AsNoTracking();
        var results = mapper.ProjectTo<ListImageResponse>(iQueryable).ToList();
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
        var model = new InsertImageRequest
        {
            ProjectId = id
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult Insert([FromForm] InsertImageRequest request)
    {
        var project = projectRepository.FindBy(e => e.Id == request.ProjectId).FirstOrDefault();

        if (project == null)
        {
            ModelState.AddModelError("ProjectId", "Invalid Project ID.");
            return View(request);
        }
        if (request.ImagePath == null || request.ImagePath.Length == 0)
        {
            ModelState.AddModelError("ImagePath", "Image file is required.");
            return View(request);
        }
        string Image = fileUploadService.UploadFileAsync(request.ImagePath, "image");

        var item = mapper.Map<Image>(request);
        item.ImagePath = Image;
        item.CreatedAt = DateTime.UtcNow;
        item.CreatedBy = Guid.NewGuid();

        project.Images ??= [];
        project.Images.Add(item);

        repository.Add(item);
        repository.Commit();

        return RedirectToAction("gets", "image", new { id = request.ProjectId });
    }


    // === Update === //
    [HttpGet]
    public IActionResult Update(Guid id)
    {
        var image = repository.FindBy(e => e.Id == id).FirstOrDefault();
        if (image == null)
        {
            return NotFound("Image not found.");
        }

        var model = new UpdateImageRequest
        {
            ProjectId = image.ProjectId,
            Description = image.Description,
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(Guid id, UpdateImageRequest request)
    {
        var image = repository.FindBy(e => e.Id == id).FirstOrDefault();
        if (image == null)
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
            ModelState.AddModelError("Image", "Image file is required.");
            return View(request);
        }
        string Image = fileUploadService.UploadFileAsync(request.ImagePath, "image");

        image.Description = request.Description ?? image.Description;
        image.UpdatedAt = DateTime.UtcNow;

        repository.Update(image);
        repository.Commit();

        return RedirectToAction("gets", "image", new { id = request.ProjectId });
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

        return RedirectToAction("gets", "image");
    }

}



public class ApiImageController(
    IMapper mapper,
    IImageRepository repository) : MyAdminController
{
    [HttpGet]
    public IActionResult Gets()
    {
        var iQueryable = repository.FindBy(e => e.DeletedAt == null)
            .AsNoTracking();
        var results = mapper.ProjectTo<ListImageResponse>(iQueryable).ToList();
        return Ok(results);
    }
}