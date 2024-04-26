namespace Gostopolis.Restaurants.Models.RestaurantTypes;

using AutoMapper;
using Gostopolis.Models;
using Data.Models;

public class RestaurantTypeOutputModel : IMapFrom<RestaurantType>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    /// <inheritdoc />
    public void Mapping(Profile mapper)
        => mapper
            .CreateMap<RestaurantType, RestaurantTypeOutputModel>();
}
