using Microsoft.EntityFrameworkCore.Migrations;

namespace User_Management.Migrations
{
    public partial class ChangeDegreeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Degree",
                table: "TB_T_Education");

            migrationBuilder.AddColumn<string>(
                name: "DegreeId",
                table: "TB_T_Education",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_Education_DegreeId",
                table: "TB_T_Education",
                column: "DegreeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_T_Education_TB_M_Degree_DegreeId",
                table: "TB_T_Education",
                column: "DegreeId",
                principalTable: "TB_M_Degree",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_T_Education_TB_M_Degree_DegreeId",
                table: "TB_T_Education");

            migrationBuilder.DropIndex(
                name: "IX_TB_T_Education_DegreeId",
                table: "TB_T_Education");

            migrationBuilder.DropColumn(
                name: "DegreeId",
                table: "TB_T_Education");

            migrationBuilder.AddColumn<string>(
                name: "Degree",
                table: "TB_T_Education",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
