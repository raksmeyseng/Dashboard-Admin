
public interface IService
{
    Task<string> UploadImageAsync(IFormFile imagePath);
}

public class ImageUploadService(IWebHostEnvironment environment) : IService
{
    private readonly IWebHostEnvironment _environment = environment;

    public async Task<string> UploadImageAsync(IFormFile imagePath)
    {
        if (imagePath == null || imagePath.Length == 0)
        {
            throw new ArgumentException("No file provided.");
        }

        if (!imagePath.ContentType.StartsWith("image/"))
        {
            throw new ArgumentException("Please upload a valid image file.");
        }

        string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(imagePath.FileName);
        string imageDirectoryPath = Path.Combine(_environment.WebRootPath, "images"); 
        string imageFullPath = Path.Combine(imageDirectoryPath, newFileName);

        if (!Directory.Exists(imageDirectoryPath))
        {
            Directory.CreateDirectory(imageDirectoryPath);
        }
        using (var stream = new FileStream(imageFullPath, FileMode.Create))
        {
            await imagePath.CopyToAsync(stream);
        }
        return Path.Combine("images", newFileName);
    }
}
