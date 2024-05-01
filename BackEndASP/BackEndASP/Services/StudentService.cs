using ApiCatalogo.Pagination;
using BackEndASP.DTOs.StudentDTOs;
using BackEndASP.Entities;
using BackEndASP.Interfaces;
using BackEndASP.Utils;
using Geocoding;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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


            IQueryable<Student> query = _dbContext.Students.Include(s => s.College)
                .Include(s => s.Connections)
                .Include(s => s.Notifications);


            // controle do conectar
            var userInPendents = _dbContext.Students.ToList();

            List<string> personsIConnectedId = new List<string>();
            List<string> userInPendentsId = new List<string>();

            foreach (var user in userInPendents)
            {
                if (user.PendentsConnectionsId.Any())
                {
                    foreach (var id in user.PendentsConnectionsId)
                    {
                        userInPendentsId.Add(id);
                    }
                }

                if (user.IdsPersonsIConnect.Any())
                {
                    foreach (var id in user.IdsPersonsIConnect)
                    {
                        personsIConnectedId.Add(id);
                    }
                }
            }

            // controle do conectar
            if (userId != null)
            {
                query = query.Where(s =>
                !s.PendentsConnectionsId.Any(p => userInPendentsId.Contains(p)) &&
                !(s.Notifications.Any(n => userInPendentsId.Contains(n.UserIdWhoSendNotification) && !n.Read)) &&
                !s.IdsPersonsIConnect.Any(c => personsIConnectedId.Contains(c)));
            }

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
                s.Personalities.Any(personality => personality.Contains(interest)))
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
        public async Task<IEnumerable<StudentResponseNotification>> FindMyAllStudentsWhoInvitationsConnections(string userId)
        {
            Student user = _dbContext.Students.AsNoTracking().FirstOrDefault(s => s.Id == userId)
                           ?? throw new ArgumentException($"This id {userId} does not exist");

            var userNotificationIds = _dbContext.Notifications
                .Include(n => n.User)
                .Where(n => n.User.Id == userId)
                .Select(n => n.Id)
                .ToList();

            var studentsWithPendentConnections = new List<Student>();
            if (user.PendentsConnectionsId.Any())
            {
                // Filtra os estudantes com base nos IDs pendentes de conexão do usuário
                studentsWithPendentConnections = _dbContext.Students
                    .Where(s => user.PendentsConnectionsId.Contains(s.Id))
                    .Include(s => s.Image)
                    .Include(s => s.College)
                    .ToList();
            }

            return studentsWithPendentConnections.Select(s => new StudentResponseNotification(s, userNotificationIds)).ToList();
        }



        // adicionando o ID de um estudante como pedido de conexão
        public async Task GiveConnectionOrder(string userId, string studentForConnectionId)
        {
            Student studentForConnection = await _dbContext.Students.Include(u => u.Notifications).FirstOrDefaultAsync(u => u.Id == studentForConnectionId)
                ?? throw new ArgumentException($"User with id {studentForConnectionId} does not exist");

            var user = await _dbContext.Students.Include(u => u.Notifications).FirstOrDefaultAsync(u => u.Id == userId)
                ?? throw new ArgumentException($"User with id {userId} does not exist");

            // Verificar se o usuário que está enviando o pedido já está salvo nas notificações do usuário receptor
            if (studentForConnection.Notifications.Any(n => n.UserIdWhoSendNotification == userId && !n.Read))
            {
                throw new ArgumentException($"You already have send connection for this user, await");
            }


            Notification notification = new Notification
            {
                UserIdWhoSendNotification = user.Id,
                User = user,
                Moment = DateTimeOffset.Now,
                Read = false,
                Text = $"O usuário {user.UserName.ToUpper()} enviou a você um pedido de conexão!"
            };

            // Add the UserNotification to the appropriate collections
            studentForConnection.Notifications.Add(notification);
            studentForConnection.PendentsConnectionsId.Add(userId);
            _dbContext.Update(studentForConnection);
        }


        // estudante aceitando ou recusando o pedido de conexão
        public async Task<bool> HandleConnection(string userId, StudentConnectionInsertDTO dto, int notificationId)
        {
            // Encontrar o estudante atual
            Student actualStudent = await _dbContext.Students
                .Include(s => s.Notifications)
                .FirstOrDefaultAsync(s => s.Id == userId)
                ?? throw new ArgumentException($"This id {userId} does not exist");

            // Encontrar a notificação
            Notification notification = await _dbContext.Notifications
                .FirstOrDefaultAsync(s => s.Id == notificationId)
                ?? throw new ArgumentException($"This id {notificationId} does not exist");

            // Marcar a notificação como lida
            notification.Read = true;

         
            Student otherStudent = await _dbContext.Students
                                       .FirstOrDefaultAsync(s => s.Id == dto.ConnectionWhyIHandle)
                                   ?? throw new ArgumentException($"This id {userId} does not exist");

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


                    otherStudent.IdsPersonsIConnect.Add(userId);

                    _dbContext.Students.Update(otherStudent);

                    // Criar a notificação
                    // Notification sendNotificationForUser = new Notification
                    //{
                    //    UserIdWhoSendNotification = actualStudent.Id,
                     //   User = otherStudent,
                     //   Moment = DateTimeOffset.Now,
                    //    Read = false,
                     //   Text = $"O usuário {actualStudent.UserName.ToUpper()} aceitou o seu pedido de conexão!"
                    //};

                    // Adicionar a notificação ao contexto do banco de dados
                    //_dbContext.Notifications.Add(sendNotificationForUser);

                    // Salvar as alterações para obter o ID da notificação atribuído automaticamente

                    // Adicionar a nova conexão ao contexto do banco de dados
                    await _dbContext.UserConnections.AddAsync(userConnection);
                    await _dbContext.SaveChangesAsync();
                }

                

                // Atualizar o estudante atual no contexto do banco de dados
                actualStudent.IdsPersonsIConnect.Add(dto.ConnectionWhyIHandle);
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

        public async Task<bool> CompleteProfileStudentHobbies(string userId, StudentCompleteProfileHobbies dto)
        {
            var student = await _dbContext.Students.FindAsync(userId)
                ?? throw new ArgumentException($"User with id {userId} does not exist");

            if (dto.Hobbies.Count != 0)
            {
                foreach(string hobbie in dto.Hobbies)
                {
                    student.Hobbies.Add(hobbie);
                }
            }

            _dbContext.Students.Update(student);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CompleteProfileStudentPersonalities(string userId, StudentCompleteProfilePersonalities dto)
        {
            var student = await _dbContext.Students.FindAsync(userId)
                ?? throw new ArgumentException($"User with id {userId} does not exist");

            if (dto.Personalities.Count != 0)
            {
                foreach (string personality in dto.Personalities)
                {
                    student.Personalities.Add(personality);
                }
            }

            _dbContext.Students.Update(student);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        private async Task InsertDTOToStudentAsync(StudentCompleteProfileDTO dto, Student student)
        {
            if (dto.Username != null)
            {
                student.UserName = dto.Username;
            }

            if (dto.Gender != null)
            {
                student.Gender = dto.Gender;
            }

            if (dto.BirthDate != null)
            {
                student.BirthDate = (DateTimeOffset) dto.BirthDate;
            }

            if (dto.PhoneNumber != null)
            {
                student.PhoneNumber = dto.PhoneNumber;
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
