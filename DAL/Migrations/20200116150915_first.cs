using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DAL.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "HistoricalEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EntityId = table.Column<string>(nullable: true),
                    EntityName = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricalEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "t_account",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoricalChangeset",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EventId = table.Column<Guid>(nullable: false),
                    ObjectData = table.Column<string>(nullable: true),
                    ObjectDelta = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricalChangeset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricalChangeset_HistoricalEvents_EventId",
                        column: x => x.EventId,
                        principalTable: "HistoricalEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_contact",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Address = table.Column<string>(maxLength: 250, nullable: false),
                    Phone = table.Column<string>(maxLength: 18, nullable: false),
                    AccountId = table.Column<int>(nullable: false),
                    AccountId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_contact_t_account_AccountId1",
                        column: x => x.AccountId1,
                        principalSchema: "public",
                        principalTable: "t_account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricalChangeset_EventId",
                table: "HistoricalChangeset",
                column: "EventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_contact_AccountId1",
                schema: "public",
                table: "t_contact",
                column: "AccountId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricalChangeset");

            migrationBuilder.DropTable(
                name: "t_contact",
                schema: "public");

            migrationBuilder.DropTable(
                name: "HistoricalEvents");

            migrationBuilder.DropTable(
                name: "t_account",
                schema: "public");
        }
    }
}
