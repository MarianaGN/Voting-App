using System;
using Microsoft.AspNetCore.Identity;

namespace AndriiCoursework.Models
{
    public class Elector : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Password { get; set; }

        public string PassportNumber { get; set; }

        public BallotForm BallotForm { get; set; }
    }
}
