namespace Gostopolis.Services.Files;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

public class FileService : IFileService
{
    public async Task<string> UploadFileAsync(string base64)
    {
        var apiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY");
        var cloudinary = new Cloudinary(apiKey);

        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(base64)
        };

        var result = await cloudinary.UploadAsync(uploadParams);

        return result.SecureUrl.AbsoluteUri;
    }
}
