using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FileSageAPI.Migrations
{
    /// <inheritdoc />
    public partial class projectstart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "archive",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RegisteredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_archive", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "author",
                columns: table => new
                {
                    ArchiveId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_author", x => x.ArchiveId);
                    table.ForeignKey(
                        name: "FK_author_archive_ArchiveId",
                        column: x => x.ArchiveId,
                        principalTable: "archive",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "book",
                columns: table => new
                {
                    ArchiveId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Summary = table.Column<string>(type: "text", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    Rating = table.Column<double>(type: "double precision", precision: 3, scale: 1, nullable: true),
                    Words = table.Column<int>(type: "integer", nullable: true),
                    TotalChapters = table.Column<int>(type: "integer", nullable: true),
                    CurrentChapter = table.Column<int>(type: "integer", nullable: true),
                    ReadingStatus = table.Column<int>(type: "integer", nullable: true),
                    WritingStatus = table.Column<int>(type: "integer", nullable: true),
                    Published = table.Column<DateOnly>(type: "date", nullable: true),
                    LastUpdate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book", x => x.ArchiveId);
                    table.ForeignKey(
                        name: "FK_book_archive_ArchiveId",
                        column: x => x.ArchiveId,
                        principalTable: "archive",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "url",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    ArchiveId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_url", x => x.Id);
                    table.ForeignKey(
                        name: "FK_url_archive_ArchiveId",
                        column: x => x.ArchiveId,
                        principalTable: "archive",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "archive_tag",
                columns: table => new
                {
                    ArchiveId = table.Column<int>(type: "integer", nullable: false),
                    TagId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_archive_tag", x => new { x.TagId, x.ArchiveId });
                    table.ForeignKey(
                        name: "FK_archive_tag_archive_ArchiveId",
                        column: x => x.ArchiveId,
                        principalTable: "archive",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_archive_tag_tag_TagId",
                        column: x => x.TagId,
                        principalTable: "tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "book_author",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "integer", nullable: false),
                    AuthorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book_author", x => new { x.AuthorId, x.BookId });
                    table.ForeignKey(
                        name: "FK_book_author_author_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "author",
                        principalColumn: "ArchiveId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_book_author_book_BookId",
                        column: x => x.BookId,
                        principalTable: "book",
                        principalColumn: "ArchiveId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_archive_tag_ArchiveId",
                table: "archive_tag",
                column: "ArchiveId");

            migrationBuilder.CreateIndex(
                name: "IX_book_author_BookId",
                table: "book_author",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_tag_Type_Content",
                table: "tag",
                columns: new[] { "Type", "Content" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_url_ArchiveId",
                table: "url",
                column: "ArchiveId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "archive_tag");

            migrationBuilder.DropTable(
                name: "book_author");

            migrationBuilder.DropTable(
                name: "url");

            migrationBuilder.DropTable(
                name: "tag");

            migrationBuilder.DropTable(
                name: "author");

            migrationBuilder.DropTable(
                name: "book");

            migrationBuilder.DropTable(
                name: "archive");
        }
    }
}
