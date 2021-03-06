﻿// <auto-generated />
using System;
using BizNest.Core.Data.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BizNest.Service.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20190510000616_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BizNest.Core.Domain.Entity.App.Business", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressCity");

                    b.Property<int>("AddressCountryId");

                    b.Property<string>("AddressPostCode");

                    b.Property<string>("AddressStreet");

                    b.Property<int>("BusinessTypeId");

                    b.Property<string>("Code");

                    b.Property<string>("Contact1Email");

                    b.Property<string>("Contact1Mobile");

                    b.Property<string>("Contact1Name");

                    b.Property<string>("Contact2Email");

                    b.Property<string>("Contact2Mobile");

                    b.Property<string>("Contact2Name");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Name");

                    b.Property<int>("RecordStatus");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Businesses");
                });

            modelBuilder.Entity("BizNest.Core.Domain.Entity.App.BusinessType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Info");

                    b.Property<int>("MaxStakeHolder");

                    b.Property<decimal>("MinCapital");

                    b.Property<int>("MinStakeHolder");

                    b.Property<string>("Name");

                    b.Property<int>("RecordStatus");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("BusinessTypes");
                });

            modelBuilder.Entity("BizNest.Core.Domain.Entity.App.StakeHolder", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountNumber");

                    b.Property<string>("AddressCity");

                    b.Property<int>("AddressCountryId");

                    b.Property<string>("AddressPostCode");

                    b.Property<string>("AddressStreet");

                    b.Property<string>("BankName");

                    b.Property<DateTime>("BirthDate");

                    b.Property<long>("BusinessId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Mobile");

                    b.Property<int>("RecordStatus");

                    b.Property<string>("SSN");

                    b.Property<decimal>("SeedCapital");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("StakeHolders");
                });
#pragma warning restore 612, 618
        }
    }
}
