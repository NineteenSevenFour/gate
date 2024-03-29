﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NineteenSevenFour.Gatehub.Data.Sqlite.Context;

#nullable disable

namespace NineteenSevenFour.Gatehub.Data.Sqlite.Migrations
{
    [DbContext(typeof(SqliteDbContext))]
    [Migration("20230523203731_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("NineteenSevenFour.Gatehub.Domain.Entities.GateApplicationMetadataEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("description");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("icon");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "applicationmetadata_index");

                    b.ToTable("applicationmetadata", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
