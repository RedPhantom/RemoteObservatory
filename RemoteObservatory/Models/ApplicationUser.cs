﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RemoteObservatory.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Specifies the permission level of a user. 
        /// </summary>
        enum Role
        {
            Student,
            Teacher,
            Staff,
            Admin
        }

        string FirstName;

        string LastName;
    }
}
