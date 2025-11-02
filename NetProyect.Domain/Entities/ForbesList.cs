namespace NetProyect.Domain.Entities;

public class ForbesList
{
    public int Id { get; set; }
    public string Uri { get; set; } = default!;
    public int Rank { get; set; }
    public int IndustryId { get; set; }
    public Industry Industry { get; set; } = default!;
    public int ProfileId { get; set; }
    public Profile Profile { get; set; } = default!;
    public int NetWorthId { get; set; }
    public NetWorth NetWorth { get; set; } = default!;
}