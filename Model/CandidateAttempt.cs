using System;

namespace NEWPROJ.Models
{
    public class CandidateAttempt
    {
        public int Id { get; set; }

        public int CandidateId { get; set; }

        public int TestId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }   // Nullable because DB me NULL allowed hai

        public string Status { get; set; }
    }
}
