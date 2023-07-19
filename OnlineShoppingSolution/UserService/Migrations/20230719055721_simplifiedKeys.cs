using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class simplifiedKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Address_AddressUserId",
                schema: "Users",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Password_PasswordUserId",
                schema: "Users",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "PasswordUserId",
                schema: "Users",
                table: "User",
                newName: "PasswordId");

            migrationBuilder.RenameColumn(
                name: "AddressUserId",
                schema: "Users",
                table: "User",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_User_PasswordUserId",
                schema: "Users",
                table: "User",
                newName: "IX_User_PasswordId");

            migrationBuilder.RenameIndex(
                name: "IX_User_AddressUserId",
                schema: "Users",
                table: "User",
                newName: "IX_User_AddressId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "Users",
                table: "Password",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Password_UserId",
                schema: "Users",
                table: "Password",
                newName: "IX_Password_Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "Users",
                table: "Address",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Address_UserId",
                schema: "Users",
                table: "Address",
                newName: "IX_Address_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Address_AddressId",
                schema: "Users",
                table: "User",
                column: "AddressId",
                principalSchema: "Users",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Password_PasswordId",
                schema: "Users",
                table: "User",
                column: "PasswordId",
                principalSchema: "Users",
                principalTable: "Password",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Address_AddressId",
                schema: "Users",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Password_PasswordId",
                schema: "Users",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "PasswordId",
                schema: "Users",
                table: "User",
                newName: "PasswordUserId");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                schema: "Users",
                table: "User",
                newName: "AddressUserId");

            migrationBuilder.RenameIndex(
                name: "IX_User_PasswordId",
                schema: "Users",
                table: "User",
                newName: "IX_User_PasswordUserId");

            migrationBuilder.RenameIndex(
                name: "IX_User_AddressId",
                schema: "Users",
                table: "User",
                newName: "IX_User_AddressUserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Users",
                table: "Password",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Password_Id",
                schema: "Users",
                table: "Password",
                newName: "IX_Password_UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Users",
                table: "Address",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_Id",
                schema: "Users",
                table: "Address",
                newName: "IX_Address_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Address_AddressUserId",
                schema: "Users",
                table: "User",
                column: "AddressUserId",
                principalSchema: "Users",
                principalTable: "Address",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Password_PasswordUserId",
                schema: "Users",
                table: "User",
                column: "PasswordUserId",
                principalSchema: "Users",
                principalTable: "Password",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
