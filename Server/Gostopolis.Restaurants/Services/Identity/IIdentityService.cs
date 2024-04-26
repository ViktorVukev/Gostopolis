namespace Gostopolis.Restaurants.Services.Identity;

using Models.Users;
using Refit;

public interface IIdentityService
{
    [Get("/Users/{id}")]
    Task<UserDetailsOutputModel> GetUserById(string id);
}
