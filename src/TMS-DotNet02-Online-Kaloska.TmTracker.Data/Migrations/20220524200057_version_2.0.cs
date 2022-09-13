using Microsoft.EntityFrameworkCore.Migrations;

namespace TMS_DotNet02_Online_Kaloska.TmTracker.Data.Migrations
{
    public partial class version_20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Projects_ProjectId",
                schema: "tracker",
                table: "Goals");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Projects_ProjectId",
                schema: "tracker",
                table: "Records");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Projects_ProjectId",
                schema: "tracker",
                table: "Goals",
                column: "ProjectId",
                principalSchema: "tracker",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Projects_ProjectId",
                schema: "tracker",
                table: "Records",
                column: "ProjectId",
                principalSchema: "tracker",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Projects_ProjectId",
                schema: "tracker",
                table: "Goals");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Projects_ProjectId",
                schema: "tracker",
                table: "Records");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Projects_ProjectId",
                schema: "tracker",
                table: "Goals",
                column: "ProjectId",
                principalSchema: "tracker",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Projects_ProjectId",
                schema: "tracker",
                table: "Records",
                column: "ProjectId",
                principalSchema: "tracker",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
