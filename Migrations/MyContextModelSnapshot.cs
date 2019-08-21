﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using the_wall.Models;

namespace the_wall.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("the_wall.Models.Comment", b =>
                {
                    b.Property<int>("Commentid")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Comment_Message")
                        .IsRequired()
                        .HasColumnName("comment");

                    b.Property<DateTime>("Created_At")
                        .HasColumnName("created_at");

                    b.Property<int>("Messageid");

                    b.Property<DateTime>("Updated_At")
                        .HasColumnName("updated_at");

                    b.Property<int>("Userid");

                    b.HasKey("Commentid");

                    b.HasIndex("Messageid");

                    b.HasIndex("Userid");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("the_wall.Models.Message", b =>
                {
                    b.Property<int>("Messageid")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnName("content");

                    b.Property<DateTime>("Created_At")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("Updated_At")
                        .HasColumnName("updated_at");

                    b.Property<int>("Userid");

                    b.HasKey("Messageid");

                    b.HasIndex("Userid");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("the_wall.Models.User", b =>
                {
                    b.Property<int>("Userid")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime>("Created_At")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasColumnType("VARCHAR(255)");

                    b.Property<string>("First_Name")
                        .IsRequired()
                        .HasColumnName("first_name")
                        .HasColumnType("VARCHAR(255)");

                    b.Property<string>("Last_Name")
                        .IsRequired()
                        .HasColumnName("last_name")
                        .HasColumnType("VARCHAR(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password")
                        .HasColumnType("VARCHAR(255)");

                    b.Property<DateTime>("Updated_At")
                        .HasColumnName("updated_at");

                    b.HasKey("Userid");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("the_wall.Models.Comment", b =>
                {
                    b.HasOne("the_wall.Models.Message", "MessageCommented")
                        .WithMany("MessagesComments")
                        .HasForeignKey("Messageid")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("the_wall.Models.User", "Commentor")
                        .WithMany("CommentsMade")
                        .HasForeignKey("Userid")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("the_wall.Models.Message", b =>
                {
                    b.HasOne("the_wall.Models.User", "Creator")
                        .WithMany("MessagesMade")
                        .HasForeignKey("Userid")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
