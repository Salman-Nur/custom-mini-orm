using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3.TestCase1
{
    public class Item : IEntity<Guid>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public List<Color> Colors { get; set; }
        public List<Feedback> Feedbacks { get; set; }
    }
}
