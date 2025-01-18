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
    
    public TaxeController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var taxe = await _context.Taxe.FirstOrDefaultAsync();
        
        return View(taxe);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(UpdateTaxeViewModel model)
    {
        if (!ModelState.IsValid)
            return RedirectToAction("Index");
        
        var taxe = _context.Taxe.FirstOrDefault();
        
        if(taxe == null)
            return RedirectToAction("Index");
        
        taxe.CAS = new Percentage(model.CAS);
        taxe.CASS = new Percentage(model.CASS);
        taxe.Impozit = new Percentage(model.Impozit);
            
        _context.SaveChanges();
        
        return View(taxe);
    }
}