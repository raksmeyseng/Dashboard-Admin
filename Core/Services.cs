using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using ArchtistStudio.Core;
using Microsoft.AspNetCore.Http; // Make sure to include this for IFormFile
using System;
using System.IO;

public interface IFileUploadService
{
    string UploadFileAsync(IFormFile imagePath);
}

public class FileUploadService : IFileUploadService
{
    private readonly RegionEndpoint region = RegionEndpoint.APSoutheast1;

    public string UploadFileAsync(IFormFile imagePath)
    {
        if (imagePath == null || imagePath.Length == 0)
        {
            throw new ArgumentException("Image file is required");
        }

        using (var amazonS3Client = new AmazonS3Client(MyEnvironment.AWSKey, MyEnvironment.AWSSecretkey, region))
        {
            using (var memoryStream = new MemoryStream())
            {
                imagePath.CopyTo(memoryStream);
                memoryStream.Position = 0;

                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = memoryStream,
                    Key = imagePath.FileName, // Consider using a unique name to avoid overwriting
                    BucketName = MyEnvironment.BucketName,
                    ContentType = imagePath.ContentType
                };

                var transferUtility = new TransferUtility(amazonS3Client);

                try
                {
                    transferUtility.Upload(uploadRequest);
                }
                catch (AmazonS3Exception ex)
                {
                    throw new Exception("Upload failed: " + ex.Message, ex);
                }
            }
        }

        string fileUrl = $"https://{MyEnvironment.BucketName}.s3.ap-southeast-1.amazonaws.com/{imagePath.FileName}";
        return fileUrl;
    }
}
