using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class UpdateAngajatViewModel
{
    public int Id { get; set; }
    
    [Required]
    public string Nume { get; set; }

    [Required]
    public string Prenume { get; set; }

    [Required]
    public string Functie { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Salarul trebuie să fie un număr pozitiv.")]
    public int SalarBaza { get; set; }

    [Required]
    [Range(0, 100, ErrorMessage = "Sporul trebuie să fie între 0% și 100%.")]
    public int SporProcentual { get; set; } // Input as percentage (0-100)

    [Range(0, int.MaxValue, ErrorMessage = "Premiile brute trebuie să fie un număr pozitiv.")]
    public int PremiiBrute { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Retinerile trebuie să fie un număr pozitiv.")]
    public int Retineri { get; set; }
}