
using BackEndASP.Entities;

public class Property : Building
{
        public double Price {  get; set; }
        public string Bedrooms { get; set; }
        public string Bathrooms { get; set; }
        public string Description { get; set; }

        public string OwnerId { get; set; }
        public Owner Owner { get; set; }

        public ICollection<PropertyStudentLikes>? StudentLikes { get; set; } = new List<PropertyStudentLikes>();
        


}

