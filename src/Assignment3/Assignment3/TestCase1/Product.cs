using Assignment3;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3.TestCase1
{
    public class Product : IEntity<Guid>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [NotMapped]
        private string SKU { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public List<Color> Colors { get; set; }
        public List<Feedback> Feedbacks { get; set; }
    }
}
