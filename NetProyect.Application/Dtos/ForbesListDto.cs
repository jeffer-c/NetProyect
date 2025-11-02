namespace NetProyect.Application.Dtos;

public class ForbesListDto
{
    public string uri { get; set; } = default!; // p.ej. "elon-musk"
    public int rank { get; set; }
    public string? industry { get; set; }
    public decimal? finalWorth { get; set; }
    public string? personName { get; set; }
}