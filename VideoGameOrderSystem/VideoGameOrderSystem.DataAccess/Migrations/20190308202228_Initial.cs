using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoGameOrderSystem.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "OS");

            migrationBuilder.CreateTable(
                name: "IBundles",
                schema: "OS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IBundles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "IProduct",
                schema: "OS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IProduct", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OBundles",
                schema: "OS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OBundles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OProduct",
                schema: "OS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OProduct", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                schema: "OS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "IBundleItems",
                schema: "OS",
                columns: table => new
                {
                    BundleID = table.Column<int>(nullable: false),
                    ProductID = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IBundleItems", x => new { x.BundleID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_BundleItems_To_IBundles",
                        column: x => x.BundleID,
                        principalSchema: "OS",
                        principalTable: "IBundles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BundleItems_To_IProduct",
                        column: x => x.ProductID,
                        principalSchema: "OS",
                        principalTable: "IProduct",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OBundleItems",
                schema: "OS",
                columns: table => new
                {
                    BundleID = table.Column<int>(nullable: false),
                    ProductID = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OBundleItems", x => new { x.BundleID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_BundleItems_To_OBundles",
                        column: x => x.BundleID,
                        principalSchema: "OS",
                        principalTable: "OBundles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BundleItems_To_OProduct",
                        column: x => x.ProductID,
                        principalSchema: "OS",
                        principalTable: "OProduct",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "OS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Birthday = table.Column<DateTime>(nullable: false),
                    StoreID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Customer_To_Store",
                        column: x => x.StoreID,
                        principalSchema: "OS",
                        principalTable: "Store",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                schema: "OS",
                columns: table => new
                {
                    StoreID = table.Column<int>(nullable: false),
                    ProductID = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => new { x.StoreID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_Inventory_To_Product",
                        column: x => x.ProductID,
                        principalSchema: "OS",
                        principalTable: "IProduct",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventory_To_Store",
                        column: x => x.StoreID,
                        principalSchema: "OS",
                        principalTable: "Store",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "OS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StoreID = table.Column<int>(nullable: false),
                    CustomerID = table.Column<int>(nullable: false),
                    TimePlaced = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Order_To_Customer",
                        column: x => x.CustomerID,
                        principalSchema: "OS",
                        principalTable: "Customer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_To_Store",
                        column: x => x.StoreID,
                        principalSchema: "OS",
                        principalTable: "Store",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                schema: "OS",
                columns: table => new
                {
                    OrderID = table.Column<int>(nullable: false),
                    ProductID = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => new { x.OrderID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_OrderItems_To_Order",
                        column: x => x.OrderID,
                        principalSchema: "OS",
                        principalTable: "Orders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_To_Product",
                        column: x => x.ProductID,
                        principalSchema: "OS",
                        principalTable: "OProduct",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_StoreID",
                schema: "OS",
                table: "Customer",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_IBundleItems_ProductID",
                schema: "OS",
                table: "IBundleItems",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ProductID",
                schema: "OS",
                table: "Inventory",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_OBundleItems_ProductID",
                schema: "OS",
                table: "OBundleItems",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductID",
                schema: "OS",
                table: "OrderItems",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerID",
                schema: "OS",
                table: "Orders",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StoreID",
                schema: "OS",
                table: "Orders",
                column: "StoreID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IBundleItems",
                schema: "OS");

            migrationBuilder.DropTable(
                name: "Inventory",
                schema: "OS");

            migrationBuilder.DropTable(
                name: "OBundleItems",
                schema: "OS");

            migrationBuilder.DropTable(
                name: "OrderItems",
                schema: "OS");

            migrationBuilder.DropTable(
                name: "IBundles",
                schema: "OS");

            migrationBuilder.DropTable(
                name: "IProduct",
                schema: "OS");

            migrationBuilder.DropTable(
                name: "OBundles",
                schema: "OS");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "OS");

            migrationBuilder.DropTable(
                name: "OProduct",
                schema: "OS");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "OS");

            migrationBuilder.DropTable(
                name: "Store",
                schema: "OS");
        }
    }
}
