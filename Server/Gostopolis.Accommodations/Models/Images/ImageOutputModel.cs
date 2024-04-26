namespace Gostopolis.Accommodations.Models.Images;

using AutoMapper;
using Data.Models;
using Gostopolis.Models;

public class ImageOutputModel : IMapFrom<Image>
{
    public int Id { get; set; }

    public string Url { get; set; }

    public string RestaurantId { get; set; }

    public void Mapping(Profile mapper)
        => mapper
            .CreateMap<Image, ImageOutputModel>();
}
