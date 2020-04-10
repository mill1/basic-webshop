using Microsoft.EntityFrameworkCore.Migrations;

namespace Earless.WebApi.Migrations
{
    public partial class OrderOrderLineRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "OrderLines",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "OrderLines",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
