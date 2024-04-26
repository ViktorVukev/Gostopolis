namespace Gostopolis.Identity.Services.Users;

using Gostopolis.Identity.Data.Models;
using Gostopolis.Identity.Models.Partners;
using Gostopolis.Services;

public interface IUserService : IDataService<User>
{
    Task<bool> IsPartner(string id);

    Task BecomePartner(string id);

    Task<IEnumerable<UserDetailsOutputModel>> GetAll();

    Task<IEnumerable<UserDetailsOutputModel>> GetPartners();

    Task<UserDetailsOutputModel> GetDetails(string id);

    Task<User> FindById(string id);

    Task<bool> Edit(string id, EditUserInputModel input);

    Task<bool> EditUserImage(EditUserImageInputModel input);

    Task<bool> EditEmailPreferences(EditUserEmailPreferencesInputModel input);

    Task<bool> Delete(string id);
}