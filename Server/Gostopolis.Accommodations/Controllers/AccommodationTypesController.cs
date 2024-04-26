namespace Gostopolis.Accommodations.Controllers;

using Data.Models;
using Gostopolis.Controllers;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models.AccommodationTypes;
using Services.AccommodationType;

public class AccommodationTypesController(
        IAccommodationTypeService accommodationTypes) 
    : ApiController
{
    [HttpPost]
    [AuthorizeAdministrator]
    public async Task<ActionResult<int>> Create(AccommodationTypeInputModel input)
        => Ok(await accommodationTypes.Create(input));

	[HttpGet]
	public async Task<IEnumerable<AccommodationTypeOutputModel>> All()
		=> await accommodationTypes.GetAll();

    [HttpPut]
    [Route(Id)]
    [AuthorizeAdministrator]
    public async Task<ActionResult> Edit(int id, AccommodationTypeInputModel input)
        => Ok(await accommodationTypes.Edit(id, input));

    [HttpDelete]
    [Route(Id)]
    [AuthorizeAdministrator]
    public async Task<ActionResult> Delete(int id)
        => Ok(await accommodationTypes.Delete(id));
}
