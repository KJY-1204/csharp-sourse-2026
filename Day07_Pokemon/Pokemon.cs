namespace Day07_Pokemon;

public class Pokemon
{
    public int  Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double HeightMeter => HeightDecimeter / 10.0;
    public double WeightKg => WeightHectogram / 10.0;
    public int HeightDecimeter { get; init; }
    public int WeightHectogram { get; init; }
    public string? ImageUrl { get; init; }
    public IReadOnlyList<string> Types { get; init; } = Array.Empty<string>();
}