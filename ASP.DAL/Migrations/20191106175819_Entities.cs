using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP.DAL.Migrations
{
    public partial class Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BootsGroups",
                columns: table => new
                {
                    BootsGroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BootsGroups", x => x.BootsGroupId);
                });

            migrationBuilder.CreateTable(
                name: "Bootses",
                columns: table => new
                {
                    BootsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BootsName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Size = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    BootsGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bootses", x => x.BootsId);
                    table.ForeignKey(
                        name: "FK_Bootses_BootsGroups_BootsGroupId",
                        column: x => x.BootsGroupId,
                        principalTable: "BootsGroups",
                        principalColumn: "BootsGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bootses_BootsGroupId",
                table: "Bootses",
                column: "BootsGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bootses");

            migrationBuilder.DropTable(
                name: "BootsGroups");
        }
    }
}
