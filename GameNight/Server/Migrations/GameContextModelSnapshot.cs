﻿// <auto-generated />
using System;
using GameNight.Server.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GameNight.Server.Migrations
{
    [DbContext(typeof(GameContext))]
    partial class GameContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GameNight.Shared.PlayedGame", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("DurationMinutes")
                        .HasColumnType("integer")
                        .HasColumnName("duration_minutes");

                    b.Property<string>("GameName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("game_name");

                    b.Property<DateTime>("StartedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("started_at_utc");

                    b.HasKey("Id")
                        .HasName("pk_played_game");

                    b.ToTable("played_game", (string)null);
                });

            modelBuilder.Entity("GameNight.Shared.PlayedGamePlayer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsWinner")
                        .HasColumnType("boolean")
                        .HasColumnName("is_winner");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<Guid?>("PlayedGameId")
                        .HasColumnType("uuid")
                        .HasColumnName("played_game_id");

                    b.HasKey("Id")
                        .HasName("pk_played_game_player");

                    b.HasIndex("PlayedGameId")
                        .HasDatabaseName("ix_played_game_player_played_game_id");

                    b.ToTable("played_game_player", (string)null);
                });

            modelBuilder.Entity("GameNight.Shared.PlayedGamePlayer", b =>
                {
                    b.HasOne("GameNight.Shared.PlayedGame", null)
                        .WithMany("Players")
                        .HasForeignKey("PlayedGameId")
                        .HasConstraintName("fk_played_game_player_played_game_played_game_id");
                });

            modelBuilder.Entity("GameNight.Shared.PlayedGame", b =>
                {
                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
