﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoscowWeatherAPI;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MoscowWeatherAPI.Migrations
{
    [DbContext(typeof(DbMainContext))]
    [Migration("20220527162608_DataFormatChanges")]
    partial class DataFormatChanges
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MoscowWeatherAPI.Models.WeatherData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AtmospherePressure")
                        .HasColumnType("integer");

                    b.Property<int>("CloudBase")
                        .HasColumnType("integer");

                    b.Property<int>("Cloudiness")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("DewPoint")
                        .HasColumnType("double precision");

                    b.Property<string>("HorizontalVisibility")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("RelativeHumidity")
                        .HasColumnType("double precision");

                    b.Property<double>("Temperature")
                        .HasColumnType("double precision");

                    b.Property<string>("WeatherConditions")
                        .HasColumnType("text");

                    b.Property<string>("WindDirection")
                        .HasColumnType("text");

                    b.Property<int>("WindSpeed")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("WeatherData");
                });
#pragma warning restore 612, 618
        }
    }
}