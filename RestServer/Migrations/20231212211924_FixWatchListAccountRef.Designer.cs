﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RestServer.Data;

#nullable disable

namespace RestServer.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20231212211924_FixWatchListAccountRef")]
    partial class FixWatchListAccountRef
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Shared.Account", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Shared.Comment", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long?>("Id"));

                    b.Property<int>("Account")
                        .HasColumnType("integer");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("Movie")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("date_posted")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Account");

                    b.HasIndex("Movie");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Shared.Directors", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long?>("Id"));

                    b.Property<long>("Movie")
                        .HasColumnType("bigint");

                    b.Property<long>("People")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Movie");

                    b.HasIndex("People");

                    b.ToTable("Directors");
                });

            modelBuilder.Entity("Shared.Movie", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Shared.People", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal?>("BirthYear")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Peoples");
                });

            modelBuilder.Entity("Shared.Ratings", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long?>("Id"));

                    b.Property<long>("Movie")
                        .HasColumnType("bigint");

                    b.Property<float>("RatingValue")
                        .HasColumnType("real");

                    b.Property<long>("Votes")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Movie");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("Shared.Stars", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long?>("Id"));

                    b.Property<long>("Movie")
                        .HasColumnType("bigint");

                    b.Property<long>("People")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Movie");

                    b.HasIndex("People");

                    b.ToTable("Stars");
                });

            modelBuilder.Entity("Shared.WatchList", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long?>("Id"));

                    b.Property<int>("Account")
                        .HasColumnType("integer");

                    b.Property<long>("Movie")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Account");

                    b.HasIndex("Movie");

                    b.ToTable("WatchLists");
                });

            modelBuilder.Entity("Shared.Comment", b =>
                {
                    b.HasOne("Shared.Account", "WrittenBy")
                        .WithMany()
                        .HasForeignKey("Account")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.Movie", "movie_id")
                        .WithMany()
                        .HasForeignKey("Movie")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WrittenBy");

                    b.Navigation("movie_id");
                });

            modelBuilder.Entity("Shared.Directors", b =>
                {
                    b.HasOne("Shared.Movie", "movie_id")
                        .WithMany()
                        .HasForeignKey("Movie")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.People", "person_id")
                        .WithMany()
                        .HasForeignKey("People")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("movie_id");

                    b.Navigation("person_id");
                });

            modelBuilder.Entity("Shared.Ratings", b =>
                {
                    b.HasOne("Shared.Movie", "movie_id")
                        .WithMany()
                        .HasForeignKey("Movie")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("movie_id");
                });

            modelBuilder.Entity("Shared.Stars", b =>
                {
                    b.HasOne("Shared.Movie", "movie_id")
                        .WithMany()
                        .HasForeignKey("Movie")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.People", "person_id")
                        .WithMany()
                        .HasForeignKey("People")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("movie_id");

                    b.Navigation("person_id");
                });

            modelBuilder.Entity("Shared.WatchList", b =>
                {
                    b.HasOne("Shared.Account", "account_id")
                        .WithMany()
                        .HasForeignKey("Account")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.Movie", "movie_id")
                        .WithMany()
                        .HasForeignKey("Movie")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("account_id");

                    b.Navigation("movie_id");
                });
#pragma warning restore 612, 618
        }
    }
}
