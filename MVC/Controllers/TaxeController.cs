using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;
using MVC.ValueObjects;

namespace MVC.Controllers;

[Route("ModificareProcente")]
public class TaxeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly string _password;
    
    public TaxeController(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _password = configuration.GetSection("AdminPassword").Value ?? throw new ArgumentNullException("AdminPassword");
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var taxe = await _context.Taxe.FirstOrDefaultAsync();
        
        return View(taxe);
    }
    
    [HttpPost]
    public IActionResult Index([FromBody]UpdateTaxeViewModel model)
    {
        Console.WriteLine(JsonSerializer.Serialize(model));
        
        if(model.Password != _password)
            return Unauthorized();
        
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(ms => ms.Value?.Errors.Count > 0)
                .Select(ms => new
                {
                    Field = ms.Key,
                    Errors = ms.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                })
                .ToArray();

            return BadRequest(new
            {
                success = false,
                message = "Validation errors occurred.",
                errors
            });
        }
        
        var taxe = _context.Taxe.FirstOrDefault();
        
        if(taxe == null)
            return RedirectToAction("Index");
        
        taxe.CAS = new Percentage(model.CAS);
        taxe.CASS = new Percentage(model.CASS);
        taxe.Impozit = new Percentage(model.Impozit);
            
        _context.SaveChanges();
        
        return Ok(taxe);
    }
}