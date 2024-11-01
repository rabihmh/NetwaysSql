using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NetwaysSql.Model.Database.Migrations
{
    /// <inheritdoc />
    public partial class SeedTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("13711902-594b-4174-b2f2-90ce83f9230b"), "Home Appliances" },
                    { new Guid("250c8a57-a911-40ff-9cf8-77ec4a39e191"), "For Kids" },
                    { new Guid("ae3f7f4f-c8d4-4cb5-ad8c-976f0df5828c"), "Books" },
                    { new Guid("c8011bc5-f5ef-496e-a4b5-3310701e913f"), "Electronics" },
                    { new Guid("db1c84a4-003f-4b6f-af67-ae2dcde31568"), "Toys" },
                    { new Guid("f0b308a1-07ff-40fc-96db-5be6e2e4047b"), "Health" },
                    { new Guid("f6ac494a-0a3d-473b-b22b-875a9680a092"), "Sport" },
                    { new Guid("f847b4bf-3e60-408d-a494-93c118501e7c"), "Fashion" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("13711902-594b-4174-b2f2-90ce83f9230b"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("250c8a57-a911-40ff-9cf8-77ec4a39e191"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("ae3f7f4f-c8d4-4cb5-ad8c-976f0df5828c"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("c8011bc5-f5ef-496e-a4b5-3310701e913f"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("db1c84a4-003f-4b6f-af67-ae2dcde31568"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("f0b308a1-07ff-40fc-96db-5be6e2e4047b"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("f6ac494a-0a3d-473b-b22b-875a9680a092"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("f847b4bf-3e60-408d-a494-93c118501e7c"));
        }
    }
}
