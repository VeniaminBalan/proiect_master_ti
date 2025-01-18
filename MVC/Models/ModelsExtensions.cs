using MVC.Entities;

namespace MVC.Models;

public static class ModelsExtensions
{
    public static AngajatViewModel ToViewModel(this Angajat angajat)
    {
        return new AngajatViewModel
        {
            Id = angajat.Id,
            Nume = angajat.Nume,
            Prenume = angajat.Prenume,
            Functie = angajat.Functie,
            SalarBaza = angajat.SalarBaza,
            SporProcentual = (int)angajat.SporProcentual.Value,
            PremiiBrute = angajat.PremiiBrute,
            Retineri = angajat.Retineri
        };
    }  
    
    public static UpdateAngajatViewModel ToUpdateViewModel(this Angajat angajat)
    {
        return new UpdateAngajatViewModel
        {
            Id = angajat.Id,
            Nume = angajat.Nume,
            Prenume = angajat.Prenume,
            Functie = angajat.Functie,
            SalarBaza = angajat.SalarBaza,
            SporProcentual = (int)angajat.SporProcentual.Value,
            PremiiBrute = angajat.PremiiBrute,
            Retineri = angajat.Retineri
        };
    }
    
}