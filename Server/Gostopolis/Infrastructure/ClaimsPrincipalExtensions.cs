﻿namespace Gostopolis.Infrastructure;

using System.Security.Claims;
using static Constants;

public static class ClaimsPrincipalExtensions
{
    public static bool IsAdministrator(this ClaimsPrincipal user)
        => user.IsInRole(AdministratorRoleName);
}