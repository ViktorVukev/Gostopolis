namespace Gostopolis.Restaurants.Services.WorkingTimes;

using Microsoft.EntityFrameworkCore;
using Gostopolis.Services;
using Data;
using Data.Models;
using Gostopolis.Services.Emails;
using Identity;
using Infrastructure;
using Models.WorkingTimes;
using Reservations;
using static Constants;

/// <summary>
/// Used to interact with working time records.
/// </summary>
/// <param name="db">Current instance of the database.</param>
public class WorkingTimeService(
        IReservationService reservations,
        IIdentityService identity,
        IEmailService email,
        RestaurantsDbContext db)
    : DataService<WorkingTime>(db), IWorkingTimeService
{
    /// <inheritdoc />
    public async Task<int> Create(WorkingTimeInputModel input)
    {
        if (this.All().Any(wt => wt.RestaurantId == input.RestaurantId))
        {
            await this.Edit(input);
            return Zero;
        }

        var workingTime = new WorkingTime()
        {
            MondayOpenTime = TimeOnly.TryParseExact(input.MondayOpenTime, "HH:mm", out var motime) ? motime : null,
            MondayCloseTime = TimeOnly.TryParseExact(input.MondayCloseTime, "HH:mm", out var mctime) ? mctime : null,
            TuesdayOpenTime = TimeOnly.TryParseExact(input.TuesdayOpenTime, "HH:mm", out var totime) ? totime : null,
            TuesdayCloseTime = TimeOnly.TryParseExact(input.TuesdayCloseTime, "HH:mm", out var tctime) ? tctime : null,
            WednesdayOpenTime = TimeOnly.TryParseExact(input.WednesdayOpenTime, "HH:mm", out var wotime) ? wotime : null,
            WednesdayCloseTime = TimeOnly.TryParseExact(input.WednesdayCloseTime, "HH:mm", out var wctime) ? wctime : null,
            ThursdayOpenTime = TimeOnly.TryParseExact(input.ThursdayOpenTime, "HH:mm", out var thotime) ? thotime : null,
            ThursdayCloseTime = TimeOnly.TryParseExact(input.ThursdayCloseTime, "HH:mm", out var thctime) ? thctime : null,
            FridayOpenTime = TimeOnly.TryParseExact(input.FridayOpenTime, "HH:mm", out var fotime) ? fotime : null,
            FridayCloseTime = TimeOnly.TryParseExact(input.FridayCloseTime, "HH:mm", out var fctime) ? fctime : null,
            SaturdayOpenTime = TimeOnly.TryParseExact(input.SaturdayOpenTime, "HH:mm", out var sotime) ? sotime : null,
            SaturdayCloseTime = TimeOnly.TryParseExact(input.SaturdayCloseTime, "HH:mm", out var sctime) ? sctime : null,
            SundayOpenTime = TimeOnly.TryParseExact(input.SundayOpenTime, "HH:mm", out var suotime) ? suotime : null,
            SundayCloseTime = TimeOnly.TryParseExact(input.SundayCloseTime, "HH:mm", out var suctime) ? suctime : null,
            RestaurantId = input.RestaurantId
        };

        await this.Save(workingTime);

        return workingTime.Id;
    }

    /// <inheritdoc />
    public async Task<bool> Edit(WorkingTimeInputModel input)
    {
        var workingTime = await this.All().SingleOrDefaultAsync(wt => wt.RestaurantId == input.RestaurantId);
        if (workingTime == null)
        {
            return false;
        }

        workingTime.MondayOpenTime =
            TimeOnly.TryParseExact(input.MondayOpenTime, "HH:mm", out var motime) ? motime : null;
        workingTime.MondayCloseTime =
            TimeOnly.TryParseExact(input.MondayCloseTime, "HH:mm", out var mctime) ? mctime : null;
        workingTime.TuesdayOpenTime =
            TimeOnly.TryParseExact(input.TuesdayOpenTime, "HH:mm", out var totime) ? totime : null;
        workingTime.TuesdayCloseTime =
            TimeOnly.TryParseExact(input.TuesdayCloseTime, "HH:mm", out var tctime) ? tctime : null;
        workingTime.WednesdayOpenTime =
            TimeOnly.TryParseExact(input.WednesdayOpenTime, "HH:mm", out var wotime) ? wotime : null;
        workingTime.WednesdayCloseTime =
            TimeOnly.TryParseExact(input.WednesdayCloseTime, "HH:mm", out var wctime) ? wctime : null;
        workingTime.ThursdayOpenTime =
            TimeOnly.TryParseExact(input.ThursdayOpenTime, "HH:mm", out var thotime) ? thotime : null;
        workingTime.ThursdayCloseTime =
            TimeOnly.TryParseExact(input.ThursdayCloseTime, "HH:mm", out var thctime) ? thctime : null;
        workingTime.FridayOpenTime =
            TimeOnly.TryParseExact(input.FridayOpenTime, "HH:mm", out var fotime) ? fotime : null;
        workingTime.FridayCloseTime =
            TimeOnly.TryParseExact(input.FridayCloseTime, "HH:mm", out var fctime) ? fctime : null;
        workingTime.SaturdayOpenTime =
            TimeOnly.TryParseExact(input.SaturdayOpenTime, "HH:mm", out var sotime) ? sotime : null;
        workingTime.SaturdayCloseTime =
            TimeOnly.TryParseExact(input.SaturdayCloseTime, "HH:mm", out var sctime) ? sctime : null;
        workingTime.SundayOpenTime =
            TimeOnly.TryParseExact(input.SundayOpenTime, "HH:mm", out var suotime) ? suotime : null;
        workingTime.SundayCloseTime =
            TimeOnly.TryParseExact(input.SundayCloseTime, "HH:mm", out var suctime) ? suctime : null;

        reservations
            .ByRestaurantId(input.RestaurantId)
            .ForEach(async r =>
            {
                var dayOfWeek = r.StartDate.DayOfWeek;

                var isReservationValid = dayOfWeek switch 
                {
                    DayOfWeek.Monday => TimeOnly.FromDateTime(r.StartDate) >= workingTime.MondayOpenTime &&
                                        TimeOnly.FromDateTime(r.StartDate) <= workingTime.MondayCloseTime,
                    DayOfWeek.Tuesday => TimeOnly.FromDateTime(r.StartDate) >= workingTime.TuesdayOpenTime &&
                                         TimeOnly.FromDateTime(r.StartDate) <= workingTime.TuesdayCloseTime,
                    DayOfWeek.Wednesday => TimeOnly.FromDateTime(r.StartDate) >= workingTime.WednesdayOpenTime &&
                                           TimeOnly.FromDateTime(r.StartDate) <= workingTime.WednesdayCloseTime,
                    DayOfWeek.Thursday => TimeOnly.FromDateTime(r.StartDate) >= workingTime.ThursdayOpenTime &&
                                          TimeOnly.FromDateTime(r.StartDate) <= workingTime.ThursdayCloseTime,
                    DayOfWeek.Friday => TimeOnly.FromDateTime(r.StartDate) >= workingTime.FridayOpenTime &&
                                        TimeOnly.FromDateTime(r.StartDate) <= workingTime.FridayCloseTime,
                    DayOfWeek.Saturday => TimeOnly.FromDateTime(r.StartDate) >= workingTime.SaturdayOpenTime &&
                                          TimeOnly.FromDateTime(r.StartDate) <= workingTime.SaturdayCloseTime,
                    DayOfWeek.Sunday => TimeOnly.FromDateTime(r.StartDate) >= workingTime.SundayOpenTime &&
                                        TimeOnly.FromDateTime(r.StartDate) <= workingTime.SundayCloseTime,
                    _ => true
                };

                if (!isReservationValid && r.StartDate >= DateTime.Now && !r.IsCancelled)
                {
                    var reservation = await db.Reservations.FirstOrDefaultAsync(res => res.Id == r.Id);

                    if (reservation != null)
                    {
                        reservation.IsCancelled = true;

                        await db.SaveChangesAsync();
                    }
                    
                    var client = await identity.GetUserById(r.ClientId);
                    await email.Send($"Резервация №{r.Id}",
                        $"Бихме искали да ви информираме, че работното време на заведението \"{r.Restaurant.Name}\", което сте избрали за вашата резервация, е променено. Поради тази промяна, съжаляваме, но вашата резервация е отменена.", client.Email);
                }
            });

        await db.SaveChangesAsync();
        await this.Save(workingTime);

        return true;
    }

    /// <inheritdoc />
    public async Task<bool> Delete(int id)
    {
        var workingTime = await this.All().SingleOrDefaultAsync(r => r.Id == id);

        if (workingTime == null)
        {
            return false;
        }

        this.Data.Remove(workingTime);

        await this.Data.SaveChangesAsync();

        return true;
    }
}
