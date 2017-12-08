using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace recru_it.Data.Migrations
{
    public partial class MultipleTagsForJobPostAndApplications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_Tags_TagsId",
                table: "JobApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_JobPosts_Tags_TagsId",
                table: "JobPosts");

            migrationBuilder.DropIndex(
                name: "IX_JobPosts_TagsId",
                table: "JobPosts");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_TagsId",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "TagsId",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "TagsId",
                table: "JobApplications");

            migrationBuilder.AddColumn<string>(
                name: "JobApplicationId",
                table: "Tags",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobPostId",
                table: "Tags",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "Profiles",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "JobPosts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedBy",
                table: "JobPosts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "JobPosts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_JobApplicationId",
                table: "Tags",
                column: "JobApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_JobPostId",
                table: "Tags",
                column: "JobPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_JobApplications_JobApplicationId",
                table: "Tags",
                column: "JobApplicationId",
                principalTable: "JobApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_JobPosts_JobPostId",
                table: "Tags",
                column: "JobPostId",
                principalTable: "JobPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_JobApplications_JobApplicationId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_JobPosts_JobPostId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_JobApplicationId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_JobPostId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "JobApplicationId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "JobPostId",
                table: "Tags");

            migrationBuilder.AlterColumn<string>(
                name: "Age",
                table: "Profiles",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedAt",
                table: "JobPosts",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "JobPosts",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedAt",
                table: "JobPosts",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "TagsId",
                table: "JobPosts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TagsId",
                table: "JobApplications",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobPosts_TagsId",
                table: "JobPosts",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_TagsId",
                table: "JobApplications",
                column: "TagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_Tags_TagsId",
                table: "JobApplications",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobPosts_Tags_TagsId",
                table: "JobPosts",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
