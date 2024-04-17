﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BackEndASP.Migrations
{
    [DbContext(typeof(SystemDbContext))]
    partial class SystemDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BackEndASP.Entities.PropertyStudentLikes", b =>
                {
                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("PropertyId")
                        .HasColumnType("int");

                    b.HasKey("StudentId", "PropertyId");

                    b.HasIndex("PropertyId");

                    b.ToTable("StudentPropertiesLikes");

                    b.HasData(
                        new
                        {
                            StudentId = "696e3379-2d73-47ad-b443-47f05e44376f",
                            PropertyId = 2
                        },
                        new
                        {
                            StudentId = "06c6340d-2689-490c-9e03-cd90a176810d",
                            PropertyId = 2
                        });
                });

            modelBuilder.Entity("BackEndASP.Entities.UserConnection", b =>
                {
                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("OtherStudentId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("StudentId", "OtherStudentId");

                    b.HasIndex("OtherStudentId");

                    b.ToTable("UserConnections");
                });

            modelBuilder.Entity("BackEndASP.Entities.UserNotifications", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("NotificationId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "NotificationId");

                    b.HasIndex("NotificationId");

                    b.ToTable("UserNotifications");
                });

            modelBuilder.Entity("Building", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HomeComplement")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Long")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Neighborhood")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Buildings");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Building");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BuildingId")
                        .HasColumnType("int");

                    b.Property<string>("ImageData64")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InsertedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "3",
                            Name = "Owner",
                            NormalizedName = "OWNER"
                        },
                        new
                        {
                            Id = "2",
                            Name = "Student",
                            NormalizedName = "STUDENT"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "4bdfd212-0bb9-4b8b-b2d2-bc5f0b17e2d2",
                            RoleId = "1"
                        },
                        new
                        {
                            UserId = "dba3f6ea-405e-4d9a-9a98-6bfe66d3a0b9",
                            RoleId = "3"
                        },
                        new
                        {
                            UserId = "06c6340d-2689-490c-9e03-cd90a176810d",
                            RoleId = "2"
                        },
                        new
                        {
                            UserId = "696e3379-2d73-47ad-b443-47f05e44376f",
                            RoleId = "2"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("Moment")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("Read")
                        .HasColumnType("bit");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("BirthDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int?>("ImageId")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("ImageId")
                        .IsUnique()
                        .HasFilter("[ImageId] IS NOT NULL");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();

                    b.HasData(
                        new
                        {
                            Id = "4bdfd212-0bb9-4b8b-b2d2-bc5f0b17e2d2",
                            AccessFailedCount = 0,
                            BirthDate = new DateTimeOffset(new DateTime(2024, 4, 12, 21, 46, 23, 708, DateTimeKind.Unspecified).AddTicks(411), new TimeSpan(0, -3, 0, 0, 0)),
                            ConcurrencyStamp = "dacb9773-32b1-4335-9244-c4a59dd2c34d",
                            CreatedDate = new DateTimeOffset(new DateTime(2024, 4, 13, 0, 46, 23, 646, DateTimeKind.Unspecified).AddTicks(8233), new TimeSpan(0, 0, 0, 0, 0)),
                            Email = "admin@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@GMAIL.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAIAAYagAAAAEK9T5dFlfCuMMtCMeLt2z2CvfVBZ9e/5yZFVsr4PajEpW7MAVmp7yPfxBz7YPr9Wkg==",
                            PhoneNumber = "999999999",
                            PhoneNumberConfirmed = true,
                            RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SecurityStamp = "dbdfb224-e56a-46da-8f12-7eb2e7261586",
                            TwoFactorEnabled = false,
                            UserName = "Admin"
                        });
                });

            modelBuilder.Entity("College", b =>
                {
                    b.HasBaseType("Building");

                    b.HasDiscriminator().HasValue("College");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Rodovia Senador José Ermírio de Moraes",
                            District = "Sorocaba",
                            HomeComplement = "",
                            Lat = "-23.469645838524144",
                            Long = "-47.42976187034831",
                            Name = "FACENS",
                            Neighborhood = "Iporanga",
                            Number = "",
                            State = "SP"
                        });
                });

            modelBuilder.Entity("Property", b =>
                {
                    b.HasBaseType("Building");

                    b.Property<string>("Bathrooms")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bedrooms")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasIndex("OwnerId");

                    b.HasDiscriminator().HasValue("Property");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Address = "Rua Achiles Audi",
                            District = "Cerquilho",
                            HomeComplement = "Casa",
                            Lat = "-23.1723808873683",
                            Long = "-47.74702041600901",
                            Name = "Casa de aluguel",
                            Neighborhood = "Centro",
                            Number = "1054",
                            State = "SP",
                            Bathrooms = "2",
                            Bedrooms = "3",
                            Description = "Excelente casa, localizada em um excelente lugar, 2 banheiros sendo 1 suite, tres quartos, sala, cozinha e garagem que cabe 3 carros tranquilamente",
                            OwnerId = "dba3f6ea-405e-4d9a-9a98-6bfe66d3a0b9",
                            Price = 1200.0
                        });
                });

            modelBuilder.Entity("BackEndASP.Entities.Owner", b =>
                {
                    b.HasBaseType("User");

                    b.HasDiscriminator().HasValue("Owner");

                    b.HasData(
                        new
                        {
                            Id = "dba3f6ea-405e-4d9a-9a98-6bfe66d3a0b9",
                            AccessFailedCount = 0,
                            BirthDate = new DateTimeOffset(new DateTime(2024, 4, 12, 21, 46, 23, 768, DateTimeKind.Unspecified).AddTicks(9878), new TimeSpan(0, -3, 0, 0, 0)),
                            ConcurrencyStamp = "69c3a4fa-6d6f-46ae-be6a-17953dac719e",
                            CreatedDate = new DateTimeOffset(new DateTime(2024, 4, 13, 0, 46, 23, 708, DateTimeKind.Unspecified).AddTicks(829), new TimeSpan(0, 0, 0, 0, 0)),
                            Email = "owner@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "OWNER@GMAIL.COM",
                            NormalizedUserName = "OWNER",
                            PasswordHash = "AQAAAAIAAYagAAAAEDfjpHRXFUcVxvMC2H4uIY5raAcvV2p+d61Ab5GaUH8/JM40GQuBJdgy7svXmgdFLg==",
                            PhoneNumber = "999999999",
                            PhoneNumberConfirmed = true,
                            RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SecurityStamp = "4de9b541-57ce-4b56-b52c-199fce2f16e7",
                            TwoFactorEnabled = false,
                            UserName = "Owner"
                        });
                });

            modelBuilder.Entity("Student", b =>
                {
                    b.HasBaseType("User");

                    b.Property<int?>("CollegeId")
                        .HasColumnType("int");

                    b.Property<string>("Hobbies")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PendentsConnectionsId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Personalitys")
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("CollegeId");

                    b.HasDiscriminator().HasValue("Student");

                    b.HasData(
                        new
                        {
                            Id = "06c6340d-2689-490c-9e03-cd90a176810d",
                            AccessFailedCount = 0,
                            BirthDate = new DateTimeOffset(new DateTime(2024, 4, 12, 21, 46, 23, 829, DateTimeKind.Unspecified).AddTicks(8182), new TimeSpan(0, -3, 0, 0, 0)),
                            ConcurrencyStamp = "822c9cf9-da64-4f3a-8fc5-67f8cebfff32",
                            CreatedDate = new DateTimeOffset(new DateTime(2024, 4, 13, 0, 46, 23, 769, DateTimeKind.Unspecified).AddTicks(298), new TimeSpan(0, 0, 0, 0, 0)),
                            Email = "student@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "STUDENT@GMAIL.COM",
                            NormalizedUserName = "STUDENT",
                            PasswordHash = "AQAAAAIAAYagAAAAED5l1U9dtt5+juccFlbV1SB/5/Cj1WREzS90qtayUzGs/+u/MBS51MF0+eUDkVm1Kg==",
                            PhoneNumber = "999999999",
                            PhoneNumberConfirmed = true,
                            RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SecurityStamp = "267bdbdd-6879-4f1b-a329-f85dd3c76196",
                            TwoFactorEnabled = false,
                            UserName = "Student",
                            CollegeId = 1,
                            Hobbies = "[]",
                            PendentsConnectionsId = "[]",
                            Personalitys = "[]"
                        },
                        new
                        {
                            Id = "696e3379-2d73-47ad-b443-47f05e44376f",
                            AccessFailedCount = 0,
                            BirthDate = new DateTimeOffset(new DateTime(2024, 4, 12, 21, 46, 23, 889, DateTimeKind.Unspecified).AddTicks(9076), new TimeSpan(0, -3, 0, 0, 0)),
                            ConcurrencyStamp = "b7b235ad-838e-4425-a77a-fd87f35d4687",
                            CreatedDate = new DateTimeOffset(new DateTime(2024, 4, 13, 0, 46, 23, 829, DateTimeKind.Unspecified).AddTicks(8821), new TimeSpan(0, 0, 0, 0, 0)),
                            Email = "joao@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "JOAO@GMAIL.COM",
                            NormalizedUserName = "JOAO",
                            PasswordHash = "AQAAAAIAAYagAAAAEMCB+M2Xn3nrvgji/3dqOouB8MqZZYxq4+VPXinHcS8PEWDRE4WMoUm0/W9nG7lJnQ==",
                            PhoneNumber = "999999999",
                            PhoneNumberConfirmed = true,
                            RefreshTokenExpiryTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SecurityStamp = "8147c2bc-a6ea-47f2-abec-05bc78cc4813",
                            TwoFactorEnabled = false,
                            UserName = "Joao",
                            CollegeId = 1,
                            Hobbies = "[\"League of Legends\",\"Pop\",\"Carros\"]",
                            PendentsConnectionsId = "[]",
                            Personalitys = "[\"Timido\",\"Quieto\",\"Amigavel\"]"
                        });
                });

            modelBuilder.Entity("BackEndASP.Entities.PropertyStudentLikes", b =>
                {
                    b.HasOne("Property", "Property")
                        .WithMany("StudentLikes")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Student", "Student")
                        .WithMany("PropertiesLikes")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Property");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("BackEndASP.Entities.UserConnection", b =>
                {
                    b.HasOne("Student", "OtherStudent")
                        .WithMany()
                        .HasForeignKey("OtherStudentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Student", "Student")
                        .WithMany("Connections")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("OtherStudent");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("BackEndASP.Entities.UserNotifications", b =>
                {
                    b.HasOne("Notification", "Notification")
                        .WithMany("UserNotifications")
                        .HasForeignKey("NotificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("User", "User")
                        .WithMany("UserNotifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Notification");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Image", b =>
                {
                    b.HasOne("Building", "Building")
                        .WithMany("Images")
                        .HasForeignKey("BuildingId");

                    b.Navigation("Building");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("User", b =>
                {
                    b.HasOne("Image", "Image")
                        .WithOne("User")
                        .HasForeignKey("User", "ImageId");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("Property", b =>
                {
                    b.HasOne("BackEndASP.Entities.Owner", "Owner")
                        .WithMany("Properties")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Student", b =>
                {
                    b.HasOne("College", "College")
                        .WithMany("Students")
                        .HasForeignKey("CollegeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("College");
                });

            modelBuilder.Entity("Building", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("Image", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("Notification", b =>
                {
                    b.Navigation("UserNotifications");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Navigation("UserNotifications");
                });

            modelBuilder.Entity("College", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Property", b =>
                {
                    b.Navigation("StudentLikes");
                });

            modelBuilder.Entity("BackEndASP.Entities.Owner", b =>
                {
                    b.Navigation("Properties");
                });

            modelBuilder.Entity("Student", b =>
                {
                    b.Navigation("Connections");

                    b.Navigation("PropertiesLikes");
                });
#pragma warning restore 612, 618
        }
    }
}
