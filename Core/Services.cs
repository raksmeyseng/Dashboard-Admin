using Amazon.S3;
using Amazon.S3.Transfer;

public interface IFileUploadService
{
    Task<string> UploadFileAsync(IFormFile imagePath);
}

public class FileUploadService(IConfiguration configuration) : IFileUploadService
{
    private readonly IConfiguration _configuration = configuration;

    public async Task<string> UploadFileAsync(IFormFile imagePath)
    {
        var accessKey = _configuration["DigitalOceanSpaces:AccessKey"];
        var secretKey = _configuration["DigitalOceanSpaces:SecretKey"];
        var region = _configuration["DigitalOceanSpaces:Region"];
        var endpoint = _configuration["DigitalOceanSpaces:EndPoint"];
        var spaceName = _configuration["DigitalOceanSpaces:SpaceName"];

        var client = new AmazonS3Client(accessKey, secretKey, new AmazonS3Config
        {
            ServiceURL = endpoint,
            ForcePathStyle = true
        });

        var transferUtility = new TransferUtility(client);
        using (var stream = imagePath.OpenReadStream())
        {
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = stream,
                BucketName = spaceName,
                Key = $"images/{imagePath.FileName}",
                CannedACL = S3CannedACL.PublicRead
            };

            await transferUtility.UploadAsync(uploadRequest);
        }
        string url = $"https://{spaceName}.{region}.digitaloceanspaces.com/images/{imagePath.FileName}";
        return url;
    }
}
