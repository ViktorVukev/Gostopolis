using Gostopolis.Accommodations.Models.Accommodations;
using Gostopolis.Accommodations.Models.Rooms;

namespace Gostopolis.Accommodations.Services.Rooms;

public interface IRoomService
{
    Task<int> Create(CreateRoomInputModel input);

    IEnumerable<RoomDetailsOutputModel> ByAccommodationId(int id);

    Task<RoomDetailsOutputModel> FindById(int id);

    //Task<bool> Edit(int id, EditAccommodationInputModel input);

    Task<bool> Delete(int id);
}
