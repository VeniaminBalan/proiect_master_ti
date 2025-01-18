using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Playwright;
using MVC.Repositories;

namespace MVC.Controllers;

[Route("[controller]")]
public class TiparireController : Controller
{
    
    private readonly AngajatiRepository repository;
    
    public TiparireController(AngajatiRepository repository)
    {
        this.repository = repository;
    }
    
    [HttpPost("StatPlata")]
    public async Task<ActionResult> StatPlata2Pdf()
    {
        var angajati = await repository.GetAll();
        
        var rawHtml = await RenderViewToRawHtml("_Report", angajati.ToList());

        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });
        
        var page = await browser.NewPageAsync();
        await page.SetContentAsync(rawHtml);
       
        var buffer =await page.PdfAsync(new PagePdfOptions
        {
            Format = "A4",
            Margin = new()
            {
                Top = "1cm",
                Bottom = "1cm",
                Left = "1cm",
                Right = "1cm"
            }
        });
        
        
        return File(buffer, "application/pdf", "report.pdf");
    }
    
    [HttpGet("StatPlata")]
    public async Task<ActionResult> StatPlata()
    {
        var angajat = await repository.GetAll();
        
        return View(angajat.ToList());
    }
    
    [HttpGet("Fluturasi/{searchTerm?}")]
    public async Task<ActionResult> Fluturasi(string searchTerm)
    {
        var angajati = await repository.GetAll(searchTerm);
        
        return View(angajati.ToList());
    }

    [HttpPost("Fluturasi2Pdf")]
    public async Task<ActionResult> Fluturasi2Pdf(string? searchTerm)
    {
        var angajati = await repository.GetAll(searchTerm);
        
        var rawHtml = await RenderViewToRawHtml("_Fluturasi", angajati.ToList());

        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });
        
        var page = await browser.NewPageAsync();
        await page.SetContentAsync(rawHtml);
       
        var buffer =await page.PdfAsync(new PagePdfOptions
        {
            Format = "A4",
            Margin = new()
            {
                Top = "1cm",
                Bottom = "1cm",
                Left = "1cm",
                Right = "1cm"
            }
        });
        
        
        return File(buffer, "application/pdf", "fluturasi.pdf");
    }
    

    
    private async Task<string> RenderViewToRawHtml(string viewTemplate ,object? model)
    {
        var viewName = viewTemplate; // Replace with your actual view name
        var viewResult = this.ControllerContext
            .HttpContext
            .RequestServices
            .GetService<IRazorViewEngine>()
            ?.FindView(ControllerContext, viewName, false);

        if (!viewResult.Success)
        {
            throw new InvalidOperationException($"The view '{viewName}' was not found.");
        }

        ViewData.Model = model;
        
        var stringWriter = new StringWriter();
        var viewContext = new ViewContext(
            ControllerContext,
            viewResult.View,
            ViewData,
            TempData,
            stringWriter,
            new HtmlHelperOptions()
        );
        
        await viewResult.View.RenderAsync(viewContext);
        return stringWriter.ToString();
    }
}