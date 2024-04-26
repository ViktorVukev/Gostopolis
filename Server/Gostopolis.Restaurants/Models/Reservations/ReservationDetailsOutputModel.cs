namespace Gostopolis.Restaurants.Models.Reservations;

using AutoMapper;
using Gostopolis.Models;
using Data.Models;
using Tables;

public class ReservationDetailsOutputModel : IMapFrom<Reservation>
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public string ClientId { get; set; }

    public DateTime StartDate { get; set; }

    public int Guests { get; set; }
    public bool IsCancelled { get; set; }

    public int RestaurantId { get; set; }

    public Restaurant Restaurant { get; set; }

    public IEnumerable<TableDetailsOutputModel> Tables { get; set; } = new List<TableDetailsOutputModel>();

    /// <inheritdoc />
    public void Mapping(Profile mapper)
        => mapper
            .CreateMap<Reservation, ReservationDetailsOutputModel>()
            .ForMember(r => r.Tables, cfg => cfg.Ignore());
}
