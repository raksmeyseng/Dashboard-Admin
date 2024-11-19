using Amazon.S3;
using Amazon.S3.Transfer;

public class DigitalOceanSpaceService
{
    private readonly IConfiguration _configuration;

    public DigitalOceanSpaceService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> UploadImageAsync(IFormFile file)
    {
        var accessKey = _configuration["DigitalOceanSpaces:AccessKey"];
        var secretKey = _configuration["DigitalOceanSpaces:SecretKey"];
        var region = _configuration["DigitalOceanSpaces:Region"];
        var endpoint = _configuration["DigitalOceanSpaces:EndPoint"];
        var spaceName = _configuration["DigitalOceanSpaces:SpaceName"];

        var client = new AmazonS3Client(accessKey, secretKey,
            new AmazonS3Config
            {
                ServiceURL = endpoint,
                ForcePathStyle = true 
            });

        var transferUtility = new TransferUtility(client);

        using (var stream = file.OpenReadStream())
        {
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = stream,
                BucketName = spaceName,
                Key = $"images/{file.FileName}",
                CannedACL = S3CannedACL.PublicRead
            };

            await transferUtility.UploadAsync(uploadRequest);
        }

        string url = $"https://{spaceName}.{region}.digitaloceanspaces.com/images/{file.FileName}";
        return url;
    }
}
