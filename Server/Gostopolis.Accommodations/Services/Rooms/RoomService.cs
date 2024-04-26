namespace Gostopolis.Accommodations.Services.Rooms;

using AutoMapper;
using Data;
using Data.Models;
using Gostopolis.Accommodations.Models.Rooms;
using Gostopolis.Services;
using Microsoft.EntityFrameworkCore;

public class RoomService(
        AccommodationsDbContext db,
        IMapper mapper)
    : DataService<Room>(db),
        IRoomService
{
    public async Task<int> Create(CreateRoomInputModel input)
    {
        var room = new Room()
        {
            Name = input.Name,
            Type = (RoomType)input.Type,
            Number = input.Number,
            Floor = input.Floor,
            PricePerNight = input.PricePerNight,
            Capacity = input.Capacity,
            Beds = input.Beds,
            RoomAmenities = input.RoomAmenities,
            HasPrivateBathroom = input.HasPrivateBathroom,
            BathroomAmenities = input.BathroomAmenities,
            AccommodationId = input.AccommodationId
        };

        await this.Save(room);

        return room.Id;
    }

    public IEnumerable<RoomDetailsOutputModel> ByAccommodationId(int id)
        => mapper.ProjectTo<RoomDetailsOutputModel>(this.All().Where(r => r.AccommodationId == id));

    public async Task<RoomDetailsOutputModel> FindById(int id)
        => await mapper
            .ProjectTo<RoomDetailsOutputModel>(this
                .All()
                .Where(r => r.Id == id))
            .FirstOrDefaultAsync();

    //public async Task<bool> Edit(int id, EditAccommodationInputModel input)
    //{
    //    var accommodation = await this.All().SingleOrDefaultAsync(a => a.Id == id);
    //    if (accommodation == null)
    //    {
    //        return false;
    //    }

    //    accommodation.IsVerified = false;
    //    accommodation.IdentificationNumber = input.IdentificationNumber;

    //    if (input.CoverPhotoBase64.StartsWith("base64"))
    //    {
    //        accommodation.CoverPhotoUrl = await fileService.UploadFileAsync(input.CoverPhotoBase64);
    //    }


    //    await this.Save(accommodation);

    //    return true;
    //}

    public async Task<bool> Delete(int id)
    {
        var room = await this.All().SingleOrDefaultAsync(r => r.Id == id);

        if (room != null)
        {
            this.Data.Remove(room);
        }

        await this.Data.SaveChangesAsync();

        return true;
    }
}
