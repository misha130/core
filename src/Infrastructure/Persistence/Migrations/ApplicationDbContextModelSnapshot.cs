﻿// <auto-generated />
using System;
using Codidact.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Codidact.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Codidact.Domain.Entities.Community", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("character varying(40)")
                        .HasMaxLength(40);

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("status")
                        .HasColumnType("integer")
                        .HasDefaultValue(1);

                    b.Property<string>("Tagline")
                        .IsRequired()
                        .HasColumnName("tagline")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnName("url")
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id")
                        .HasName("pk_communities");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("ix_communities_name");

                    b.HasIndex("Tagline")
                        .IsUnique()
                        .HasName("ix_communities_tagline");

                    b.HasIndex("Url")
                        .IsUnique()
                        .HasName("ix_communities_url");

                    b.ToTable("communities");
                });

            modelBuilder.Entity("Codidact.Domain.Entities.Member", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Bio")
                        .HasColumnName("bio")
                        .HasColumnType("text");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnName("display_name")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("IsEmailVerified")
                        .HasColumnName("is_email_verified")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsFromStackExchange")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("is_from_stack_exchange")
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsSuspended")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("is_suspended")
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Location")
                        .HasColumnName("location")
                        .HasColumnType("text");

                    b.Property<long?>("StackExchangeId")
                        .HasColumnName("stack_exchange_id")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("StackExchangeLastImported")
                        .HasColumnName("stack_exchange_last_imported")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("StackExchangeValidated")
                        .HasColumnName("stack_exchange_validated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("SuspensionEndDate")
                        .HasColumnName("suspension_end_date")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id")
                        .HasName("pk_members");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasName("ix_members_email");

                    b.ToTable("members");
                });

            modelBuilder.Entity("Codidact.Domain.Entities.TrustLevel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnName("content")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Explanation")
                        .HasColumnName("explanation")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_trust_levels");

                    b.HasIndex("Content")
                        .IsUnique()
                        .HasName("ix_trust_levels_content");

                    b.HasIndex("Explanation")
                        .IsUnique()
                        .HasName("ix_trust_levels_explanation");

                    b.ToTable("trust_levels");
                });
#pragma warning restore 612, 618
        }
    }
}
