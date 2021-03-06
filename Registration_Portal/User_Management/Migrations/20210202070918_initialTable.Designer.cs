﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using User_Management.Context;

namespace User_Management.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20210202070918_initialTable")]
    partial class initialTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("User_Management.Models.Department", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TB_M_Department");
                });

            modelBuilder.Entity("User_Management.Models.Education", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Degree")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DepartmentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GPA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GraduateYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UniversityId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("UniversityId");

                    b.ToTable("TB_T_Education");
                });

            modelBuilder.Entity("User_Management.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EducationId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReligionID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EducationId");

                    b.HasIndex("ReligionID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("TB_M_Employee");
                });

            modelBuilder.Entity("User_Management.Models.Religion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TB_M_Religion");
                });

            modelBuilder.Entity("User_Management.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TB_M_Role");
                });

            modelBuilder.Entity("User_Management.Models.University", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TB_M_University");
                });

            modelBuilder.Entity("User_Management.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsUpdatePassword")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("Phone")
                        .IsUnique()
                        .HasFilter("[Phone] IS NOT NULL");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasFilter("[Username] IS NOT NULL");

                    b.ToTable("TB_M_User");
                });

            modelBuilder.Entity("User_Management.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("TB_T_UserRole");
                });

            modelBuilder.Entity("User_Management.Models.Education", b =>
                {
                    b.HasOne("User_Management.Models.Department", "Department")
                        .WithMany("UniversityDepartment")
                        .HasForeignKey("DepartmentId");

                    b.HasOne("User_Management.Models.University", "University")
                        .WithMany("UnivDepts")
                        .HasForeignKey("UniversityId");

                    b.Navigation("Department");

                    b.Navigation("University");
                });

            modelBuilder.Entity("User_Management.Models.Employee", b =>
                {
                    b.HasOne("User_Management.Models.Education", "Education")
                        .WithMany()
                        .HasForeignKey("EducationId");

                    b.HasOne("User_Management.Models.Religion", "Religion")
                        .WithMany("Employees")
                        .HasForeignKey("ReligionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("User_Management.Models.User", "User")
                        .WithOne("Employee")
                        .HasForeignKey("User_Management.Models.Employee", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Education");

                    b.Navigation("Religion");

                    b.Navigation("User");
                });

            modelBuilder.Entity("User_Management.Models.UserRole", b =>
                {
                    b.HasOne("User_Management.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("User_Management.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("User_Management.Models.Department", b =>
                {
                    b.Navigation("UniversityDepartment");
                });

            modelBuilder.Entity("User_Management.Models.Religion", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("User_Management.Models.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("User_Management.Models.University", b =>
                {
                    b.Navigation("UnivDepts");
                });

            modelBuilder.Entity("User_Management.Models.User", b =>
                {
                    b.Navigation("Employee");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
