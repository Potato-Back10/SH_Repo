using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Gamza.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Battles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IsFinished = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    StartBattle = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndBattle = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Battles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CombatLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BattleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SourceParticipantId = table.Column<long>(type: "bigint", nullable: true),
                    TargetParticipantId = table.Column<long>(type: "bigint", nullable: true),
                    Ts = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Payload = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombatLogs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tier = table.Column<int>(type: "int", nullable: false),
                    MinLevel = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_Jobs_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Monsters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    MaxHP = table.Column<int>(type: "int", nullable: false),
                    HP = table.Column<int>(type: "int", nullable: false),
                    Attack = table.Column<int>(type: "int", nullable: false),
                    Defense = table.Column<int>(type: "int", nullable: false),
                    RewardExp = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monsters", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RewardGrants",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BattleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    Ts = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RewardGrants", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoginID = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BattleParticipants",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BattleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ActorId = table.Column<int>(type: "int", nullable: false),
                    Team = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    MaxHP = table.Column<int>(type: "int", nullable: false),
                    HP = table.Column<int>(type: "int", nullable: false),
                    Attack = table.Column<int>(type: "int", nullable: false),
                    Defense = table.Column<int>(type: "int", nullable: false),
                    AttackSpeed = table.Column<float>(type: "float", nullable: false),
                    MoveSpeed = table.Column<float>(type: "float", nullable: false),
                    AttackCoolTime = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattleParticipants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BattleParticipants_Battles_BattleId",
                        column: x => x.BattleId,
                        principalTable: "Battles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NickName = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Job = table.Column<int>(type: "int", nullable: true),
                    CurrentJobId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Exp = table.Column<int>(type: "int", nullable: false),
                    PlayerStauts = table.Column<int>(type: "int", nullable: false),
                    PlayerSecondStauts = table.Column<int>(type: "int", nullable: false),
                    ExpToNext = table.Column<long>(type: "bigint", nullable: false),
                    MaxHP = table.Column<int>(type: "int", nullable: false),
                    HP = table.Column<int>(type: "int", nullable: false),
                    MaxMP = table.Column<int>(type: "int", nullable: false),
                    MP = table.Column<int>(type: "int", nullable: false),
                    Defense = table.Column<int>(type: "int", nullable: false),
                    PhysicalAttack = table.Column<int>(type: "int", nullable: false),
                    MagicAttack = table.Column<int>(type: "int", nullable: false),
                    AttackSpeed = table.Column<float>(type: "float", nullable: false),
                    MoveSpeed = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Jobs_CurrentJobId",
                        column: x => x.CurrentJobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Players_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PlayerJobHistories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    ChangedAtUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerJobHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerJobHistories_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerJobHistories_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Code", "MinLevel", "Name", "ParentId", "Tier" },
                values: new object[,]
                {
                    { 1, "warrior", 10, "워리어", null, 1 },
                    { 2, "thief", 10, "도적", null, 1 },
                    { 3, "mage", 10, "법사", null, 1 },
                    { 4, "archer", 10, "궁수", null, 1 },
                    { 5, "healer", 10, "힐러", null, 1 },
                    { 10, "knight", 30, "나이트", 1, 2 },
                    { 11, "spearman", 30, "스피어맨", 1, 2 },
                    { 12, "ronin", 30, "로닌", 1, 2 },
                    { 30, "assassin", 30, "어쌔신", 2, 2 },
                    { 31, "shadowdancer", 30, "쉐도우댄서", 2, 2 },
                    { 32, "phantom", 30, "팬텀", 2, 2 },
                    { 50, "archMage", 30, "아크메이지", 3, 2 },
                    { 51, "darkMage", 30, "흑마법사", 3, 2 },
                    { 52, "timeMage", 30, "시간법사", 3, 2 },
                    { 70, "hunter", 30, "헌터", 4, 2 },
                    { 71, "sniper", 30, "명사수", 4, 2 },
                    { 72, "TrickArcher", 30, "트릭아처", 4, 2 },
                    { 90, "cleric", 30, "클레릭", 5, 2 },
                    { 91, "onmyoji", 30, "음양사", 5, 2 },
                    { 92, "alchemist", 30, "연금술사", 5, 2 },
                    { 20, "paladin", 70, "팔라딘", 10, 3 },
                    { 21, "dragon_knight", 70, "드래곤 나이트", 11, 3 },
                    { 22, "blade", 70, "블레이드", 12, 3 },
                    { 40, "reaper", 70, "리퍼", 30, 3 },
                    { 41, "specter", 70, "스펙터", 31, 3 },
                    { 42, "raven", 70, "레이븐", 32, 3 },
                    { 60, "grandsorcerer", 70, "그랜드소서러", 50, 3 },
                    { 61, "necromancer", 70, "네크로맨서", 51, 3 },
                    { 62, "chronotrigger", 70, "크로노트리거", 52, 3 },
                    { 80, "beastmaster", 70, "비스트마스터", 70, 3 },
                    { 81, "deadeye", 70, "데드아이", 71, 3 },
                    { 82, "mirage", 70, "미라지", 72, 3 },
                    { 100, "priest", 70, "프리스트", 90, 3 },
                    { 101, "sorcerer", 70, "점술사", 91, 3 },
                    { 102, "homunculus", 70, "호문쿨루스", 92, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BattleParticipants_BattleId",
                table: "BattleParticipants",
                column: "BattleId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_ParentId",
                table: "Jobs",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerJobHistories_JobId",
                table: "PlayerJobHistories",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerJobHistories_PlayerId",
                table: "PlayerJobHistories",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_CurrentJobId",
                table: "Players",
                column: "CurrentJobId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_NickName",
                table: "Players",
                column: "NickName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_UserId",
                table: "Players",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RewardGrants_BattleId_PlayerId",
                table: "RewardGrants",
                columns: new[] { "BattleId", "PlayerId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BattleParticipants");

            migrationBuilder.DropTable(
                name: "CombatLogs");

            migrationBuilder.DropTable(
                name: "Monsters");

            migrationBuilder.DropTable(
                name: "PlayerJobHistories");

            migrationBuilder.DropTable(
                name: "RewardGrants");

            migrationBuilder.DropTable(
                name: "Battles");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
