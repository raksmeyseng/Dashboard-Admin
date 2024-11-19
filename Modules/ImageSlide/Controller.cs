using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using ArchtistStudio.Modules.Project;

namespace ArchtistStudio.Modules.ImageSlide;

public class ImageSlideController(
     DigitalOceanSpaceService digitalOceanSpaceService,
    IMapper mapper,
    IImageSlideRepository repository,
    IProjectRepository projectRepository) : MyController
{
    // === Gets ====//
    [HttpGet]
    public IActionResult Gets([FromQuery] string? projectame, int pageNumber = 1, int pageSize = 13)
    {
        pageNumber = pageNumber < 1 ? 1 : pageNumber;
        var iQueryable = repository
            .FindBy(e => e.DeletedAt == null)
            .Include(image => image.Project)
            .AsNoTracking();

        if (!string.IsNullOrEmpty(projectame))
        {
            var searchTerm = projectame.ToLower();
            iQueryable = iQueryable.Where(e => e.Project.ProjectName.ToLower().Contains(searchTerm));
        }

        var totalItems = iQueryable.Count();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var pagedData = iQueryable
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var results = mapper.ProjectTo<ListImageSlideResponse>(pagedData.AsQueryable()).ToList();

        ViewBag.TotalPages = totalPages;
        ViewBag.CurrentPage = pageNumber;
        ViewBag.SearchQuery = projectame;

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
        var model = new InsertImageSlideRequest
        {
            ProjectId = id
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Insert([FromForm] InsertImageSlideRequest request)
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
        string Image = await digitalOceanSpaceService.UploadImageAsync(request.ImagePath);

        var item = mapper.Map<ImageSlide>(request);
        item.CreatedAt = DateTime.UtcNow;
        item.CreatedBy = Guid.NewGuid();
        item.ImagePath = Image;

        project.ImageSlides ??= [];
        project.ImageSlides.Add(item);

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

        var model = new UpdateImageSlideRequest
        {
            ProjectId = image.ProjectId,
            Description = image.Description,
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public  async Task<IActionResult> Update(Guid id, UpdateImageSlideRequest request)
    {
        var item = repository.FindBy(e => e.Id == id).FirstOrDefault();
        if (item == null)
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
        if (request.ImagePath != null && request.ImagePath.Length > 0)
        {
            string Image = await digitalOceanSpaceService.UploadImageAsync(request.ImagePath);
            item.ImagePath = Image;
        }

        item.Description = request.Description ?? item.Description;
        item.UpdatedAt = DateTime.UtcNow;

        repository.Update(item);
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

public class ApiImageSlideController(
    IImageSlideRepository repository) : MyAdminController
{
    [HttpGet]
    public IActionResult Gets(int pageNumber = 1, int pageSize = 10)
    {
        pageNumber = pageNumber < 1 ? 1 : pageNumber;
        var iQueryable = repository.FindBy(e => e.DeletedAt == null)
                                   .AsNoTracking();
        var pagedResults = iQueryable
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(s => new DatailImageSlideResponse
            {
                ImagePath = s.ImagePath,
                Description = s.Description,
            })
            .ToList();

        return Ok(pagedResults);
    }

}