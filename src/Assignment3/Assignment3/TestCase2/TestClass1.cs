using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3.TestCase2
{ 
    public class TestClass1 : IEntity<Guid>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public SubClass2 Class2 { get; set; }
        public List<Temp1> Temps { get; set; }
    }
}
