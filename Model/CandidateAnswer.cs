using System;

namespace NEWPROJ.Model
{
    public class CandidateAnswer
    {
        public int Id { get; set; }

        public int CandidateId { get; set; }

        public int TestQuestionId { get; set; }

        public int OptionId { get; set; }
    }
}
