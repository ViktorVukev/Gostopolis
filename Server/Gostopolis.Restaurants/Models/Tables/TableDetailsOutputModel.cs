namespace Gostopolis.Restaurants.Models.Tables;

using AutoMapper;
using Gostopolis.Data.Interfaces;
using Gostopolis.Models;
using Data.Models;

public class TableDetailsOutputModel : IMapFrom<Table>, IIterable
{
    public int Id { get; set; }

    public string Number { get; set; }

    public int Capacity { get; set; }

    public bool IsSmokingAllowed { get; set; }

    public bool IsOutdoor { get; set; }

    public int RestaurantId { get; set; }

    /// <inheritdoc />
    public void Mapping(Profile mapper)
        => mapper
            .CreateMap<Table, TableDetailsOutputModel>();
}
