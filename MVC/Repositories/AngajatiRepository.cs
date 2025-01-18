using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Entities;

namespace MVC.Repositories;

public class AngajatiRepository(ApplicationDbContext context)
{
    public async Task<IEnumerable<Angajat>> GetAll(string? searchString = "")
    {
        var query = context.Angajati.AsQueryable();
        
        if(!string.IsNullOrWhiteSpace(searchString))
        {
            query = query.Where(a => a.Nume.Contains(searchString) || a.Prenume.Contains(searchString));
        }
        
        return await query
            .ToListAsync();
    }
    
    public async Task<Angajat?> GetById(int id, bool trackChanges = false)
    {
        
        if (trackChanges)
        {
            return await context.Angajati
                .FindAsync(id);
        }
        
        return await context.Angajati
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        
    }
}