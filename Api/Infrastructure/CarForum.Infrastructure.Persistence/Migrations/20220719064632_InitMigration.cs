﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarForum.Infrastructure.Persistence.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        { 
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "user",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAdress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "entry",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entry_user_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "dbo",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntryComment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryComment_entry_EntryId",
                        column: x => x.EntryId,
                        principalSchema: "dbo",
                        principalTable: "entry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntryComment_user_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "dbo",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "entryfavorite",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entryfavorite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entryfavorite_entry_EntryId",
                        column: x => x.EntryId,
                        principalSchema: "dbo",
                        principalTable: "entry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_entryfavorite_user_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "dbo",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "entryvote",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoteType = table.Column<int>(type: "int", nullable: false),
                    EntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entryvote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entryvote_entry_EntryId",
                        column: x => x.EntryId,
                        principalSchema: "dbo",
                        principalTable: "entry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "entrycommentfavorite",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entrycommentfavorite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entrycommentfavorite_EntryComment_EntryCommentId",
                        column: x => x.EntryCommentId,
                        principalTable: "EntryComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_entrycommentfavorite_user_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "dbo",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "entrycommentvote",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoteType = table.Column<int>(type: "int", nullable: false),
                    EntryCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entrycommentvote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entrycommentvote_EntryComment_EntryCommentId",
                        column: x => x.EntryCommentId,
                        principalTable: "EntryComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_entry_CreatedById",
                schema: "dbo",
                table: "entry",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EntryComment_CreateById",
                table: "EntryComment",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_EntryComment_EntryId",
                table: "EntryComment",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_entrycommentfavorite_CreateById",
                schema: "dbo",
                table: "entrycommentfavorite",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_entrycommentfavorite_EntryCommentId",
                schema: "dbo",
                table: "entrycommentfavorite",
                column: "EntryCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_entrycommentvote_EntryCommentId",
                schema: "dbo",
                table: "entrycommentvote",
                column: "EntryCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_entryfavorite_CreatedById",
                schema: "dbo",
                table: "entryfavorite",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_entryfavorite_EntryId",
                schema: "dbo",
                table: "entryfavorite",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_entryvote_EntryId",
                schema: "dbo",
                table: "entryvote",
                column: "EntryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "entrycommentfavorite",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "entrycommentvote",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "entryfavorite",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "entryvote",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EntryComment");

            migrationBuilder.DropTable(
                name: "entry",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "user",
                schema: "dbo");
        }
    }
}
