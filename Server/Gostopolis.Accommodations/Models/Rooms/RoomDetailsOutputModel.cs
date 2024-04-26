namespace Gostopolis.Accommodations.Models.Rooms;

using Gostopolis.Accommodations.Data.Models;
using Accommodations;
using Gostopolis.Models;
using AutoMapper;
using Gostopolis.Data.Interfaces;

public class RoomDetailsOutputModel : IMapFrom<Room>, IIterable
{
    public int Id { get; set; }

    public string Name { get; set; }

    public RoomType Type { get; set; }

    public string Number { get; set; }

    public int Floor { get; set; }

    public decimal PricePerNight { get; set; }

    public int Capacity { get; set; }

    public string Beds { get; set; }

    public string RoomAmenities { get; set; }

    public bool HasPrivateBathroom { get; set; }

    public string BathroomAmenities { get; set; }

    public int AccommodationId { get; set; }

    public void Mapping(Profile mapper)
        => mapper
            .CreateMap<Room, RoomDetailsOutputModel>();
}
