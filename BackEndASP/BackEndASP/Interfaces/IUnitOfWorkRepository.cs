﻿namespace BackEndASP.Interfaces
{
    public interface IUnitOfWorkRepository
    {

        ICollegeRepository CollegeRepository { get; }
        IImageRepository ImageRepository { get; }
        IUserRepository UserRepository { get; }
        INotificationRepository NotificationRepository { get; }
        IOwnerRepository OwnerRepository { get; }
        IPropertyRepository PropertyRepository { get; }
        IStudentRepository StudentRepository { get; }
        IBuildingRepository BuildingRepository { get; }


        Task CommitAsync();

    }
}
