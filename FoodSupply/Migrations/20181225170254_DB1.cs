using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodSupply.Migrations
{
    public partial class DB1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Supplier",
                table: "Goods",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Goods",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GoodsSets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GoodsInSets",
                columns: table => new
                {
                    IdGoods = table.Column<int>(nullable: false),
                    IdSet = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsInSets", x => new { x.IdGoods, x.IdSet });
                    table.ForeignKey(
                        name: "FK_GoodsInSets_Goods_IdGoods",
                        column: x => x.IdGoods,
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodsInSets_GoodsSets_IdSet",
                        column: x => x.IdSet,
                        principalTable: "GoodsSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SetOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdUser = table.Column<string>(nullable: true),
                    IdSet = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SetOrders_GoodsSets_IdSet",
                        column: x => x.IdSet,
                        principalTable: "GoodsSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SetOrders_AspNetUsers_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoodsInSets_IdSet",
                table: "GoodsInSets",
                column: "IdSet");

            migrationBuilder.CreateIndex(
                name: "IX_SetOrders_IdSet",
                table: "SetOrders",
                column: "IdSet");

            migrationBuilder.CreateIndex(
                name: "IX_SetOrders_IdUser",
                table: "SetOrders",
                column: "IdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoodsInSets");

            migrationBuilder.DropTable(
                name: "SetOrders");

            migrationBuilder.DropTable(
                name: "GoodsSets");

            migrationBuilder.DropColumn(
                name: "Supplier",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Goods");
        }
    }
}
