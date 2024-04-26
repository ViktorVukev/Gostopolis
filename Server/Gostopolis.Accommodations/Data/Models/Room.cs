namespace Gostopolis.Accommodations.Data.Models;

public class Room
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

    public Accommodation Accommodation { get; set; }

    public IList<ReservationRoom> ReservationRooms { get; set; } = new List<ReservationRoom>();
}
