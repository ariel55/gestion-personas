using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pais",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sexo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sexo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(nullable: true),
                    Apellido = table.Column<string>(nullable: true),
                    TipoDocumento = table.Column<string>(nullable: false),
                    NroDocumento = table.Column<string>(nullable: false),
                    FechaNacimiento = table.Column<DateTime>(nullable: false),
                    PaisId = table.Column<int>(nullable: false),
                    Nacionalidad = table.Column<string>(nullable: true),
                    SexoId = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persona_Pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Persona_Sexo_SexoId",
                        column: x => x.SexoId,
                        principalTable: "Sexo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Pais",
                columns: new[] { "Id", "Nombre" },
                values: new object[] { 1, "Argentina" });

            migrationBuilder.InsertData(
                table: "Pais",
                columns: new[] { "Id", "Nombre" },
                values: new object[] { 2, "Chile" });

            migrationBuilder.InsertData(
                table: "Pais",
                columns: new[] { "Id", "Nombre" },
                values: new object[] { 3, "Uruguay" });

            migrationBuilder.InsertData(
                table: "Sexo",
                columns: new[] { "Id", "Descripcion" },
                values: new object[] { 1, "Hombre" });

            migrationBuilder.InsertData(
                table: "Sexo",
                columns: new[] { "Id", "Descripcion" },
                values: new object[] { 2, "Mujer" });

            migrationBuilder.InsertData(
                table: "Persona",
                columns: new[] { "Id", "Apellido", "Email", "FechaNacimiento", "Nacionalidad", "Nombre", "NroDocumento", "PaisId", "SexoId", "Telefono", "TipoDocumento" },
                values: new object[] { 1, "Pintos", "micorreo@gmail.com", new DateTime(2020, 7, 7, 0, 0, 0, 0, DateTimeKind.Local), null, "Marcos", "12345678", 1, 1, "1111", "dni" });

            migrationBuilder.InsertData(
                table: "Persona",
                columns: new[] { "Id", "Apellido", "Email", "FechaNacimiento", "Nacionalidad", "Nombre", "NroDocumento", "PaisId", "SexoId", "Telefono", "TipoDocumento" },
                values: new object[] { 2, "Lopez", "lucas@gmail.com", new DateTime(2020, 7, 7, 0, 0, 0, 0, DateTimeKind.Local), null, "Maria", "12345679", 2, 2, "2222", "dni" });

            migrationBuilder.CreateIndex(
                name: "IX_Persona_PaisId",
                table: "Persona",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_SexoId",
                table: "Persona",
                column: "SexoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persona");

            migrationBuilder.DropTable(
                name: "Pais");

            migrationBuilder.DropTable(
                name: "Sexo");
        }
    }
}
