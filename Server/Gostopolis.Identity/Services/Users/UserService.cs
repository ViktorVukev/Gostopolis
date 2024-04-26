namespace Gostopolis.Identity.Services.Users;

using AutoMapper;
using Data;
using Data.Models;
using Gostopolis.Data.Models;
using Models.Partners;
using Gostopolis.Services;
using Gostopolis.Services.Files;
using Gostopolis.Services.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Constants;
using Messages.Partners;
using MassTransit;

public class UserService(
    IdentityDbContext db,
    ICurrentUserService currentUser,
    IFileService fileService,
    UserManager<User> userManager,
    IMapper mapper,
    IPublishEndpoint publisher)
    : DataService<User>(db), IUserService
{
    public async Task<bool> IsPartner(string userId) 
        => await userManager.IsInRoleAsync(await this.FindById(userId), PartnerRoleName);

    public async Task BecomePartner(string userId)
    {
        var messageData = new PartnerApprovedMessage()
        {
            PartnerId = userId
        };

        var message = new Message(messageData);

        await userManager.AddToRoleAsync(await this.FindById(userId), PartnerRoleName);

        await this.Save(await this.FindById(userId), message);

        await publisher.Publish(messageData);

        await this.MarkMessageAsPublished(message.Id);
    }

    public async Task<IEnumerable<UserDetailsOutputModel>> GetAll()
    {
        var users = new List<User>();

        this.All().ToList().ForEach(u =>
        {
            if (!userManager.IsInRoleAsync(u, AdministratorRoleName).GetAwaiter().GetResult())
            {
                users.Add(u);
            }
        });

        return mapper.ProjectTo<UserDetailsOutputModel>(users.AsQueryable());
    }

    public async Task<IEnumerable<UserDetailsOutputModel>> GetPartners()
    {
        var partners = new List<User>();
        
        this.All().ToList().ForEach(u =>
        {
            if (userManager.IsInRoleAsync(u, PartnerRoleName).GetAwaiter().GetResult())
            {
                partners.Add(u);
            }
        });

        return mapper.ProjectTo<UserDetailsOutputModel>(partners.AsQueryable());
    }

    public async Task<UserDetailsOutputModel> GetDetails(string id)
        => await mapper
            .ProjectTo<UserDetailsOutputModel>(this
                .All()
                .Where(u => u.Id == id))
            .FirstOrDefaultAsync();

    public async Task<User> FindById(string id)
        => await this.Data.FindAsync<User>(id);

    public async Task<bool> Edit(string id, EditUserInputModel input)
    {
        var user = await this.FindById(id);

        user.FirstName = input.FirstName;
        user.LastName = input.LastName;
        user.PhoneNumber = input.PhoneNumber;

        await this.Save(user);

        return true;
    }

    public async Task<bool> EditUserImage(EditUserImageInputModel input)
    {
        var user = await this.FindById(currentUser.UserId);

        var newImageUrl = await fileService.UploadFileAsync(input.ImageBase64);

        user.ImageUrl = newImageUrl;

        await this.Save(user);

        return true;
    }

    public async Task<bool> EditEmailPreferences(EditUserEmailPreferencesInputModel input)
    {
        var user = await this.FindById(currentUser.UserId);

        user.LoginNotification = input.LoginNotification;

        await this.Save(user);

        return true;
    }

    public async Task<bool> Delete(string id)
    {
        var user = await this.FindById(id);

        this.Data.Remove(user);

        await this.Data.SaveChangesAsync();

        return true;
    }
}