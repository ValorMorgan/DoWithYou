﻿// <auto-generated />
using DoWithYou.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace DoWithYou.Data.Migrations
{
    [DbContext(typeof(DoWithYouContext))]
    [Migration("20180117211206_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DoWithYou.Data.Entities.DoWithYou.User", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("DoWithYou.Data.Entities.DoWithYou.UserProfile", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address1")
                        .IsRequired();

                    b.Property<string>("Address2");

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("MiddleName");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Phone");

                    b.Property<string>("State")
                        .IsRequired();

                    b.Property<string>("ZipCode")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.ToTable("UserProfile");
                });

            modelBuilder.Entity("DoWithYou.Data.Entities.DoWithYou.UserProfile", b =>
                {
                    b.HasOne("DoWithYou.Data.Entities.DoWithYou.User", "User")
                        .WithOne("UserProfile")
                        .HasForeignKey("DoWithYou.Data.Entities.DoWithYou.User", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
