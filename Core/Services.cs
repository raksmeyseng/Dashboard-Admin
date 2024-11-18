using Amazon.S3;
using Amazon.S3.Transfer;
using ArchtistStudio.Core;

public interface IFileUploadService
{
    string UploadFileAsync(IFormFile imagePath);
}

public class FileUploadService : IFileUploadService
{
    public string UploadFileAsync(IFormFile imagePath)
    {
        if (imagePath == null || imagePath.Length == 0)
        {
            throw new ArgumentException("Image file is required");
        }
        using (var amazonS3Client = new AmazonS3Client(MyEnvironment.AccessKey, MyEnvironment.Secretkey, new AmazonS3Config
        {
            ServiceURL = MyEnvironment.Endpoint,
            ForcePathStyle = true
        }))
        {
            using var memoryStream = new MemoryStream();
            imagePath.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = memoryStream,
                Key = imagePath.FileName,
                BucketName = MyEnvironment.SpaceName,
                ContentType = imagePath.ContentType,
                CannedACL = S3CannedACL.PublicRead
            };
            var transferUtility = new TransferUtility(amazonS3Client);

            try
            {
                transferUtility.UploadAsync(uploadRequest);
            }
            catch (AmazonS3Exception ex)
            {
                throw new Exception("Upload failed: " + ex.Message, ex);
            }
        }
        string fileUrl = $"https://{MyEnvironment.SpaceName}.{MyEnvironment.Region}.digitaloceanspaces.com/{imagePath.FileName}";
        return fileUrl;
    }
}
