using Gostopolis.Accommodations.Data.Models;

namespace Gostopolis.Accommodations.Models.Reservations;

using AutoMapper;
using Gostopolis.Models;

public class ReservationDetailsOutputModel : IMapFrom<Reservation>
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public string ClientId { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int Guests { get; set; }

    public decimal TotalPrice { get; set; }

    public string AdditionalInformation { get; set; }

    public int AccommodationId { get; set; }

    public Accommodation Accommodation { get; set; }

    //public IList<ReservationRoom> ReservationRooms { get; set; } = new List<ReservationRoom>();

    public void Mapping(Profile mapper)
        => mapper
            .CreateMap<Reservation, ReservationDetailsOutputModel>();
}
