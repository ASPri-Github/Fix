using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApisConvenciones9.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cat_Actividades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cat_Actividades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cat_Recomendaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cat_Recomendaciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cat_Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cat_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreConvencion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtitulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    Fecha_inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fecha_fin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitud = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitud = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LugarDestino = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Perfil_Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfil_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Versiones_App",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Version_Android = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version_IOs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Token_Map_Box = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versiones_App", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Actividades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtitulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Especificaciones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Evento_IdId = table.Column<int>(type: "int", nullable: false),
                    Categoria_IdId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actividades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actividades_Cat_Actividades_Categoria_IdId",
                        column: x => x.Categoria_IdId,
                        principalTable: "Cat_Actividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Actividades_Eventos_Evento_IdId",
                        column: x => x.Evento_IdId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hoteles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreHotel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitud = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitud = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Evento_IdId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hoteles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hoteles_Eventos_Evento_IdId",
                        column: x => x.Evento_IdId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pregunta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Evento_IdId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pregunta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pregunta_Eventos_Evento_IdId",
                        column: x => x.Evento_IdId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recomendaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Informacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitud = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitud = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Evento_IdId = table.Column<int>(type: "int", nullable: false),
                    CategoriaRecomendacion_IdId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recomendaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recomendaciones_Cat_Recomendaciones_CategoriaRecomendacion_IdId",
                        column: x => x.CategoriaRecomendacion_IdId,
                        principalTable: "Cat_Recomendaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recomendaciones_Eventos_Evento_IdId",
                        column: x => x.Evento_IdId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vuelos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha_Vuelo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reservacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero_Vuelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Asiento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Origen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lugar_Origen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hora_Salida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destino = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lugar_Destino = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hora_Llegada = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detalle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Evento_IdId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vuelos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vuelos_Eventos_Evento_IdId",
                        column: x => x.Evento_IdId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Convencionistas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Puesto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Evento_IdId = table.Column<int>(type: "int", nullable: false),
                    PerfilUsuario_idId = table.Column<int>(type: "int", nullable: false),
                    CategoriaUsuario_IdId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Convencionistas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Convencionistas_Cat_Usuarios_CategoriaUsuario_IdId",
                        column: x => x.CategoriaUsuario_IdId,
                        principalTable: "Cat_Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Convencionistas_Eventos_Evento_IdId",
                        column: x => x.Evento_IdId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Convencionistas_Perfil_Usuario_PerfilUsuario_idId",
                        column: x => x.PerfilUsuario_idId,
                        principalTable: "Perfil_Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Respuestas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pregunta_IdId = table.Column<int>(type: "int", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respuestas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Respuestas_Pregunta_Pregunta_IdId",
                        column: x => x.Pregunta_IdId,
                        principalTable: "Pregunta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actividades_Categoria_IdId",
                table: "Actividades",
                column: "Categoria_IdId");

            migrationBuilder.CreateIndex(
                name: "IX_Actividades_Evento_IdId",
                table: "Actividades",
                column: "Evento_IdId");

            migrationBuilder.CreateIndex(
                name: "IX_Convencionistas_CategoriaUsuario_IdId",
                table: "Convencionistas",
                column: "CategoriaUsuario_IdId");

            migrationBuilder.CreateIndex(
                name: "IX_Convencionistas_Evento_IdId",
                table: "Convencionistas",
                column: "Evento_IdId");

            migrationBuilder.CreateIndex(
                name: "IX_Convencionistas_PerfilUsuario_idId",
                table: "Convencionistas",
                column: "PerfilUsuario_idId");

            migrationBuilder.CreateIndex(
                name: "IX_Hoteles_Evento_IdId",
                table: "Hoteles",
                column: "Evento_IdId");

            migrationBuilder.CreateIndex(
                name: "IX_Pregunta_Evento_IdId",
                table: "Pregunta",
                column: "Evento_IdId");

            migrationBuilder.CreateIndex(
                name: "IX_Recomendaciones_CategoriaRecomendacion_IdId",
                table: "Recomendaciones",
                column: "CategoriaRecomendacion_IdId");

            migrationBuilder.CreateIndex(
                name: "IX_Recomendaciones_Evento_IdId",
                table: "Recomendaciones",
                column: "Evento_IdId");

            migrationBuilder.CreateIndex(
                name: "IX_Respuestas_Pregunta_IdId",
                table: "Respuestas",
                column: "Pregunta_IdId");

            migrationBuilder.CreateIndex(
                name: "IX_Vuelos_Evento_IdId",
                table: "Vuelos",
                column: "Evento_IdId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actividades");

            migrationBuilder.DropTable(
                name: "Convencionistas");

            migrationBuilder.DropTable(
                name: "Hoteles");

            migrationBuilder.DropTable(
                name: "Recomendaciones");

            migrationBuilder.DropTable(
                name: "Respuestas");

            migrationBuilder.DropTable(
                name: "Versiones_App");

            migrationBuilder.DropTable(
                name: "Vuelos");

            migrationBuilder.DropTable(
                name: "Cat_Actividades");

            migrationBuilder.DropTable(
                name: "Cat_Usuarios");

            migrationBuilder.DropTable(
                name: "Perfil_Usuario");

            migrationBuilder.DropTable(
                name: "Cat_Recomendaciones");

            migrationBuilder.DropTable(
                name: "Pregunta");

            migrationBuilder.DropTable(
                name: "Eventos");
        }
    }
}
