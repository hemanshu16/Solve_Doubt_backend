﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Online_Discussion_Forum.Models;

#nullable disable

namespace Online_Discussion_Forum.Migrations
{
    [DbContext(typeof(DiscussionDbContext))]
    partial class DiscussionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Online_Discussion_Forum.Models.Answers", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Answer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("Question_id")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("Update_date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Upvotes")
                        .HasColumnType("int");

                    b.Property<long?>("User_id")
                        .HasColumnType("bigint");

                    b.Property<string>("username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Answers_");
                });

            modelBuilder.Entity("Online_Discussion_Forum.Models.Questions", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("update_date")
                        .HasColumnType("datetime2");

                    b.Property<long?>("user_id")
                        .HasColumnType("bigint");

                    b.Property<string>("username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("views")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Questions_");
                });

            modelBuilder.Entity("Online_Discussion_Forum.Models.Tags", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Tag_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("question_id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Tag_");
                });

            modelBuilder.Entity("Online_Discussion_Forum.Models.Upvote", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("answerid")
                        .HasColumnType("bigint");

                    b.Property<long>("questionid")
                        .HasColumnType("bigint");

                    b.Property<long>("userid")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Upvote_");
                });

            modelBuilder.Entity("Online_Discussion_Forum.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Userid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("about")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("image_url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("passwordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("passwordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("User_");
                });
#pragma warning restore 612, 618
        }
    }
}
