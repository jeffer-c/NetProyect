namespace NetProyect.Domain.Entities;

public class Profile
{
    public int Id { get; set; }
    public string PersonName { get; set; } = default!;
    public string? LastName { get; set; }
    public string? Gender { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string? CountryOfCitizenship { get; set; }
    public string? Source { get; set; }
    public string? SquareImage { get; set; }
    public bool ImageExists { get; set; }
}