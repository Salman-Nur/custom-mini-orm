using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3.TestCase1
{
    public class Feedback
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public User FeedbackGiver { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
    }
}
