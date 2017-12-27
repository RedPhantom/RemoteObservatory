using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RemoteObservatory.Models.Astronomy;
using Microsoft.AspNetCore.Identity;
using RemoteObservatory.Models;
using Microsoft.VisualBasic;

namespace RemoteObservatory.Authorization
{
    public class UserIsOwnerAuthHandler : AuthorizationHandler<OperationAuthorizationRequirement, ObservationModel>
    {
        UserManager<ApplicationUser> _userManager;

        public UserIsOwnerAuthHandler(UserManager<ApplicationUser>
            userManager)
        {
            _userManager = userManager;
        }

        protected override Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                                   OperationAuthorizationRequirement requirement,
                                   ObservationModel resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.FromResult(0);
            }

            // If we're not asking for CRUD permission, return.
            /*
            if (requirement.Name != Constants.CreateOperationName &&
                requirement.Name != Constants.ReadOperationName &&
                requirement.Name != Constants.UpdateOperationName &&
                requirement.Name != Constants.DeleteOperationName)
            {
                return Task.FromResult(0);
            }
            */
            if (resource.OwnerID == _userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }
}
