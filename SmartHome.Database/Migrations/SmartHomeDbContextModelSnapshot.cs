﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartHome.Database;

namespace SmartHome.Database.Migrations
{
    [DbContext(typeof(SmartHomeDbContext))]
    partial class SmartHomeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("Relational:Sequence:.EntityFrameworkHiLoSequence", "'EntityFrameworkHiLoSequence', '', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SmartHome.Database.Entities.BatteryMeasurement", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:HiLoSequenceName", "EntityFrameworkHiLoSequence")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<long>("BatteryPowerSourceTypeId");

                    b.Property<DateTime>("MeasurementDateTime");

                    b.Property<long>("UnitId");

                    b.Property<double>("Voltage");

                    b.HasKey("Id");

                    b.HasIndex("BatteryPowerSourceTypeId");

                    b.HasIndex("MeasurementDateTime");

                    b.HasIndex("UnitId");

                    b.ToTable("BatteryMeasurement");
                });

            modelBuilder.Entity("SmartHome.Database.Entities.BatteryPowerSourceType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BatteryType");

                    b.Property<double>("MaximumVoltage");

                    b.Property<double>("MinimumVoltage");

                    b.HasKey("Id");

                    b.ToTable("BatteryPowerSourceType");
                });

            modelBuilder.Entity("SmartHome.Database.Entities.HumidityMeasurement", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:HiLoSequenceName", "EntityFrameworkHiLoSequence")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<double>("Humidity");

                    b.Property<DateTime>("MeasurementDateTime");

                    b.Property<long>("UnitId");

                    b.HasKey("Id");

                    b.HasIndex("MeasurementDateTime");

                    b.HasIndex("UnitId");

                    b.ToTable("HumidityMeasurement");
                });

            modelBuilder.Entity("SmartHome.Database.Entities.TemperatureMeasurement", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:HiLoSequenceName", "EntityFrameworkHiLoSequence")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<DateTime>("MeasurementDateTime");

                    b.Property<double>("Temperature");

                    b.Property<long>("UnitId");

                    b.HasKey("Id");

                    b.HasIndex("MeasurementDateTime");

                    b.HasIndex("UnitId");

                    b.ToTable("TemperatureMeasurement");
                });

            modelBuilder.Entity("SmartHome.Database.Entities.Unit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("BatteryPowerSourceTypeId");

                    b.Property<long>("UnitTypeId");

                    b.HasKey("Id");

                    b.HasIndex("BatteryPowerSourceTypeId");

                    b.HasIndex("UnitTypeId");

                    b.ToTable("Unit");
                });

            modelBuilder.Entity("SmartHome.Database.Entities.UnitType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("UnitType");
                });

            modelBuilder.Entity("SmartHome.Database.Entities.BatteryMeasurement", b =>
                {
                    b.HasOne("SmartHome.Database.Entities.BatteryPowerSourceType", "BatteryPowerSourceType")
                        .WithMany()
                        .HasForeignKey("BatteryPowerSourceTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SmartHome.Database.Entities.Unit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SmartHome.Database.Entities.HumidityMeasurement", b =>
                {
                    b.HasOne("SmartHome.Database.Entities.Unit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SmartHome.Database.Entities.TemperatureMeasurement", b =>
                {
                    b.HasOne("SmartHome.Database.Entities.Unit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SmartHome.Database.Entities.Unit", b =>
                {
                    b.HasOne("SmartHome.Database.Entities.BatteryPowerSourceType", "BatteryPowerSourceType")
                        .WithMany()
                        .HasForeignKey("BatteryPowerSourceTypeId");

                    b.HasOne("SmartHome.Database.Entities.UnitType", "UnitType")
                        .WithMany()
                        .HasForeignKey("UnitTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
