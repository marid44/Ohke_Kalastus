﻿// <auto-generated />
using System;
using KalastusWebsite.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KalastusWebsite.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241113083955_AddAuthorColumnToComments")]
    partial class AddAuthorColumnToComments
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("KalastusWebsite.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ConversationId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("MediaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ConversationId");

                    b.HasIndex("MediaId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("KalastusWebsite.Models.Conversation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Conversations");
                });

            modelBuilder.Entity("KalastusWebsite.Models.Media", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UploadedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("UploadedBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MediaFiles");
                });

            modelBuilder.Entity("KalastusWebsite.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("KalastusWebsite.Models.Comment", b =>
                {
                    b.HasOne("KalastusWebsite.Models.Conversation", "Conversation")
                        .WithMany("Comments")
                        .HasForeignKey("ConversationId");

                    b.HasOne("KalastusWebsite.Models.Media", "Media")
                        .WithMany("Comments")
                        .HasForeignKey("MediaId");

                    b.Navigation("Conversation");

                    b.Navigation("Media");
                });

            modelBuilder.Entity("KalastusWebsite.Models.Conversation", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("KalastusWebsite.Models.Media", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
