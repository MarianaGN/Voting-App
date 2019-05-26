using System;

namespace AndriiCoursework.Models
{
    public class BallotForm
    {
        public Guid Id { get; set; }

        public BallotForm()
        {
            Id = Guid.NewGuid();
        }

        public DateTime DateOfElection { get; set; }

        public DateTime UpdateDate { get; set; }

        public Guid? CandidateId { get; set; }

        public Guid ElectorId { get; set; }

        public Elector Elector { get; set; }

    }
}
