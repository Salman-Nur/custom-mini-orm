using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3.TestCase2
{
    public class Temp2
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public Temp3 Temp { get; set; }
    }
}