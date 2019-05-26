using System;
using System.Collections;
using System.Collections.Generic;

namespace AndriiCoursework.Models.Static
{
    public static class StaticData
    {
        public static DateTime DateOfElection = new DateTime(2019, 3, 30);

        public static IEnumerable<Candidate> CandidatesList = new List<Candidate>
        {
            new Candidate {FirstName = "Mark", LastName = "Levis", Info = "Good man"},
            new Candidate {FirstName = "Anna", LastName = "Johnson", Info = "Good girl"},
            new Candidate {FirstName = "Sam", LastName = "Smith", Info = "Kind heart"},
            new Candidate {FirstName = "Olga", LastName = "Koval", Info = "Hardworking and persistent"}
        };
    }
}
