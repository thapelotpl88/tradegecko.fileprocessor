﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using tradegecko.fileprocessor.Domain.Entities;

namespace tradegecko.fileprocessor.Migrations
{
    [DbContext(typeof(TradegeckoDbContext))]
    [Migration("20190829095650_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("tradegecko.fileprocessor.Domain.Entities.ObjectTransaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("transaction_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ObjectChanges")
                        .IsRequired()
                        .HasColumnName("object_changes");

                    b.Property<int>("ObjectId")
                        .HasColumnName("object_id");

                    b.Property<string>("ObjectType")
                        .IsRequired()
                        .HasColumnName("object_type")
                        .HasMaxLength(50);

                    b.Property<DateTime>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("timestamp");

                    b.HasKey("TransactionId");

                    b.ToTable("object_transaction");
                });
#pragma warning restore 612, 618
        }
    }
}
