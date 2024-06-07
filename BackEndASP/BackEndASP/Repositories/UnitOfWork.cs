using BackEndASP.Interfaces;
using BackEndASP.Services;
using Microsoft.EntityFrameworkCore;

public class UnitOfWork : IUnitOfWorkRepository
{
    private IPropertyRepository _propertyRepository;
    private IStudentRepository _studentRepository;
    private IImageRepository _imageRepository;
    private ICollegeRepository _collegeRepository;
    private IBuildingRepository _buildingRepository;
    private IUserRepository _userRepository;
    private IOwnerRepository _ownerRepository;
    private INotificationRepository _notificationRepository;

    private SystemDbContext _dbContext;

    public UnitOfWork(SystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }




    public ICollegeRepository CollegeRepository { get { return _collegeRepository = _collegeRepository ?? new CollegeService(_dbContext); } }

    public IImageRepository ImageRepository { get { return _imageRepository = _imageRepository ?? new ImageService(_dbContext); } }

    public INotificationRepository NotificationRepository { get { return _notificationRepository = _notificationRepository ?? new NotificationService(_dbContext); } }

    public IOwnerRepository OwnerRepository { get { return _ownerRepository = _ownerRepository ?? new OwnerService(_dbContext); } }

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

