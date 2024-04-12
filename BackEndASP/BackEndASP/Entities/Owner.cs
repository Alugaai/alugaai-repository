namespace BackEndASP.Entities;

    public class Owner : User
    {

        public List<Property>? Properties { get;} = new List<Property>();
    }
