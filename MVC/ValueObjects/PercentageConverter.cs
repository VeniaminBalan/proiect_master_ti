using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MVC.ValueObjects;


public class PercentageConverter() : ValueConverter<Percentage, double>(p => p.Value, v => new Percentage(v));