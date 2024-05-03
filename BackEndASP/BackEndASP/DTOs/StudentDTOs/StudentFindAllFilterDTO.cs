using BackEndASP.DTOs.ImageDTOs;
using Microsoft.IdentityModel.Tokens;

namespace BackEndASP.DTOs.StudentDTOs
{
    public class StudentFindAllFilterDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string College { get; set; } = "";
        public ImageUserDTO Image { get; set; }
        public List<string> Hobbies { get; set; } = new List<string>();
        public List<string> Personalitys { get; set; } = new List<string>();



        public StudentFindAllFilterDTO()
        {

        }


        public StudentFindAllFilterDTO(Student entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name.ToUpper();
            this.Email = entity.Email;
            this.Age = CalcAge(entity.BirthDate, DateTimeOffset.Now);
            this.College = entity.College == null ? "" : entity.College.Name;
            this.Image = entity.Image != null ? new ImageUserDTO(entity.Image) : null;
            this.Hobbies = entity.Hobbies.ToList();
            this.Personalitys = entity.Personalities.ToList();
        }



        private int CalcAge(DateTimeOffset dataNascimento, DateTimeOffset dataAtual)
        {
            int idade = dataAtual.Year - dataNascimento.Year;

            if (dataAtual < dataNascimento.AddYears(idade))
            {
                idade--;
            }

            return idade;
        }

    }
}
