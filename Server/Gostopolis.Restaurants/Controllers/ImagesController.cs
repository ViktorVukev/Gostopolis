namespace Gostopolis.Restaurants.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Gostopolis.Controllers;
using Gostopolis.Services;
using Gostopolis.Services.Identity;
using Models.Images;
using Services.Images;
using Services.Restaurants;

using static Constants;

/// <summary>
/// Used to upload and delete images from property's gallery.
/// </summary>
/// <param name="restaurants">Service interface that keeps restaurants business logic.</param>
/// <param name="currentUser">Current user service interface.</param>
/// <param name="images">Service interface that keeps images business logic.</param>
public class ImagesController(
        IRestaurantService restaurants, 
        ICurrentUserService currentUser,
        IImageService images) 
    : ApiController
{
    /// <summary>
    /// Uploads image to Cloudinary.
    /// </summary>
    /// <param name="input">The image in base64 format.</param>
    /// <returns>The id of the newly created image.</returns>
    [HttpPost]
    [Authorize(Roles = PartnerRoleName)]
    public async Task<Result<int>> Upload(UploadImageInputModel input)
    {
        var restaurant = await restaurants.FindById(input.RestaurantId);

        if (restaurant.PartnerId != currentUser.UserId)
        {
            return Error.ImagesForbidden;
        }

        return await images.Upload(input);
    }

    /// <summary>
    /// Deletes the image with the given id.
    /// </summary>
    /// <param name="id">The id of the image.</param>
    /// <returns>Whether the image is successfully deleted or not.</returns>
    [HttpDelete]
    [Authorize(Roles = PartnerRoleName)]
    public async Task<Result> Delete(int id)
    {
        var restaurant = restaurants
            .GetAll()
            .Single(r => r.Images
                .Any(i => i.Id == id));

        if (restaurant.PartnerId != currentUser.UserId)
        {
            return Error.ImagesForbidden;
        }

        var isSucceeded = await images.Delete(id);

        return isSucceeded
            ? Result.Success 
            : Error.CommonError;
    }
}
