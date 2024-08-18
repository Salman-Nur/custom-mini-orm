using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3.TestCase2
{
    public class Temp3
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        private DateTime Timestamp { get;set; }
    }
}