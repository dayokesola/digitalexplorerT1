using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BizNest.Service.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    BusinessTypeId = table.Column<int>(nullable: false),
                    AddressStreet = table.Column<string>(nullable: true),
                    AddressCity = table.Column<string>(nullable: true),
                    AddressPostCode = table.Column<string>(nullable: true),
                    AddressCountryId = table.Column<int>(nullable: false),
                    Contact1Name = table.Column<string>(nullable: true),
                    Contact1Email = table.Column<string>(nullable: true),
                    Contact1Mobile = table.Column<string>(nullable: true),
                    Contact2Name = table.Column<string>(nullable: true),
                    Contact2Email = table.Column<string>(nullable: true),
                    Contact2Mobile = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    MinStakeHolder = table.Column<int>(nullable: false),
                    MaxStakeHolder = table.Column<int>(nullable: false),
                    MinCapital = table.Column<decimal>(nullable: false),
                    Info = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StakeHolders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RecordStatus = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    BusinessId = table.Column<long>(nullable: false),
                    SSN = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    AddressStreet = table.Column<string>(nullable: true),
                    AddressCity = table.Column<string>(nullable: true),
                    AddressPostCode = table.Column<string>(nullable: true),
                    AddressCountryId = table.Column<int>(nullable: false),
                    SeedCapital = table.Column<decimal>(nullable: false),
                    BankName = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StakeHolders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Businesses");

            migrationBuilder.DropTable(
                name: "BusinessTypes");

            migrationBuilder.DropTable(
                name: "StakeHolders");
        }
    }
}
