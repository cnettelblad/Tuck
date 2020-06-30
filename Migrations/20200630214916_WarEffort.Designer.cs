﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tuck;

namespace Tuck.Migrations
{
    [DbContext(typeof(TuckContext))]
    [Migration("20200630214916_WarEffort")]
    partial class WarEffort
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5");

            modelBuilder.Entity("Tuck.Model.BuffInstance", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("GuildId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("JobId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Time")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Buffs");
                });

            modelBuilder.Entity("Tuck.Model.Subscription", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("ChannelId")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("GuildId")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("TargetGuildId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("Tuck.Model.WarEffortContribution", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<uint>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("GuildId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemType")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Contributions");
                });
#pragma warning restore 612, 618
        }
    }
}
