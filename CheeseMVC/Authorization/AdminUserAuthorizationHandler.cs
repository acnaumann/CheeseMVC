using System;

using System.Threading.Tasks;
using CheeseMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace CheeseMVC.Authorization
{
    public class AdminUserAuthorizationHandler
        // this is saying 
        :AuthorizationHandler<OperationAuthorizationRequirement, Document>
    {

        UserManager<IdentityUser> _userManager;


        public AdminUserAuthorizationHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            //which operation
            OperationAuthorizationRequirement requirement,
            //on what resource(what piece of data)
            Document resource)
        {               //inclass example is: resource == null
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            //if current logged in user is in Admin role then we succeed requirement
            if (context.User.IsInRole(Constants.AdministratorsRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

    }
}
