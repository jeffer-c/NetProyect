namespace NetProyect.Application.Dtos;

public class ProfileDto
{
    public string? personName { get; set; }
    public string? lastName { get; set; }
    public string? gender { get; set; }
    public string? countryOfCitizenship { get; set; }
    public string? source { get; set; }
    public string? squareImage { get; set; }
    public bool imageExists { get; set; }
    public string? birthDate { get; set; } // ISO string; se convertirá a DateOnly?
}