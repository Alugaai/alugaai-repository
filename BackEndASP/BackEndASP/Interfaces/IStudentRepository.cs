﻿using ApiCatalogo.Pagination;
using BackEndASP.DTOs;
using BackEndASP.DTOs.StudentDTOs;
using BackEndASP.Utils;

namespace BackEndASP.Interfaces
{
    public interface IStudentRepository
    {

        Task<PagedList<StudentFindAllFilterDTO>> FindAllStudentsAsync(PageStudentQueryParams pageQueryParams, string userId);

        StudentsConnectionsDTO FindAllMyStudentsConnections(string userId);
        StudentsConnectionsDTO FindMyAllStudentsWhoInvitationsConnections(string userId);
        Task GiveConnectionOrder(string userId, string studentForConnectionId);
        Task<bool> HandleConnection(string userId, StudentConnectionInsertDTO dto, int notificationId);

    }
}
