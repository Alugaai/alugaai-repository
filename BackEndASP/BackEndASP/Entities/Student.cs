using BackEndASP.Entities;

public class Student : User
{
    public List<string>? Personalities { get; set; } = new List<string>();
    public List<string>? Hobbies { get; set; } = new List<string>();
    public List<UserConnection>? Connections { get; set; } = new List<UserConnection>();
    public List<string>? IdsPersonsIConnect { get; set; } = new List<string>();
    public List<string>? PendentsConnectionsId { get; set; } = new List<string>();
    public int? CollegeId { get; set; }
    public College? College { get; set; }
    public List<PropertyStudentLikes>? PropertiesLikes { get; set; } = new List<PropertyStudentLikes>();

}
