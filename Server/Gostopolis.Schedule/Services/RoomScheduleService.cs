namespace Gostopolis.Schedule.Services;

using AutoMapper;
using Data;
using Data.Models;
using Gostopolis.Messages.Schedule;
using Gostopolis.Services;
using Microsoft.EntityFrameworkCore;

public class RoomScheduleService : DataService<RoomOccupation>, IRoomScheduleService
{
    private readonly IMapper mapper;

    public RoomScheduleService(ScheduleDbContext db, IMapper mapper)
        : base(db)
    {
        this.mapper = mapper;
    }

    public async Task CreateReservation(RoomOccupiedMessage message)
    {
        var roomOccupation = new RoomOccupation()
        {
            RoomId = message.RoomId,
            ReservationId = message.ReservationId,
            StartTime = message.StartTime,
            EndTime = message.EndTime
        };

        await this.Data.AddAsync(roomOccupation);

        await this.Data.SaveChangesAsync();
    }

    public async Task RemoveReservation(int reservationId)
    {
        var occupation = this.All().FirstOrDefault();

        this.Data.Remove(occupation);

        await this.Data.SaveChangesAsync();
    }

    public async Task<bool> IsAvailable(int roomId, DateTime startDate, DateTime endDate)
    {
        var isNotAvailable = await this.All().AnyAsync(ro => 
            ro.RoomId == roomId 
            && ((ro.StartTime >= startDate && ro.StartTime <= endDate) 
                || (ro.EndTime >= startDate && ro.EndTime <= endDate)));

        return !isNotAvailable;
    }
}
