namespace Gostopolis.Accommodations.Services.Images;

using Data.Models;
using Gostopolis.Services;
using Models.Images;

/// <summary>
/// Used to interact with image records.
/// </summary>
public interface IImageService : IDataService<Image>
{
    /// <summary>
    /// Uploads the image to Cloudinary.
    /// </summary>
    /// <param name="input">The image in base64 format.</param>
    /// <returns>The id of the newly created image.</returns>
    Task<int> Upload(UploadImageInputModel input);

    /// <summary>
    /// Deletes image from the database.
    /// </summary>
    /// <param name="id">The id of the image.</param>
    /// <returns>Whether the image is successfully deleted or not.</returns>
    Task<bool> Delete(int id);
}