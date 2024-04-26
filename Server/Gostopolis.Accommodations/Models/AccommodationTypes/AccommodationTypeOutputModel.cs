namespace Gostopolis.Accommodations.Models.AccommodationTypes;

using AutoMapper;
using Data.Models;
using Gostopolis.Models;

public class AccommodationTypeOutputModel : IMapFrom<AccommodationType>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public void Mapping(Profile mapper)
        => mapper
            .CreateMap<AccommodationType, AccommodationTypeOutputModel>();
}
