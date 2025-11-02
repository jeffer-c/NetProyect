namespace NetProyect.Domain.Entities;

public class Industry
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public ICollection<ForbesList> Forbes { get; set; } = new List<ForbesList>();
}