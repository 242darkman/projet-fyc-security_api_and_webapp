using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobalRenov.Migrations
{
    public partial class updateIntervention : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Adresse",
                table: "Interventions",
                newName: "Address");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Interventions",
                newName: "Adresse");
        }
    }
}
