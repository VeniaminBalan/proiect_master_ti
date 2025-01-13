using MVC.ValueObjects;

namespace MVC.Entities;

public class Angajat : BaseEntity<int>
{
    public string Nume { get; set; }
    public string Prenume { get; set; }
    public string Functie { get; set; }
    public int SalarBaza { get; set; }
    public Percentage SporProcentual { get; set; }
    public int PremiiBrute { get; set; }
    public int TotalBrut { get; set; }
    public int BrutImpozitabil { get; set; }
    public int Impozit { get; set; }
    public int CAS { get; set; }
    public int CASS { get; set; }
    public int Retineri { get; set; }
    public int ViratCard { get; set; }

    public Angajat(string nume, string prenume, string functie, int salarBaza, Percentage spor, int premiiBrute, int retineri)
    {
        Nume = nume;
        Prenume = prenume;
        Functie = functie;
        SalarBaza = salarBaza;
        SporProcentual = spor;
        PremiiBrute = premiiBrute;
        Retineri = retineri;
    }
    
    private Angajat() { }

}