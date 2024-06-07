
public class College : Building
{
    public ICollection<Student>? Students { get; set; } = new List<Student>();
}