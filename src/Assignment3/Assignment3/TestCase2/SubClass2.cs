using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3.TestCase2
{
    public class SubClass2
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public string Property1 { get; set; }
        public string Property2 { get; set; }
    }
}
