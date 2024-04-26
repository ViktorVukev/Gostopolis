namespace Gostopolis.Identity.Services.Applications;

using System.Linq.Expressions;
using AutoMapper;
using Data;
using Data.Models;
using Gostopolis.Services;
using Microsoft.EntityFrameworkCore;
using Models.Applications;

public class ApplicationService(
        IdentityDbContext db,
        IMapper mapper)
    : DataService<Application>(db), IApplicationService
{
    public async Task<IEnumerable<ApplicationDetailsOutputModel>> GetAll()
        => await mapper
            .ProjectTo<ApplicationDetailsOutputModel>(this.All())
            .ToListAsync();

    public async Task<ApplicationDetailsOutputModel> GetDetails(int id)
        => await mapper
            .ProjectTo<ApplicationDetailsOutputModel>(this
                .All()
                .Where(d => d.Id == id))
            .FirstOrDefaultAsync();

    public Task<IList<Application>> FindByUser(
        string userId)
        => this.FindByUser(userId, application => application);

    public async Task<Application> FindById(int id)
        => await this.Data.FindAsync<Application>(id);

    private async Task<IList<T>> FindByUser<T>(
        string userId,
        Expression<Func<Application, T>> selector)
    {
        var applicationData = await this
            .All()
            .Where(a => a.UserId == userId)
            .Select(selector)
            .ToListAsync();

        return applicationData;
    }
}
