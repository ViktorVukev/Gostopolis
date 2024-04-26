namespace Gostopolis.Restaurants.Services.Images;

using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Gostopolis.Services;
using Gostopolis.Services.Files;
using Data;
using Data.Models;
using Models.Images;

/// <summary>
/// Used to interact with image records.
/// </summary>
/// <param name="db">Current instance of the database.</param>
/// <param name="fileService">Common interface for uploading images.</param>
/// <param name="mapper">AutoMapper interface.</param>
public class ImageService(
        RestaurantsDbContext db,
        IFileService fileService,
        IMapper mapper)
    : DataService<Image>(db), IImageService
{
    /// <inheritdoc />
    public async Task<int> Upload(UploadImageInputModel input)
    {
        var imageUrl = await fileService.UploadFileAsync(input.ImageBase64);

        var image = new Image()
        {
            RestaurantId = input.RestaurantId,
            Url = imageUrl
        };

        await this.Save(image);

        return image.Id;
    }

    /// <inheritdoc />
    public async Task<bool> Delete(int id)
    {
        var image = await this.All().FirstOrDefaultAsync(i => i.Id == id);
        if (image == null)
        {
            return false;
        }

        this.Data.Remove(image);
        await this.Data.SaveChangesAsync();

        return true;
    }

    private async Task<ImageOutputModel?> FindById(int id)
        => await mapper
            .ProjectTo<ImageOutputModel>(this
                .All()
                .Where(i => i.Id == id))
            .FirstOrDefaultAsync();
}