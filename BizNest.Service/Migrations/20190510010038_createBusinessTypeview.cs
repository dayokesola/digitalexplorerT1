using Microsoft.EntityFrameworkCore.Migrations;

namespace BizNest.Service.Migrations
{
    public partial class createBusinessTypeview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var txt = @"create view dbo.businesstypesmodel as select x.* from businesstypes x 
where x.RecordStatus != 3 and x.RecordStatus != 4"; 

            migrationBuilder.Sql(txt);



        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            var txt = @"drop view businesstypesmodel";
            migrationBuilder.Sql(txt);
        }
    }
}
