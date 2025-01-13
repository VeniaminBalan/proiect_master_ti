namespace MVC.ValueObjects;

using System;

public class Percentage : IEquatable<Percentage>
{
    public double Value { get; }

    // Constructor
    public Percentage(double value)
    {
        if (value < 0 || value > 100)
            throw new ArgumentOutOfRangeException(nameof(value), "Percentage must be between 0 and 100.");
        
        Value = value;
    }

    // Method to increase a value by the percentage
    public double Increase(double baseValue)
    {
        return baseValue + (baseValue * Value / 100);
    }

    // Method to decrease a value by the percentage
    public double Decrease(double baseValue)
    {
        return baseValue - (baseValue * Value / 100);
    }

    // Method to calculate a percentage of a given value
    public double Of(double baseValue)
    {
        return baseValue * Value / 100;
    }

    // Override equality methods for value comparison
    public override bool Equals(object? obj)
    {
        return Equals(obj as Percentage);
    }

    public bool Equals(Percentage? other)
    {
        if (other is null) return false;
        return Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    // Override ToString for display purposes
    public override string ToString()
    {
        return $"{Value}%";
    }

    // Static factory method for clarity
    public static Percentage FromValue(double value)
    {
        return new Percentage(value);
    }
}
