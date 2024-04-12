﻿using BackEndASP.Interfaces;
using BackEndASP.Services;

public class UnitOfWork : IUnitOfWorkRepository
    {
        private IPropertyRepository _propertyRepository;
        private IStudentRepository _studentRepository;
        private IImageRepository _imageRepository;
        private ICollegeRepository _collegeRepository;    
        private IBuildingRepository _buildingRepository;
        private IUserRepository _userRepository;

        private SystemDbContext _dbContext;
        private IWebHostEnvironment _environment;

    public UnitOfWork(SystemDbContext dbContext, IWebHostEnvironment environment)
    {
        _dbContext = dbContext;
        _environment = environment;
    }




    public ICollegeRepository CollegeRepository { get { return _collegeRepository = _collegeRepository ?? new CollegeService(_dbContext); } }

    public IImageRepository ImageRepository { get { return _imageRepository = _imageRepository ?? new ImageService(_dbContext, _environment); } }

    public INotificationRepository NotificationRepository => throw new NotImplementedException();

    public IOwnerRepository OwnerRepository => throw new NotImplementedException();

    public IPropertyRepository PropertyRepository { get { return _propertyRepository = _propertyRepository ?? new PropertyService(_dbContext); } }
    public IUserRepository UserRepository { get { return _userRepository = _userRepository ?? new UserService(_dbContext); } }

    public IStudentRepository StudentRepository { get { return _studentRepository = _studentRepository ?? new StudentService(_dbContext); } }

    public IBuildingRepository BuildingRepository { get { return _buildingRepository = _buildingRepository ?? new BuildingService(); } }

    public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task Dispose()
        {
            await _dbContext.DisposeAsync();
        }
    }

