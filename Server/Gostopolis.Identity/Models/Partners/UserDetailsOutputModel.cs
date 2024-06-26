﻿namespace Gostopolis.Identity.Models.Partners;

using AutoMapper;
using Data.Models;
using Gostopolis.Models;

public class UserDetailsOutputModel : IMapFrom<User>
{
    public string Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string ImageUrl { get; set; }

    public bool LoginNotification { get; set; }

    public void Mapping(Profile mapper)
        => mapper
            .CreateMap<User, UserDetailsOutputModel>();
}
