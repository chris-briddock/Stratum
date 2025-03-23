using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessageBroker.Persistence.Migrations.WriteMigrations
{
    /// <inheritdoc />
    public partial class InitialWriteMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SYSTEM_CLIENT_APPLICATIONS",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    SessionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
                    PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodStartColumn", true),
                    created_by = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    created_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    deleted_by = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    deleted_on_utc = table.Column<DateTime>(type: "datetime2", maxLength: 36, nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    EntityModificationStatus_ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETUTCDATE()"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    api_key = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYSTEM_CLIENT_APPLICATIONS", x => x.id);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "SYSTEM_CLIENT_APPLICATIONSHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.CreateTable(
                name: "SYSTEM_TOPICS",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
                    PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodStartColumn", true),
                    created_by = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    created_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    deleted_by = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    deleted_on_utc = table.Column<DateTime>(type: "datetime2", maxLength: 36, nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    EntityModificationStatus_ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETUTCDATE()"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    concurrency_stamp = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYSTEM_TOPICS", x => x.id);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "SYSTEM_TOPICSHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.CreateTable(
                name: "SYSTEM_SESSIONS",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ClientApplicationId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
                    PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodStartColumn", true),
                    created_by = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    created_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    deleted_by = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    deleted_on_utc = table.Column<DateTime>(type: "datetime2", maxLength: 36, nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    session_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    user_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    start_date_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ip_address = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    user_agent = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    concurrency_stamp = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYSTEM_SESSIONS", x => x.id);
                    table.ForeignKey(
                        name: "FK_SYSTEM_SESSIONS_SYSTEM_CLIENT_APPLICATIONS_ClientApplicationId",
                        column: x => x.ClientApplicationId,
                        principalTable: "SYSTEM_CLIENT_APPLICATIONS",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "SYSTEM_SESSIONSHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.CreateTable(
                name: "SYSTEM_EVENTS",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    TopicId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
                    PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodStartColumn", true),
                    created_by = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    created_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    type = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    payload = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    concurrency_stamp = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYSTEM_EVENTS", x => x.id);
                    table.ForeignKey(
                        name: "FK_SYSTEM_EVENTS_SYSTEM_TOPICS_TopicId",
                        column: x => x.TopicId,
                        principalTable: "SYSTEM_TOPICS",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "SYSTEM_EVENTSHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.CreateTable(
                name: "SYSTEM_SUBSCRIPTIONS",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    TopicId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    ClientApplicationId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    ClientApplicationId1 = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
                    PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:TemporalIsPeriodStartColumn", true),
                    topic_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    created_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    deleted_by = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    deleted_on_utc = table.Column<DateTime>(type: "datetime2", maxLength: 36, nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    modified_by = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    EntityModificationStatus_ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETUTCDATE()"),
                    type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    concurrency_stamp = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYSTEM_SUBSCRIPTIONS", x => x.id);
                    table.ForeignKey(
                        name: "FK_SYSTEM_SUBSCRIPTIONS_SYSTEM_CLIENT_APPLICATIONS_ClientApplicationId",
                        column: x => x.ClientApplicationId,
                        principalTable: "SYSTEM_CLIENT_APPLICATIONS",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SYSTEM_SUBSCRIPTIONS_SYSTEM_CLIENT_APPLICATIONS_ClientApplicationId1",
                        column: x => x.ClientApplicationId1,
                        principalTable: "SYSTEM_CLIENT_APPLICATIONS",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_SYSTEM_SUBSCRIPTIONS_SYSTEM_TOPICS_TopicId",
                        column: x => x.TopicId,
                        principalTable: "SYSTEM_TOPICS",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "SYSTEM_SUBSCRIPTIONSHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.CreateIndex(
                name: "IX_SYSTEM_CLIENT_APPLICATIONS_api_key",
                table: "SYSTEM_CLIENT_APPLICATIONS",
                column: "api_key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SYSTEM_CLIENT_APPLICATIONS_concurrency_stamp",
                table: "SYSTEM_CLIENT_APPLICATIONS",
                column: "concurrency_stamp",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SYSTEM_EVENTS_concurrency_stamp",
                table: "SYSTEM_EVENTS",
                column: "concurrency_stamp",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SYSTEM_EVENTS_TopicId",
                table: "SYSTEM_EVENTS",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_SYSTEM_SESSIONS_ClientApplicationId",
                table: "SYSTEM_SESSIONS",
                column: "ClientApplicationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SYSTEM_SUBSCRIPTIONS_ClientApplicationId",
                table: "SYSTEM_SUBSCRIPTIONS",
                column: "ClientApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_SYSTEM_SUBSCRIPTIONS_ClientApplicationId1",
                table: "SYSTEM_SUBSCRIPTIONS",
                column: "ClientApplicationId1");

            migrationBuilder.CreateIndex(
                name: "IX_SYSTEM_SUBSCRIPTIONS_concurrency_stamp",
                table: "SYSTEM_SUBSCRIPTIONS",
                column: "concurrency_stamp",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SYSTEM_SUBSCRIPTIONS_TopicId",
                table: "SYSTEM_SUBSCRIPTIONS",
                column: "TopicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SYSTEM_EVENTS")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "SYSTEM_EVENTSHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.DropTable(
                name: "SYSTEM_SESSIONS")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "SYSTEM_SESSIONSHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.DropTable(
                name: "SYSTEM_SUBSCRIPTIONS")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "SYSTEM_SUBSCRIPTIONSHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.DropTable(
                name: "SYSTEM_CLIENT_APPLICATIONS")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "SYSTEM_CLIENT_APPLICATIONSHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.DropTable(
                name: "SYSTEM_TOPICS")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "SYSTEM_TOPICSHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");
        }
    }
}
