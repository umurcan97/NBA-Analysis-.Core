﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NBA.Models.DataContext;

namespace NBA.Models.Migrations
{
    [DbContext(typeof(NBAContext))]
    [Migration("20210903010108_v0")]
    partial class v0
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NBA.Models.Entities.FullSeason", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Away3PA")
                        .HasColumnType("float");

                    b.Property<double>("Away3PM")
                        .HasColumnType("float");

                    b.Property<double>("AwayAssists")
                        .HasColumnType("float");

                    b.Property<double>("AwayBenchPoints")
                        .HasColumnType("float");

                    b.Property<double>("AwayBlocks")
                        .HasColumnType("float");

                    b.Property<double>("AwayDefensiveRebounds")
                        .HasColumnType("float");

                    b.Property<double>("AwayFGA")
                        .HasColumnType("float");

                    b.Property<double>("AwayFGM")
                        .HasColumnType("float");

                    b.Property<double>("AwayFTA")
                        .HasColumnType("float");

                    b.Property<double>("AwayFTM")
                        .HasColumnType("float");

                    b.Property<double>("AwayFastBreakPoints")
                        .HasColumnType("float");

                    b.Property<double>("AwayOffensiveRebounds")
                        .HasColumnType("float");

                    b.Property<double>("AwayPITP")
                        .HasColumnType("float");

                    b.Property<double>("AwayPoints")
                        .HasColumnType("float");

                    b.Property<double>("AwayPointsofTO")
                        .HasColumnType("float");

                    b.Property<double>("AwaySteals")
                        .HasColumnType("float");

                    b.Property<int>("AwayTeam")
                        .HasColumnType("int");

                    b.Property<double>("AwayTotalRebounds")
                        .HasColumnType("float");

                    b.Property<double>("AwayTurnovers")
                        .HasColumnType("float");

                    b.Property<DateTime>("GameDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("GameNo")
                        .HasColumnType("int");

                    b.Property<double>("Home3PA")
                        .HasColumnType("float");

                    b.Property<double>("Home3PM")
                        .HasColumnType("float");

                    b.Property<double>("HomeAssists")
                        .HasColumnType("float");

                    b.Property<double>("HomeBenchPoints")
                        .HasColumnType("float");

                    b.Property<double>("HomeBlocks")
                        .HasColumnType("float");

                    b.Property<double>("HomeDefensiveRebounds")
                        .HasColumnType("float");

                    b.Property<double>("HomeFGA")
                        .HasColumnType("float");

                    b.Property<double>("HomeFGM")
                        .HasColumnType("float");

                    b.Property<double>("HomeFTA")
                        .HasColumnType("float");

                    b.Property<double>("HomeFTM")
                        .HasColumnType("float");

                    b.Property<double>("HomeFastBreakPoints")
                        .HasColumnType("float");

                    b.Property<double>("HomeOffensiveRebounds")
                        .HasColumnType("float");

                    b.Property<double>("HomePITP")
                        .HasColumnType("float");

                    b.Property<double>("HomePoints")
                        .HasColumnType("float");

                    b.Property<double>("HomePointsofTO")
                        .HasColumnType("float");

                    b.Property<double>("HomeSteals")
                        .HasColumnType("float");

                    b.Property<int>("HomeTeam")
                        .HasColumnType("int");

                    b.Property<double>("HomeTotalRebounds")
                        .HasColumnType("float");

                    b.Property<double>("HomeTurnovers")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("FullSeason");
                });

            modelBuilder.Entity("NBA.Models.Entities.GameTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AwayTeam")
                        .HasColumnType("int");

                    b.Property<DateTime>("GameDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("GameNo")
                        .HasColumnType("int");

                    b.Property<int>("HomeTeam")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("GameTime");
                });

            modelBuilder.Entity("NBA.Models.Players", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Team")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("NBA.Models.Teams", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Conference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeamName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}
