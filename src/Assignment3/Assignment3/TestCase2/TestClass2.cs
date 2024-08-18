using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3.TestCase2
{
    public class TestClass2 : IEntity<Guid>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public SubClass1 Class1 { get; set; }
        public List<SubClass2> SubClasses { get; set; }
        public List<Temp1> Temps { get; set; }
    }
}
