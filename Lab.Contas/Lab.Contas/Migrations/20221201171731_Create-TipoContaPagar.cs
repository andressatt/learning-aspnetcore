using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab.Contas.Migrations
{
    public partial class CreateTipoContaPagar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "ContaPagar",
                newName: "TipoId");

            migrationBuilder.CreateTable(
                name: "TipoContaPagar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoContaPagar", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContaPagar_TipoId",
                table: "ContaPagar",
                column: "TipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContaPagar_TipoContaPagar_TipoId",
                table: "ContaPagar",
                column: "TipoId",
                principalTable: "TipoContaPagar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContaPagar_TipoContaPagar_TipoId",
                table: "ContaPagar");

            migrationBuilder.DropTable(
                name: "TipoContaPagar");

            migrationBuilder.DropIndex(
                name: "IX_ContaPagar_TipoId",
                table: "ContaPagar");

            migrationBuilder.RenameColumn(
                name: "TipoId",
                table: "ContaPagar",
                newName: "Tipo");
        }
    }
}
