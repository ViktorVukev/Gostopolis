namespace Gostopolis.Admin.Services.Identity;

using Gostopolis.Services;
using Models.Applications;
using Models.Identity;
using Models.Users;
using Refit;
using UserInputModel = Models.Users.UserInputModel;

public interface IIdentityService
{
    [Post("/Identity/Login")]
    Task<UserOutputModel> Login([Body] Models.Identity.UserInputModel loginInput);

    [Get("/Users")]
    Task<IEnumerable<UserDetailsOutputModel>> GetAll();

    [Get("/Users/Partners")]
    Task<IEnumerable<UserDetailsOutputModel>> GetPartners();

    [Get("/Applications")]
    Task<IEnumerable<ApplicationDetailsOutputModel>> Applications();

    [Get("/Applications/{id}")]
    Task<ApplicationDetailsOutputModel> Details(int id);

    [Get("/Users/{id}")]
    Task<UserDetailsOutputModel> Details(string id);

    [Put("/Applications/{id}")]
    Task<bool> Edit(int id, ApplicationDetailsOutputModel application);

    [Put("/Users/{id}")]
    Task<bool> EditUser(string id, EditUserInputModel model);

    [Delete("/Users/{id}")]
    Task Delete(string id);

    [Post("/Identity/Register")]
    Task<UserOutputModel> Register(UserInputModel input);
}