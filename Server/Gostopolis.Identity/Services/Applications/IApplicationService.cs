namespace Gostopolis.Identity.Services.Applications;

using Data.Models;
using Gostopolis.Services;
using Models.Applications;

public interface IApplicationService : IDataService<Application>
{
    Task<IList<Application>> FindByUser(string userId);

    Task<IEnumerable<ApplicationDetailsOutputModel>> GetAll();

    Task<ApplicationDetailsOutputModel> GetDetails(int id);

    Task<Application> FindById(int id);
}
