using ApiCatalogo.Pagination;
using BackEndASP.DTOs.StudentDTOs;
using BackEndASP.Entities;
using BackEndASP.Interfaces;
using BackEndASP.Utils;
using Geocoding;
using Microsoft.EntityFrameworkCore;

namespace BackEndASP.Services
{
    public class StudentService : IStudentRepository
    {

        private readonly SystemDbContext _dbContext;

        public StudentService(SystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        // busca todos o usuários com filtros de paginação, idade, faculdade, etc
        public async Task<PagedList<StudentFindAllFilterDTO>> FindAllStudentsAsync(PageStudentQueryParams pageQueryParams, string userId)
        {
            DateTime initialAge = DateTime.Now.AddYears(-pageQueryParams.InitialAge);
            DateTime finalAge = DateTime.Now.AddYears(-pageQueryParams.FinalAge);


            IQueryable<Student> query = _dbContext.Students.Include(s => s.College).AsNoTracking();


            if (pageQueryParams.OwnCollege)
            {
                Student student = await query.FirstOrDefaultAsync(s => s.Id == userId) ?? throw new ArgumentException($"This id {userId} does not exist");

                int? collegeId = student.CollegeId;

                query = query.Where(s => s.CollegeId == collegeId);
            }

            if (!string.IsNullOrEmpty(pageQueryParams.Name))
            {
                query = query.Where(s => s.UserName.ToUpper().Contains(pageQueryParams.Name.ToUpper()));
            }

            if (pageQueryParams.Interests.Any(interest => !string.IsNullOrWhiteSpace(interest)))
            {
                query = query.Where(s =>
                pageQueryParams.Interests.Any(interest =>
                s.Hobbies.Any(hobby => hobby.Contains(interest)) ||
                s.Personalitys.Any(personality => personality.Contains(interest)))
                );

            }

            query = query.Where(s => s.BirthDate <= initialAge && s.BirthDate >= finalAge).Include(s => s.Image);

            if (userId != null)
            {
                query = query.Where(s => s.Id != userId);
            }

            var result = await PaginationHelper.CreateAsync(query, pageQueryParams.PageNumber, pageQueryParams.PageSize);
            var resultDTO = result.Select(s => new StudentFindAllFilterDTO(s));

            return new PagedList<StudentFindAllFilterDTO>(resultDTO, pageQueryParams.PageNumber, pageQueryParams.PageSize, result.TotalCount);

        }




        // busca todas minhas conexões
        public StudentsConnectionsDTO FindAllMyStudentsConnections(string userId)
        {
            Student student = _dbContext.Students
                .Include(s => s.Connections)
                .AsNoTracking().FirstOrDefault(s => s.Id == userId)
                ?? throw new ArgumentException($"This id {userId} does not exist");

            return new StudentsConnectionsDTO(student.Connections);
        }

        

        //busca todas as solicitações que eu tenho pendente
        public StudentsConnectionsDTO FindMyAllStudentsWhoInvitationsConnections(string userId)
        {
            Student student = _dbContext.Students.AsNoTracking().FirstOrDefault(s => s.Id == userId)
                ?? throw new ArgumentException($"This id {userId} does not exist");
            
            return new StudentsConnectionsDTO(student.PendentsConnectionsId);
        }



        // adicionando o ID de um estudante como pedido de conexão
        public async Task GiveConnectionOrder(string userId, string studentForConnectionId)
        {
            Student studentForConnection = await _dbContext.Students.FindAsync(studentForConnectionId)
                ?? throw new ArgumentException($"User with id {studentForConnectionId} does not exist");

            var user = await _dbContext.Users.FindAsync(userId)
                ?? throw new ArgumentException($"User with id {userId} does not exist");

            var userNotification = new UserNotifications
            {
                User = user,
                Notification = new Notification
                {
                    Moment = DateTimeOffset.Now,
                    Read = false,
                    Text = $"O usuário {user.UserName.ToUpper()} enviou a você um pedido de conexão!"
                }
            };

            // Add the UserNotification to the appropriate collections
            studentForConnection.UserNotifications.Add(userNotification);
            studentForConnection.PendentsConnectionsId.Add(userId);
            _dbContext.Update(studentForConnection);
        }


        // estudante aceitando ou recusando o pedido de conexão
        public async Task<bool> HandleConnection(string userId, StudentConnectionInsertDTO dto, int notificationId)
        {
            // Encontrar o estudante atual
            Student actualStudent = await _dbContext.Students
                .Include(s => s.UserNotifications)
                .FirstOrDefaultAsync(s => s.Id == userId)
                ?? throw new ArgumentException($"This id {userId} does not exist");

            // Encontrar a notificação
            Notification notification = await _dbContext.Notifications
                .FirstOrDefaultAsync(s => s.Id == notificationId)
                ?? throw new ArgumentException($"This id {notificationId} does not exist");

            // Marcar a notificação como lida
            notification.Read = true;

            // Remover a conexão de PendentsConnectionsId
            if (actualStudent.PendentsConnectionsId != null && actualStudent.PendentsConnectionsId.Contains(dto.ConnectionWhyIHandle))
            {
                actualStudent.PendentsConnectionsId.Remove(dto.ConnectionWhyIHandle);

                if (dto.Action)
                {
                    // Se a ação for verdadeira, a conexão é aceita
                    UserConnection userConnection = new UserConnection
                    {
                        StudentId = userId,
                        OtherStudentId = dto.ConnectionWhyIHandle
                    };

                    // Criar a notificação
                    Notification sendNotificationForUser = new Notification
                    {
                        Moment = DateTimeOffset.Now,
                        Read = false,
                        Text = $"O usuário {actualStudent.UserName.ToUpper()} aceitou o seu pedido de conexão!"
                    };

                    // Adicionar a notificação ao contexto do banco de dados
                    _dbContext.Notifications.Add(sendNotificationForUser);

                    // Salvar as alterações para obter o ID da notificação atribuído automaticamente
                    await _dbContext.SaveChangesAsync();

                    // Criar o relacionamento entre o usuário e a notificação
                    var userNotification = new UserNotifications
                    {
                        UserId = dto.ConnectionWhyIHandle,
                        NotificationId = sendNotificationForUser.Id // Usar o ID atribuído da nova notificação
                    };

                    // Adicionar a notificação ao conjunto de notificações do outro estudante
                    Student otherStudent = await _dbContext.Students
                        .FirstOrDefaultAsync(s => s.Id == dto.ConnectionWhyIHandle)
                        ?? throw new ArgumentException($"This id {userId} does not exist");

                    otherStudent.UserNotifications.Add(userNotification);

                    // Adicionar a nova conexão ao contexto do banco de dados
                    await _dbContext.UserConnections.AddAsync(userConnection);
                    _dbContext.Students.Update(otherStudent);
                }

                // Atualizar o estudante atual no contexto do banco de dados
                _dbContext.Students.Update(actualStudent);

                // Salvar as alterações no banco de dados
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> CompleteProfileStudent(string userId, StudentCompleteProfileDTO dto)
        {
            var student = await _dbContext.Students.FindAsync(userId)
                ?? throw new ArgumentException($"User with id {userId} does not exist");

            await InsertDTOToStudentAsync(dto, student);

            _dbContext.Students.Update(student);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        private async Task InsertDTOToStudentAsync(StudentCompleteProfileDTO dto, Student student)
        {
            if (dto.Hobbies.Count != 0)
            {
                student.Hobbies.Clear();
                foreach (string hobbie in dto.Hobbies)
                {
                    student.Hobbies.Add(hobbie);
                }
            }

            if (dto.Personalitys.Count != 0)
            {
                student.Personalitys.Clear();
                foreach (string personalitys in dto.Personalitys)
                {
                    student.Personalitys.Add(personalitys);
                }
            }

            if (dto.CollegeId != 0)
            {
                College college = await _dbContext.Colleges.FirstOrDefaultAsync(c => c.Id == dto.CollegeId)
                    ?? throw new ArgumentException("Resource not found");

                student.College = college;
            }
        }
    }
}
