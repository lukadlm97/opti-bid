﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OptiBid.Microservices.Accounts.Persistence;

#nullable disable

namespace OptiBid.Microservices.Accounts.Persistence.Migrations
{
    [DbContext(typeof(AccountsContext))]
    [Migration("20230117182041_remove_country_table_from_context_2023-17-01-19-21")]
    partial class removecountrytablefromcontext202317011921
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OptiBid.Microservices.Accounts.Domain.Entities.Contact", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("ContactTypeID")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ContactTypeID");

                    b.HasIndex("UserID");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("OptiBid.Microservices.Accounts.Domain.Entities.ContactType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("ContactTypes");
                });

            modelBuilder.Entity("OptiBid.Microservices.Accounts.Domain.Entities.Profession", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Professions");
                });

            modelBuilder.Entity("OptiBid.Microservices.Accounts.Domain.Entities.Skill", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("ProfessionID")
                        .HasColumnType("int");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ProfessionID");

                    b.HasIndex("UserID");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("OptiBid.Microservices.Accounts.Domain.Entities.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("CountryID")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int?>("UserRoleID")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("UserRoleID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OptiBid.Microservices.Accounts.Domain.Entities.UserRole", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("OptiBid.Microservices.Accounts.Domain.Entities.Contact", b =>
                {
                    b.HasOne("OptiBid.Microservices.Accounts.Domain.Entities.ContactType", "ContactType")
                        .WithMany("Contacts")
                        .HasForeignKey("ContactTypeID");

                    b.HasOne("OptiBid.Microservices.Accounts.Domain.Entities.User", "User")
                        .WithMany("Contacts")
                        .HasForeignKey("UserID");

                    b.Navigation("ContactType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OptiBid.Microservices.Accounts.Domain.Entities.Skill", b =>
                {
                    b.HasOne("OptiBid.Microservices.Accounts.Domain.Entities.Profession", "Profession")
                        .WithMany("Skills")
                        .HasForeignKey("ProfessionID");

                    b.HasOne("OptiBid.Microservices.Accounts.Domain.Entities.User", "User")
                        .WithMany("Skills")
                        .HasForeignKey("UserID");

                    b.Navigation("Profession");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OptiBid.Microservices.Accounts.Domain.Entities.User", b =>
                {
                    b.HasOne("OptiBid.Microservices.Accounts.Domain.Entities.UserRole", "UserRole")
                        .WithMany("Users")
                        .HasForeignKey("UserRoleID");

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("OptiBid.Microservices.Accounts.Domain.Entities.ContactType", b =>
                {
                    b.Navigation("Contacts");
                });

            modelBuilder.Entity("OptiBid.Microservices.Accounts.Domain.Entities.Profession", b =>
                {
                    b.Navigation("Skills");
                });

            modelBuilder.Entity("OptiBid.Microservices.Accounts.Domain.Entities.User", b =>
                {
                    b.Navigation("Contacts");

                    b.Navigation("Skills");
                });

            modelBuilder.Entity("OptiBid.Microservices.Accounts.Domain.Entities.UserRole", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
