using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;

namespace ArchtistStudio.Modules.About;


public class AboutController(
    IFileUploadService fileUploadService,
    IMapper mapper,
    IAboutRepository repository) : MyController
{
    // === Gets ====//
    [HttpGet]
    public IActionResult Gets()
    {
        var about = repository.GetSingle(e => e.DeletedAt == null);
        if (about == null) return RedirectToAction(nameof(Insert));

        var result = mapper.Map<DetailAboutResponse>(about);
        return View(result);
    }

    [HttpGet]
    public IActionResult Insert() => View();

    [HttpPost]
    public async Task<IActionResult> Insert([FromForm] InsertAboutRequest request)
    {
        if (!ModelState.IsValid) return View(request);

        if (repository.GetSingle(e => e.DeletedAt == null) != null)
        {
            TempData["Message"] = "About section already exists. Update it instead.";
            return RedirectToAction(nameof(Gets));
        }

        try
        {
            var about = mapper.Map<About>(request);

            about.ImagePath = await fileUploadService.UploadFileAsync(request.ImagePath);
            about.ImagePathWe = await fileUploadService.UploadFileAsync(request.ImagePathWe);
            about.ImagePathVision = await fileUploadService.UploadFileAsync(request.ImagePathVision);
            about.ImagePathService = await fileUploadService.UploadFileAsync(request.ImagePathService);
            about.ImagePathProcess = await fileUploadService.UploadFileAsync(request.ImagePathProcess);
            about.ImagePathPlanning = await fileUploadService.UploadFileAsync(request.ImagePathPlanning);

            about.CreatedAt = DateTime.UtcNow;
            about.CreatedBy = Guid.NewGuid();

            repository.Add(about);
            repository.Commit();

            return RedirectToAction(nameof(Gets));
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "Failed to save data. Try again later.");
            return View(request);
        }
    }

    [HttpGet]
    public IActionResult Update(Guid id)
    {
        var about = repository.GetSingle(e => e.Id == id && e.DeletedAt == null);
        if (about == null) return NotFound();

        var model = mapper.Map<UpdateAboutRequest>(about);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Guid id, UpdateAboutRequest request)
    {
        var about = repository.GetSingle(e => e.Id == id && e.DeletedAt == null);
        if (about == null) return NotFound();

        try
        {
            about.ImagePath = request.ImagePath != null ? await fileUploadService.UploadFileAsync(request.ImagePath) : about.ImagePath;
            about.ImagePathWe = request.ImagePathWe != null ? await fileUploadService.UploadFileAsync(request.ImagePathWe) : about.ImagePathWe;
            about.ImagePathVision = request.ImagePathVision != null ? await fileUploadService.UploadFileAsync(request.ImagePathVision) : about.ImagePathVision;
            about.ImagePathService = request.ImagePathService != null ? await fileUploadService.UploadFileAsync(request.ImagePathService) : about.ImagePathService;
            about.ImagePathProcess = request.ImagePathProcess != null ? await fileUploadService.UploadFileAsync(request.ImagePathProcess) : about.ImagePathProcess;
            about.ImagePathPlanning = request.ImagePathPlanning != null ? await fileUploadService.UploadFileAsync(request.ImagePathPlanning) : about.ImagePathPlanning;

            about.Since = request.Since ?? about.Since;
            about.We = request.We ?? about.We;
            about.Vision = request.Vision ?? about.Vision;
            about.Service = request.Service ?? about.Service;
            about.Process = request.Process ?? about.Process;
            about.Planning = request.Planning ?? about.Planning;
            about.UpdatedAt = DateTime.UtcNow;

            repository.Update(about);
            repository.Commit();

            return RedirectToAction(nameof(Gets));
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "Failed to update data. Try again later.");
            return View(request);
        }
    }

    public IActionResult Error()
    {
        return View();
    }
}


public class ApiAboutController(
    IMapper mapper,
    IAboutRepository repository) : MyAdminController
{
    [HttpGet]
    public IActionResult Gets()
    {
        var iQueryable = repository.FindBy(e => e.DeletedAt == null)
            .AsNoTracking();
        var results = mapper.ProjectTo<ListAboutResponse>(iQueryable).FirstOrDefault();
        return Ok(results);
    }
}
