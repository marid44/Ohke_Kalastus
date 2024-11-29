﻿// <auto-generated />
using System;
using KalastusWebsite.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KalastusWebsite.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("KalastusWebsite.Models.BioHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BioText")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserProfileId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserProfileId");

                    b.ToTable("BioHistories");
                });

            modelBuilder.Entity("KalastusWebsite.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ConversationId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ConversationId");

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

            modelBuilder.Entity("KalastusWebsite.Models.Fish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DescriptionEN")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DescriptionFI")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("EnglishName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FinnishName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Habitat")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("HabitatFI")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Fishes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DescriptionEN = "Perch lives in clean freshwater lakes.",
                            DescriptionFI = "Ahven elää järvissä, joissa on puhdasta vettä.",
                            EnglishName = "Perch",
                            FinnishName = "Ahven",
                            Habitat = "Freshwater",
                            HabitatFI = "Makea vesi",
                            ImageUrl = "/images/ahven.jpg"
                        },
                        new
                        {
                            Id = 2,
                            DescriptionEN = "Pike is a well-known predatory fish in Finland.",
                            DescriptionFI = "Hauki on tunnettu petokala Suomessa.",
                            EnglishName = "Pike",
                            FinnishName = "Hauki",
                            Habitat = "Freshwater",
                            HabitatFI = "Makea vesi",
                            ImageUrl = "/images/hauki.jpg"
                        },
                        new
                        {
                            Id = 3,
                            DescriptionEN = "Whitefish lives in the Baltic Sea and some lakes.",
                            DescriptionFI = "Siika elää Itämeressä ja joissakin järvissä.",
                            EnglishName = "Whitefish",
                            FinnishName = "Siika",
                            Habitat = "Brackish/Sea",
                            HabitatFI = "Murtovesi / Meri",
                            ImageUrl = "/images/siika.jpg"
                        });
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

            modelBuilder.Entity("KalastusWebsite.Models.MediaComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("MediaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MediaId");

                    b.ToTable("MediaComments");
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

            modelBuilder.Entity("KalastusWebsite.Models.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Bio")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProfileImagePath")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("RegisteredAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("KalastusWebsite.Models.Vote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsUpVote")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MediaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MediaId", "UserId")
                        .IsUnique();

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("KalastusWebsite.Models.BioHistory", b =>
                {
                    b.HasOne("KalastusWebsite.Models.UserProfile", null)
                        .WithMany("BioHistory")
                        .HasForeignKey("UserProfileId");
                });

            modelBuilder.Entity("KalastusWebsite.Models.Comment", b =>
                {
                    b.HasOne("KalastusWebsite.Models.Conversation", "Conversation")
                        .WithMany("Comments")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conversation");
                });

            modelBuilder.Entity("KalastusWebsite.Models.MediaComment", b =>
                {
                    b.HasOne("KalastusWebsite.Models.Media", "Media")
                        .WithMany("Comments")
                        .HasForeignKey("MediaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Media");
                });

            modelBuilder.Entity("KalastusWebsite.Models.UserProfile", b =>
                {
                    b.HasOne("KalastusWebsite.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("KalastusWebsite.Models.Vote", b =>
                {
                    b.HasOne("KalastusWebsite.Models.Media", "Media")
                        .WithMany("Votes")
                        .HasForeignKey("MediaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Media");
                });

            modelBuilder.Entity("KalastusWebsite.Models.Conversation", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("KalastusWebsite.Models.Media", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Votes");
                });

            modelBuilder.Entity("KalastusWebsite.Models.UserProfile", b =>
                {
                    b.Navigation("BioHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
