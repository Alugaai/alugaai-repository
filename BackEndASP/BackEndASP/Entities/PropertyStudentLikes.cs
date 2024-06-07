namespace BackEndASP.Entities
{
    public class PropertyStudentLikes
    {
        public string? StudentId { get; set; }
        public Student? Student { get; set; }

        public int? PropertyId { get; set; }
        public Property? Property { get; set; }

    }
}
