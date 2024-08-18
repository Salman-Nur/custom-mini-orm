using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment3.Migrations
{
    /// <inheritdoc />
    public partial class CreateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubClass1",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Property1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Property2 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubClass1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Temp3",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temp3", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Enlisted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Color_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Color_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TestClass2",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Class1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestClass2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestClass2_SubClass1_Class1Id",
                        column: x => x.Class1Id,
                        principalTable: "SubClass1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeedbackGiverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedback_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Feedback_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Feedback_User_FeedbackGiverId",
                        column: x => x.FeedbackGiverId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubClass2",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Property1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Property2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestClass2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubClass2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubClass2_TestClass2_TestClass2Id",
                        column: x => x.TestClass2Id,
                        principalTable: "TestClass2",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TestClass1",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Class2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestClass1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestClass1_SubClass2_Class2Id",
                        column: x => x.Class2Id,
                        principalTable: "SubClass2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Temp1",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestClass1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TestClass2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temp1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Temp1_TestClass1_TestClass1Id",
                        column: x => x.TestClass1Id,
                        principalTable: "TestClass1",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Temp1_TestClass2_TestClass2Id",
                        column: x => x.TestClass2Id,
                        principalTable: "TestClass2",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Temp2",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TempId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Temp1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temp2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Temp2_Temp1_Temp1Id",
                        column: x => x.Temp1Id,
                        principalTable: "Temp1",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Temp2_Temp3_TempId",
                        column: x => x.TempId,
                        principalTable: "Temp3",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Color_ItemId",
                table: "Color",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Color_ProductId",
                table: "Color",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_FeedbackGiverId",
                table: "Feedback",
                column: "FeedbackGiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_ItemId",
                table: "Feedback",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_ProductId",
                table: "Feedback",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SubClass2_TestClass2Id",
                table: "SubClass2",
                column: "TestClass2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Temp1_TestClass1Id",
                table: "Temp1",
                column: "TestClass1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Temp1_TestClass2Id",
                table: "Temp1",
                column: "TestClass2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Temp2_Temp1Id",
                table: "Temp2",
                column: "Temp1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Temp2_TempId",
                table: "Temp2",
                column: "TempId");

            migrationBuilder.CreateIndex(
                name: "IX_TestClass1_Class2Id",
                table: "TestClass1",
                column: "Class2Id");

            migrationBuilder.CreateIndex(
                name: "IX_TestClass2_Class1Id",
                table: "TestClass2",
                column: "Class1Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "Temp2");

            migrationBuilder.DropTable(
                name: "Vendor");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Temp1");

            migrationBuilder.DropTable(
                name: "Temp3");

            migrationBuilder.DropTable(
                name: "TestClass1");

            migrationBuilder.DropTable(
                name: "SubClass2");

            migrationBuilder.DropTable(
                name: "TestClass2");

            migrationBuilder.DropTable(
                name: "SubClass1");
        }
    }
}
