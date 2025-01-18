using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Entities;
using MVC.Models;
using MVC.Repositories;
using MVC.ValueObjects;

namespace MVC.Controllers;

[Route("[controller]")]
public class AngajatiController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly AngajatiRepository _repository;
    
    public AngajatiController(ApplicationDbContext context, AngajatiRepository repository)
    {
        _context = context;
        _repository = repository;
    }
    
    public async Task<IActionResult> Index()
    {
        var angajati = await _context.Angajati.ToListAsync();
        return View(angajati);
    }
    
    [HttpGet("Adaugare")]
    public IActionResult Adaugare()
    {
        return View(new CreateAngajatViewModel());
    }
    
    [HttpPost("Adaugare")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Adaugare(CreateAngajatViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Convert ViewModel to Domain Model
        var angajat = new Angajat(
            model.Nume,
            model.Prenume,
            model.Functie,
            model.SalarBaza,
            new Percentage(model.SporProcentual),
            model.PremiiBrute,
            model.Retineri
        );

        // Add and save to the database
        _context.Angajati.Add(angajat);
        await _context.SaveChangesAsync();

        return RedirectToAction("Adaugare");
    }
    
    [HttpGet("Detalii/{id}")]
    public async Task<IActionResult> Detalii(int id)
    {
        var angajat = await _context.Angajati.FindAsync(id);
        return View(angajat);
    }
    
    // POST: Angajati/SearchAndEdit
    [HttpGet("Actualizare/{searchTerm?}")]
    public async Task<IActionResult> Actualizare(string searchTerm)
    {
        var angajati = await _repository.GetAll(searchTerm);
        
        return View(angajati.Select(a => a.ToUpdateViewModel()).ToList());
    }

    [HttpPost("SaveChanges")]
    public async Task<IActionResult> SaveChanges([FromBody] List<UpdateAngajatViewModel>? updatedAngajati)
    {
        if (updatedAngajati == null || !updatedAngajati.Any())
        {
            return BadRequest(new
            {
                success = false,
                message = "No data provided.",
            });
        }

        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(ms => ms.Value != null && ms.Value.Errors.Count > 0)
                .Select(ms => new
                {
                    Field = ms.Key,
                    Errors = ms.Value?.Errors.Select(e => e.ErrorMessage)
                });

            return BadRequest(new
            {
                success = false,
                message = "Validation errors occurred.",
                errors
            });
        }

        foreach (var updatedAngajat in updatedAngajati)
        {
            var existingAngajat = await _context.Angajati.FindAsync(updatedAngajat.Id);
            if (existingAngajat == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Angajat with ID {updatedAngajat.Id} not found."
                });
            }

            existingAngajat.Nume = updatedAngajat.Nume;
            existingAngajat.Prenume = updatedAngajat.Prenume;
            existingAngajat.Functie = updatedAngajat.Functie;
            existingAngajat.SalarBaza = updatedAngajat.SalarBaza;
            existingAngajat.SporProcentual = new Percentage(updatedAngajat.SporProcentual);
            existingAngajat.PremiiBrute = updatedAngajat.PremiiBrute;
            existingAngajat.Retineri = updatedAngajat.Retineri;
        }

        await _context.SaveChangesAsync();

        return Ok(new
        {
            success = true,
            message = "Changes saved successfully!"
        });
    }
    
    
    [HttpGet("Stergere/{searchTerm?}")]
    public async Task<IActionResult> Stergere(string searchTerm)
    {
        var angajati = await _repository.GetAll(searchTerm);
        
        return View(angajati.Select(a => a.ToViewModel()).ToList());
    }
    
    [HttpDelete("DeleteMultiple")]
    public async Task<IActionResult> DeleteMultiple([FromBody] List<int>? ids)
    {
        if (ids == null || !ids.Any())
        {
            return BadRequest(new { success = false, message = "No IDs were provided." });
        }

        var angajatiToDelete = await _context.Angajati.Where(a => ids.Contains(a.Id)).ToListAsync();
        if (!angajatiToDelete.Any())
        {
            return NotFound(new { success = false, message = "No matching employees found for deletion." });
        }

        _context.Angajati.RemoveRange(angajatiToDelete);
        await _context.SaveChangesAsync();

        return Ok(new { success = true, message = $"{angajatiToDelete.Count} employee(s) deleted successfully!" });
    }
    
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}