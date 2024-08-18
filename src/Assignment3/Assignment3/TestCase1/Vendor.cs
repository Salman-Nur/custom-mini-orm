using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3.TestCase1;

public class Vendor : IEntity<Guid>
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [NotMapped]
    private string _name;

    public Guid Id { get; set; }

    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }
    public bool Enlisted { get; set; }

    public int GetRatingScore()
    {
        throw new NotImplementedException();
    }
}