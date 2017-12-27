using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

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

        [Required]
        [DataType(DataType.Text)]
        string FirstName;

        [Required]
        [DataType(DataType.Text)]
        string LastName;
    }
}
