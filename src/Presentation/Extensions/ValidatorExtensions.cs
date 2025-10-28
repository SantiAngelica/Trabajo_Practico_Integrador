using System.Security.Claims;
using Core.Exceptions;
using Domain.Enum;
using Microsoft.AspNetCore.Server.HttpSys;

namespace Presentation.Extensions;

public static class ValidatorExtension
{
    public static string ValidateRoleAndId(
        ClaimsPrincipal user,
        string requiredId,
        bool onlyId,
        RolesEnum requiredRole
    )
    {
        var userId = user.FindFirst("id")?.Value;
        var userRole = user.FindFirst("role")?.Value;

        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userRole))
            throw new AppValidationException("Missing data in token");

        RolesEnum userRoleType = (RolesEnum)int.Parse(userRole);

        bool hasRole = userRoleType >= requiredRole;
        bool hasId = userId == requiredId;

        if (onlyId && hasId)
        {
            return userId;
        }

        if (hasRole || hasId)
        {
            return userId;
        }

        throw new AppValidationException("Unautorized");
    }
}
