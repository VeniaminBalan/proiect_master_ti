using MVC.ValueObjects;

namespace MVC.Entities;

public class Taxe : BaseEntity<int>
{
    public Percentage CAS { get; set; }
    public Percentage CASS { get; set; }
    public Percentage Impozit { get; set; }
    
    public Taxe(Percentage cas, Percentage cass, Percentage impozit, int id) : base(id)
    {
        CAS = cas;
        CASS = cass;
        Impozit = impozit;
    }
    
    private Taxe() { }
}