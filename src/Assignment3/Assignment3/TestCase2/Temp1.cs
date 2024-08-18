using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3.TestCase2
{
    public class Temp1
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public List<Temp2> Temps { get; set; }
    }
}
