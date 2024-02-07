using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class addresstbl1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "tblUser",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tblAddress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParmanentAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAddress", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblUser_AddressId",
                table: "tblUser",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblUser_tblAddress_AddressId",
                table: "tblUser",
                column: "AddressId",
                principalTable: "tblAddress",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblUser_tblAddress_AddressId",
                table: "tblUser");

            migrationBuilder.DropTable(
                name: "tblAddress");

            migrationBuilder.DropIndex(
                name: "IX_tblUser_AddressId",
                table: "tblUser");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "tblUser");
        }
    }
}
