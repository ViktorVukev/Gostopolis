namespace Gostopolis.Accommodations.Services.AccommodationType;

using AutoMapper;
using Data;
using Data.Models;
using Gostopolis.Services;
using Microsoft.EntityFrameworkCore;
using Models.AccommodationTypes;

public class AccommodationTypeService(
        AccommodationsDbContext db,
        IMapper mapper) 
    : DataService<AccommodationType>(db), IAccommodationTypeService
{
    public async Task<int> Create(AccommodationTypeInputModel input)
    {
        var accommodationType = new AccommodationType(input.Name, input.Description);

        await this.Save(accommodationType);

        return accommodationType.Id;
    }

    public async Task<IEnumerable<AccommodationTypeOutputModel>> GetAll()
        => await mapper
            .ProjectTo<AccommodationTypeOutputModel>(this.All())
            .ToListAsync();

    public async Task<AccommodationType> FindById(int id)
        => await this.Data.FindAsync<AccommodationType>(id);

    public async Task<bool> Edit(int id, AccommodationTypeInputModel input)
    {
        var accommodationType = await this.FindById(id);

        accommodationType.Name = input.Name;
        accommodationType.Description = input.Description;

        await this.Save(accommodationType);

        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var accommodationType = await this.FindById(id);

        this.Data.Remove(accommodationType);

        await this.Data.SaveChangesAsync();

        return true;
    }
}
