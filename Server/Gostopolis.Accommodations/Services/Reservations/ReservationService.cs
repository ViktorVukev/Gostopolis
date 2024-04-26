namespace Gostopolis.Accommodations.Services.Reservations;

using Accommodations;
using AutoMapper;
using Data.Models;
using Gostopolis.Accommodations.Data;
using Gostopolis.Data.Models;
using Gostopolis.Messages.Partners;
using Gostopolis.Services;
using Gostopolis.Services.Emails;
using Gostopolis.Services.Identity;
using Infrastructure;
using MassTransit;
using Messages.Schedule;
using Models.Reservations;
using Models.Rooms;
using Rooms;
using Schedule;

public class ReservationService(
    AccommodationsDbContext db,
    ICurrentUserService currentUser,
    IRoomService rooms,
    IAccommodationService accommodations,
    IEmailService email,
    IMapper mapper, 
    IPublishEndpoint publisher) 
    : DataService<Reservation>(db), 
        IReservationService
{
    public async Task<int> Create(CreateReservationInputModel input)
    {
        var accommodation = await accommodations.FindById(input.AccommodationId);

        var reservationRooms = new List<RoomDetailsOutputModel>();

        input.RoomIds
            .Split(";")
            .Select(int.Parse)
            .ForEach(id => reservationRooms.Add(rooms.FindById(id).GetAwaiter().GetResult()));

        var totalPrice = reservationRooms.Sum(room => room.PricePerNight * input.Nights);

        var reservation = new Reservation()
        {
            ClientId = currentUser.UserId,
            DateOfBirth = input.DateOfBirth,
            StartDate = input.StartDate,
            EndDate = input.EndDate,
            Guests = input.Guests,
            TotalPrice = totalPrice,
            AdditionalInformation = input.AdditionalInformation,
            AccommodationId = input.AccommodationId
        };

        var messages = new List<Message>();

        var messageData = new ReservationCreatedMessage()
        {
            ReservationId = reservation.Id,
            PropertyId = input.AccommodationId
        };


        await this.Save(reservation, new Message(messageData));

        await publisher.Publish(messageData);

        foreach (var room in reservationRooms)
        {
            var reservationRoom = new ReservationRoom()
            {
                RoomId = room.Id,
                ReservationId = reservation.Id
            };

            var scheduleData = new RoomOccupiedMessage()
            {
                ReservationId = reservation.Id,
                RoomId = room.Id,
                StartTime = input.StartDate.ToDateTime((TimeOnly)accommodation.CheckInTime),
                EndTime = input.EndDate.ToDateTime((TimeOnly)accommodation.CheckOutTime)
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
            $"Към вашия обект {accommodation.Name} бе добавена нова резерация за периода {input.StartDate.ToString("dd/MM/yyyy")}-{input.EndDate.ToString("dd/MM/yyyy")}",
            input.PartnerEmail);

        await email.Send("Потвърждение за резервация",
            $"Успешно създадохте резервация в {accommodation.Type.Name} \"{accommodation.Name}\" за периода {input.StartDate.ToString("dd/MM/yyyy")}-{input.EndDate.ToString("dd/MM/yyyy")}",
            input.ClientEmail);

        return reservation.Id;
    }

    public IEnumerable<ReservationDetailsOutputModel> ByCurrentUser()
        => mapper.ProjectTo<ReservationDetailsOutputModel>(this.All().Where(r => r.ClientId == currentUser.UserId));

    public IEnumerable<ReservationDetailsOutputModel> ByAccommodationId(int id)
        => mapper.ProjectTo<ReservationDetailsOutputModel>(this.All().Where(r => r.AccommodationId == id));

}
