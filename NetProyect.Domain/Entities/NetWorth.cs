namespace NetProyect.Domain.Entities;

public class NetWorth
{
    public int Id { get; set; }
    public decimal? Original { get; set; }
    public decimal? Number { get; set; }
    public string? Currency { get; set; }
    public decimal? EstWorthPrev { get; set; }
    public decimal? FinalWorth { get; set; }
    public string? Formatted { get; set; }
}