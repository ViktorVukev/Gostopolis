namespace Gostopolis.Restaurants.Models.Images;

using AutoMapper;
using Gostopolis.Models;
using Data.Models;

public class ImageOutputModel : IMapFrom<Image>
{
    public int Id { get; set; }

    public string Url { get; set; }

    public string RestaurantId { get; set; }

    /// <inheritdoc />
    public void Mapping(Profile mapper)
        => mapper
            .CreateMap<Image, ImageOutputModel>();
}
