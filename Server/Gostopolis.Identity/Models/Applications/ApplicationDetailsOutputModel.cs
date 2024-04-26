namespace Gostopolis.Identity.Models.Applications;

using AutoMapper;
using Data.Models;
using Gostopolis.Data;
using Gostopolis.Models;

public class ApplicationDetailsOutputModel : IMapFrom<Application>
{
    public int Id { get; set; }

    public string UserId { get; set; }

    public ApplicationStatus? Status { get; set; }

    public int? StatusInt { get; set; }

    public string DocumentUrl { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ApprovedOn { get; set; }

    public DateTime? DeclinedOn { get; set; }

    public void Mapping(Profile mapper)
        => mapper
            .CreateMap<Application, ApplicationDetailsOutputModel>()
            .ForMember(a => a.StatusInt, o => o.Ignore());
}
