using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTableUsedCoupon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsedCoupons",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CouponId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsedCoupons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsedCoupons_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsedCoupons_Coupons_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsedCoupons_CouponId",
                table: "UsedCoupons",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_UsedCoupons_UserId",
                table: "UsedCoupons",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsedCoupons");
        }
    }
}
