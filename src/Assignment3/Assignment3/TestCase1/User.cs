using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3.TestCase1
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
