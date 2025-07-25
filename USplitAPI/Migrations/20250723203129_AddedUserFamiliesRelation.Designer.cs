﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using USplitAPI.Data;

#nullable disable

namespace USplitAPI.Migrations
{
    [DbContext(typeof(USplitDBContext))]
    [Migration("20250723203129_AddedUserFamiliesRelation")]
    partial class AddedUserFamiliesRelation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("USplitAPI.Domain.DebtEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("LenderUserId")
                        .HasColumnType("integer");

                    b.Property<int>("OwnerFamilyId")
                        .HasColumnType("integer");

                    b.Property<int>("OwnerUserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LenderUserId");

                    b.HasIndex("OwnerUserId", "OwnerFamilyId");

                    b.ToTable("Debts");
                });

            modelBuilder.Entity("USplitAPI.Domain.FamilyEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OwnerUserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OwnerUserId");

                    b.ToTable("Families");
                });

            modelBuilder.Entity("USplitAPI.Domain.RefreshTokenEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("USplitAPI.Domain.TransactionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<int>("FamilyId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FamilyId");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("USplitAPI.Domain.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateJoined")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("USplitAPI.Domain.UserFamilyJoinedEntity", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("FamilyId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "FamilyId");

                    b.HasIndex("FamilyId");

                    b.ToTable("UserFamilies");
                });

            modelBuilder.Entity("USplitAPI.Domain.DebtEntity", b =>
                {
                    b.HasOne("USplitAPI.Domain.UserEntity", "LenderUser")
                        .WithMany()
                        .HasForeignKey("LenderUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("USplitAPI.Domain.UserFamilyJoinedEntity", "UserFamily")
                        .WithMany("Debts")
                        .HasForeignKey("OwnerUserId", "OwnerFamilyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LenderUser");

                    b.Navigation("UserFamily");
                });

            modelBuilder.Entity("USplitAPI.Domain.FamilyEntity", b =>
                {
                    b.HasOne("USplitAPI.Domain.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("OwnerUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("USplitAPI.Domain.RefreshTokenEntity", b =>
                {
                    b.HasOne("USplitAPI.Domain.UserEntity", "User")
                        .WithOne()
                        .HasForeignKey("USplitAPI.Domain.RefreshTokenEntity", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("USplitAPI.Domain.TransactionEntity", b =>
                {
                    b.HasOne("USplitAPI.Domain.FamilyEntity", "Family")
                        .WithMany("TransactionList")
                        .HasForeignKey("FamilyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("USplitAPI.Domain.UserEntity", "User")
                        .WithMany("TransactionList")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Family");

                    b.Navigation("User");
                });

            modelBuilder.Entity("USplitAPI.Domain.UserFamilyJoinedEntity", b =>
                {
                    b.HasOne("USplitAPI.Domain.FamilyEntity", "Family")
                        .WithMany("UserFamilyList")
                        .HasForeignKey("FamilyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("USplitAPI.Domain.UserEntity", "User")
                        .WithMany("UserFamilyList")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Family");

                    b.Navigation("User");
                });

            modelBuilder.Entity("USplitAPI.Domain.FamilyEntity", b =>
                {
                    b.Navigation("TransactionList");

                    b.Navigation("UserFamilyList");
                });

            modelBuilder.Entity("USplitAPI.Domain.UserEntity", b =>
                {
                    b.Navigation("TransactionList");

                    b.Navigation("UserFamilyList");
                });

            modelBuilder.Entity("USplitAPI.Domain.UserFamilyJoinedEntity", b =>
                {
                    b.Navigation("Debts");
                });
#pragma warning restore 612, 618
        }
    }
}
