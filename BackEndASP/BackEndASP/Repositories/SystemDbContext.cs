
using BackEndASP.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;



    public class SystemDbContext : IdentityDbContext<User>
    {
        
        public SystemDbContext(DbContextOptions<SystemDbContext> option) : base(option) 
        {

        // DOCKER APLICAR MIGRATIONS
        try
        {
            var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            if (databaseCreator != null)
            {
                if (!databaseCreator.CanConnect()) databaseCreator.Create();
                if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        ///////

    }

        public DbSet<Building> Buildings { get; set; }
        public DbSet<College> Colleges { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PropertyStudentLikes> StudentPropertiesLikes { get; set; }
        public DbSet<UserConnection> UserConnections { get; set; }


    [Obsolete]
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = "3", Name = "Owner", NormalizedName = "OWNER" },
            new IdentityRole { Id = "2", Name = "Student", NormalizedName = "STUDENT" }

        );

        // Users ids
        var adminId = Guid.NewGuid().ToString();
        var ownerId = Guid.NewGuid().ToString();
        var studentId = Guid.NewGuid().ToString();
        var joaoId = Guid.NewGuid().ToString();


        // Seed college
        modelBuilder.Entity<College>().HasData(
            new College
            {
                Id = 1,
                Address = "Rodovia Senador José Ermírio de Moraes",
                District = "Sorocaba",
                Name = "FACENS",
                State = "SP",
                HomeComplement = "",
                Neighborhood = "Iporanga",
                Number = "",
                Lat = "-23.469645838524144",
                Long = "-47.42976187034831"
            }
        );

        //Seed Properties
        modelBuilder.Entity<Property>().HasData(
            new Property
            {
                Id = 2,
                Address = "Rua Achiles Audi",
                District = "Cerquilho",
                Name = "Casa de aluguel",
                State = "SP",
                HomeComplement = "Casa",
                Neighborhood = "Centro",
                Number = "1054",
                Lat = "-23.1723808873683",
                Long = "-47.74702041600901",
                Price = 1200.00,
                Bathrooms = "2",
                Bedrooms = "3",
                Description = "Excelente casa, localizada em um excelente lugar, 2 banheiros sendo 1 suite, tres quartos, sala, cozinha e garagem que " +
                "cabe 3 carros tranquilamente",
                OwnerId = ownerId
            }
        );

        //Seed likes beetween students and properties
        modelBuilder.Entity<PropertyStudentLikes>().HasData(
            new PropertyStudentLikes { PropertyId = 2, StudentId = joaoId },
            new PropertyStudentLikes { PropertyId = 2, StudentId = studentId }
        );



        // Seed users
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = adminId,
                Email = "admin@gmail.com",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "Senha#123"),
                EmailConfirmed = true,
                PhoneNumber = "999999999",
                PhoneNumberConfirmed = true,
                NormalizedEmail = "ADMIN@GMAIL.COM",
                BirthDate = DateTimeOffset.Now
            }
        );

        modelBuilder.Entity<Owner>().HasData(
            new Owner
            {
                Id = ownerId,
                Email = "owner@gmail.com",
                UserName = "Owner",
                NormalizedUserName = "OWNER",
                PasswordHash = new PasswordHasher<Owner>().HashPassword(null, "Senha#123"), 
                EmailConfirmed = true,
                PhoneNumber = "999999999",
                PhoneNumberConfirmed = true,
                NormalizedEmail = "OWNER@GMAIL.COM",
                BirthDate = DateTimeOffset.Now
            }
        );

        modelBuilder.Entity<Student>().HasData(
            new Student
            {
                Id = studentId,
                Email = "student@gmail.com",
                UserName = "Student",
                NormalizedUserName = "STUDENT",
                PasswordHash = new PasswordHasher<Student>().HashPassword(null, "Senha#123"), 
                EmailConfirmed = true,
                PhoneNumber = "999999999",
                PhoneNumberConfirmed = true,
                NormalizedEmail = "STUDENT@GMAIL.COM",
                BirthDate = DateTimeOffset.Now,
                CollegeId = 1
            }
        );


        modelBuilder.Entity<Student>().HasData(
            new Student
            {
                Id = joaoId,
                Email = "joao@gmail.com",
                UserName = "Joao",
                NormalizedUserName = "JOAO",
                PasswordHash = new PasswordHasher<Student>().HashPassword(null, "Senha#123"),
                EmailConfirmed = true,
                PhoneNumber = "999999999",
                PhoneNumberConfirmed = true,
                NormalizedEmail = "JOAO@GMAIL.COM",
                BirthDate = DateTimeOffset.Now,
                Personalities = new List<string> 
                {
                    "Timido",
                    "Quieto",
                    "Amigavel"
                },
                Hobbies = new List<string>
                {
                    "League of Legends",
                    "Pop",
                    "Carros"
                },
                CollegeId = 1

                
            }
        );

        // Seed user roles
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { UserId = adminId, RoleId = "1" }, // Admin role
            new IdentityUserRole<string> { UserId = ownerId, RoleId = "3" }, // Owner role
            new IdentityUserRole<string> { UserId = studentId, RoleId = "2" }, // Student role
            new IdentityUserRole<string> { UserId = joaoId, RoleId = "2" } // Student role
        );


        modelBuilder.Entity<Image>()
            .HasOne(i => i.User)
            .WithOne(u => u.Image)
            .HasForeignKey<User>(u => u.ImageId) 
            .IsRequired(false);


        modelBuilder.Entity<Student>()
            .HasOne(s => s.College)
            .WithMany(c => c.Students)
            .HasForeignKey(s => s.CollegeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PropertyStudentLikes>()
            .HasKey(sp => new { sp.StudentId, sp.PropertyId });

        modelBuilder.Entity<PropertyStudentLikes>()
            .HasOne(sp => sp.Student)
            .WithMany(s => s.PropertiesLikes)
            .HasForeignKey(sp => sp.StudentId)
            .OnDelete(DeleteBehavior.NoAction);


        modelBuilder.Entity<PropertyStudentLikes>()
            .HasOne(sp => sp.Property)
            .WithMany(p => p.StudentLikes)
            .HasForeignKey(sp => sp.PropertyId)
            .OnDelete(DeleteBehavior.NoAction);


        modelBuilder.Entity<UserConnection>()
            .HasKey(uc => new { uc.StudentId, uc.OtherStudentId });

        modelBuilder.Entity<Student>()
            .HasMany(s => s.Connections)
            .WithOne(c => c.Student)
            .HasForeignKey(c => c.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        //

        modelBuilder.Entity<User>()
            .HasMany(u => u.Notifications)
            .WithOne(n => n.User);
    }

}

