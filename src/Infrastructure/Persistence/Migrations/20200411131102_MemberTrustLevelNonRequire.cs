using Microsoft.EntityFrameworkCore.Migrations;

namespace Codidact.Core.Infrastructure.Persistence.Migrations
{
    public partial class MemberTrustLevelNonRequire : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "trust_level_id",
                table: "members",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "trust_level_id",
                table: "members",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
