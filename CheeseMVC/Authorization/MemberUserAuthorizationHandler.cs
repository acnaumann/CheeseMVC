using System;
using System.Threading.Tasks;
using CheeseMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace CheeseMVC.Authorization
{
    public class MemberUserAuthorizationHandler
        : AuthorizationHandler<OperationAuthorizationRequirement, Document>
    {
        UserManager<IdentityUser> _userManager;

        public MemberUserAuthorizationHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            Document resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }
            //a regular member can only do the CRUD operations below
            if (requirement.Name != Constants.CreateOperationName &&
                requirement.Name != Constants.ReadOperationName &&
                requirement.Name != Constants.UpdateOperationName &&
                requirement.Name != Constants.DeleteOperationName)
            {
                return Task.CompletedTask;
            }

            //does the resource user ID match our current user's ID they succeed
            if (resource.UserID == _userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
