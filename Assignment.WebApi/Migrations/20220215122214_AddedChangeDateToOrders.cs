using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment.WebApi.Migrations
{
    public partial class AddedChangeDateToOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "OrderChangeDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderChangeDate",
                table: "Orders");
        }
    }
}
