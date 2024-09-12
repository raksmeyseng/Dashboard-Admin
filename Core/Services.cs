public interface IFileUploadService
{
    string UploadFileAsync(IFormFile file, string directoryPath);
}

public class FileUploadService(IWebHostEnvironment environment) : IFileUploadService
{
    public string UploadFileAsync(IFormFile file, string directoryPath)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("File is required.");
        }
      string relativePath = directoryPath.TrimStart('/');   
        string newFileName = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(file.FileName);
        string fileFullPath = Path.Combine(environment.WebRootPath, relativePath, newFileName);

        var directory = Path.GetDirectoryName(fileFullPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        using (var stream = new FileStream(fileFullPath, FileMode.Create))
        {
             file.CopyToAsync(stream);
        }

        return newFileName;
    }
}
