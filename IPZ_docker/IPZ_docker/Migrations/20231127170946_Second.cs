using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPZ_docker.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_Cars_CarId",
                table: "Purchase");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_Clients_ClientId",
                table: "Purchase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchase",
                table: "Purchase");

            migrationBuilder.DropIndex(
                name: "IX_Purchase_CarId",
                table: "Purchase");

            migrationBuilder.RenameTable(
                name: "Purchase",
                newName: "Purchases");

            migrationBuilder.RenameIndex(
                name: "IX_Purchase_ClientId",
                table: "Purchases",
                newName: "IX_Purchases_ClientId");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Purchases",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "Purchases",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                column: "PurchaseId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                column: "Sex",
                value: "Male");

            migrationBuilder.InsertData(
                table: "Purchases",
                columns: new[] { "Id", "CarId", "ClientId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_PurchaseId",
                table: "Cars",
                column: "PurchaseId",
                unique: true,
                filter: "[PurchaseId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Purchases_PurchaseId",
                table: "Cars",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Clients_ClientId",
                table: "Purchases",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Purchases_PurchaseId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Clients_ClientId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Cars_PurchaseId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases");

            migrationBuilder.DeleteData(
                table: "Purchases",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "Cars");

            migrationBuilder.RenameTable(
                name: "Purchases",
                newName: "Purchase");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_ClientId",
                table: "Purchase",
                newName: "IX_Purchase_ClientId");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Purchase",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "Purchase",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchase",
                table: "Purchase",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                column: "Sex",
                value: "No");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_CarId",
                table: "Purchase",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_Cars_CarId",
                table: "Purchase",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_Clients_ClientId",
                table: "Purchase",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }
    }
}
