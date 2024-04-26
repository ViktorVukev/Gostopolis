namespace Gostopolis.Services.Files;

public interface IFileService
{
    Task<string> UploadFileAsync(string base64);
}
