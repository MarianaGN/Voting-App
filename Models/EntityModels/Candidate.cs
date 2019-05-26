using System;

namespace AndriiCoursework.Models
{
    public class Candidate
    {
        public Guid Id { get; set; }

        public Candidate()
        {
            Id = Guid.NewGuid();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Info { get; set; }
    }
}
