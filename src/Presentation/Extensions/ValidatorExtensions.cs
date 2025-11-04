using System.Security.Claims;
using Core.Exceptions;
using Domain.Enum;
using Microsoft.AspNetCore.Server.HttpSys;

namespace Presentation.Extensions;

public static class ValidatorExtension
{
    public static int ValidateRoleAndId(
        ClaimsPrincipal user,
        int? requiredId,
        bool onlyId,
        RolesEnum requiredRole
    )
    {
        var userId = user.FindFirst("id")?.Value;
        var userRole = user.FindFirst(ClaimTypes.Role)?.Value;

        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userRole))
            throw new AppValidationException("Missing data in token");

        RolesEnum userRoleType = (RolesEnum)int.Parse(userRole);
        int userIdInt = int.Parse(userId);

        bool hasRole = userRoleType >= requiredRole;
        bool hasId = userIdInt == requiredId;

        if (onlyId && hasId)
        {
            return userIdInt;
        }

        if (hasRole || hasId)
        {
            return userIdInt;
        }

        throw new AppValidationException("Unautorized");
    }
}
