namespace Gostopolis.Restaurants.Services.Reservations;

using AutoMapper;
using Data;
using Data.Models;
using Gostopolis.Data.Models;
using Messages.Partners;
using Gostopolis.Messages.Schedule;
using Gostopolis.Services;
using Gostopolis.Services.Emails;
using Gostopolis.Services.Identity;
using Infrastructure;
using MassTransit;
using Models.Reservations;
using Models.Tables;
using Restaurants;
using Tables;

public class ReservationService(
    RestaurantsDbContext db,
    ICurrentUserService currentUser,
    ITableService tables,
    IRestaurantService restaurants,
    IEmailService email,
    IMapper mapper, 
    IPublishEndpoint publisher) 
    : DataService<Reservation>(db), 
        IReservationService
{
    /// <inheritdoc />
    public async Task<int> Create(CreateReservationInputModel input)
    {
        var restaurant = await restaurants.FindById(input.RestaurantId);

        var reservationTables = new List<TableDetailsOutputModel>();

        input.TableIds
            .Split(";")
            .Select(int.Parse)
            .ForEach(id => reservationTables.Add(tables.FindById(id).GetAwaiter().GetResult()));

        var reservation = new Reservation()
        {
            ClientId = currentUser.UserId,
            StartDate = input.StartDate,
            Guests = input.Guests,
            RestaurantId = input.RestaurantId
        };

        var messages = new List<Message>();

        var messageData = new ReservationCreatedMessage()
        {
            ReservationId = reservation.Id,
            PropertyId = input.RestaurantId
        };

        await this.Save(reservation, new Message(messageData));

        await publisher.Publish(messageData);

        foreach (var table in reservationTables)
        {
            var reservationRoom = new ReservationTable()
            {
                TableId = table.Id,
                ReservationId = reservation.Id
            };

            var scheduleData = new TableOccupiedMessage()
            {
                ReservationId = reservation.Id,
                TableId = table.Id,
                StartTime = input.StartDate,
                EndTime = input.StartDate.AddHours(2)
            };

            var scheduleMessage = new Message(scheduleData);

            this.Data.Add(reservationRoom);

            await publisher.Publish(scheduleData);

            messages.Add(scheduleMessage);
        }

        await this.Save(reservation, messages.ToArray());

        foreach (var message in messages)
        {
            await this.MarkMessageAsPublished(message.Id);
        }

        await email.Send("Нова резервация",
            $"Към вашия обект {restaurant.Name} бе добавена нова резервация за {input.Guests} човека на {input.StartDate.ToString("dd/MM/yyyy")} в {input.StartDate.ToString("HH:mm")} часа.",
            input.PartnerEmail);

        await email.Send("Потвърждение за резервация",
            $"Успешно създадохте резервация в {restaurant.Type.Name} \"{restaurant.Name}\" за {input.Guests} човека на {input.StartDate.ToString("dd/MM/yyyy")} в {input.StartDate.ToString("HH:mm")} часа",
            currentUser.Email);

        return reservation.Id;
    }

    /// <inheritdoc />
    public IEnumerable<ReservationDetailsOutputModel> ByCurrentUser()
    {
        var reservations = mapper.ProjectTo<ReservationDetailsOutputModel>(
            this.All().Where(r => r.ClientId == currentUser.UserId))
            .ToList();

        reservations.ForEach(r => r.Tables = tables.FindByReservationId(r.Id));

        return reservations;
    }

    /// <inheritdoc />
    public IEnumerable<ReservationDetailsOutputModel> ByRestaurantId(int id)
    {
        var reservations = mapper.ProjectTo<ReservationDetailsOutputModel>(
                this.All().Where(r => r.RestaurantId == id))
            .ToList();

        reservations.ForEach(r => r.Tables = tables.FindByReservationId(r.Id));

        return reservations;
    }
}
