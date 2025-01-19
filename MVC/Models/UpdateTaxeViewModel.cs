using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class UpdateTaxeViewModel
{
    [Range(0, 100, ErrorMessage = "CAS trebuie să fie între 0% și 100%.")]
    public int CAS { get; set; }
    [Range(0, 100, ErrorMessage = "CASS trebuie să fie între 0% și 100%.")]
    public int CASS { get; set; }
    [Range(0, 100, ErrorMessage = "Impozitul trebuie să fie între 0% și 100%.")]
    public int Impozit { get; set; }

    [Required]
    public string Password { get; init; }
}