using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FreedomCode.Api.Migrations
{
    public partial class initialmigrationv4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppVariableType",
                columns: table => new
                {
                    AppVariableTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    VariableTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppVariableType", x => x.AppVariableTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserAppVariableType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    AppVariableTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAppVariableType", x => new { x.Id, x.UserId, x.AppVariableTypeId });
                    table.UniqueConstraint("AK_UserAppVariableType_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAppVariableType_AppVariableType_AppVariableTypeId",
                        column: x => x.AppVariableTypeId,
                        principalTable: "AppVariableType",
                        principalColumn: "AppVariableTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAppVariableType_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppVariableType",
                columns: new[] { "AppVariableTypeId", "Name", "VariableTypeId" },
                values: new object[,]
                {
                    { 1, "UserRole", null },
                    { 1001, "Admin", 1 },
                    { 1002, "Staff", 1 },
                    { 1003, "Tanent", 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "FirstName", "LastName", "Password", "Token", "Username" },
                values: new object[,]
                {
                    { 1, "Admin", "User", "admin", null, "admin@mail.com" },
                    { 2, "Normal", "User", "user", null, "user@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "UserAppVariableType",
                columns: new[] { "Id", "UserId", "AppVariableTypeId" },
                values: new object[] { 100, 1, 1001 });

            migrationBuilder.InsertData(
                table: "UserAppVariableType",
                columns: new[] { "Id", "UserId", "AppVariableTypeId" },
                values: new object[] { 1001, 2, 1002 });

            migrationBuilder.CreateIndex(
                name: "IX_UserAppVariableType_AppVariableTypeId",
                table: "UserAppVariableType",
                column: "AppVariableTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAppVariableType_UserId",
                table: "UserAppVariableType",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAppVariableType");

            migrationBuilder.DropTable(
                name: "AppVariableType");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
