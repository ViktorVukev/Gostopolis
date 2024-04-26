using AutoMapper;
using Gostopolis.Messages.Schedule;
using Gostopolis.Schedule.Data.Models;
using Gostopolis.Schedule.Data;
using Gostopolis.Services;

namespace Gostopolis.Schedule.Services;

using Microsoft.EntityFrameworkCore;

public class TableScheduleService : DataService<TableOccupation>, ITableScheduleService
{
    private readonly IMapper mapper;

    public TableScheduleService(ScheduleDbContext db, IMapper mapper)
        : base(db)
    {
        this.mapper = mapper;
    }

    public async Task CreateReservation(TableOccupiedMessage message)
    {
        var tableOccupation = new TableOccupation()
        {
            TableId = message.TableId,
            ReservationId = message.ReservationId,
            StartTime = message.StartTime,
            EndTime = message.EndTime
        };

        await this.Data.AddAsync(tableOccupation);

        await this.Data.SaveChangesAsync();
    }

    public async Task RemoveReservation(int reservationId)
    {
        var occupation = this.All().FirstOrDefault();

        this.Data.Remove(occupation);

        await this.Data.SaveChangesAsync();
    }

    public async Task<bool> IsAvailable(int tableId, DateTime startDate, DateTime endDate)
    {
        var isNotAvailable = await this.All().AnyAsync(to => 
            to.TableId == tableId 
            && ((to.StartTime >= startDate && to.StartTime <= endDate) 
                || (to.EndTime >= startDate && to.EndTime <= endDate)));

        return !isNotAvailable;
    }
}
