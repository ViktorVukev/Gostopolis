namespace Gostopolis.Infrastructure;

using Microsoft.AspNetCore.Authorization;
using static Constants;

/// <inheritdoc />
public class AuthorizeAdministratorAttribute : AuthorizeAttribute
{
    /// <inheritdoc />
    public AuthorizeAdministratorAttribute() => this.Roles = AdministratorRoleName;
}