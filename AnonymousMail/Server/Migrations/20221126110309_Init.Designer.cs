// <auto-generated />
using System;
using AnonymousMail.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AnonymousMail.Server.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221126110309_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AnonymousMail.Shared.Models.MailMessage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Body")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FromUserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ToUserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FromUserId");

                    b.HasIndex("ToUserId");

                    b.ToTable("MailMessages");
                });

            modelBuilder.Entity("AnonymousMail.Shared.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AnonymousMail.Shared.Models.MailMessage", b =>
                {
                    b.HasOne("AnonymousMail.Shared.Models.User", "FromUser")
                        .WithMany("MailMessagesFromUsers")
                        .HasForeignKey("FromUserId")
                        .IsRequired();

                    b.HasOne("AnonymousMail.Shared.Models.User", "ToUser")
                        .WithMany("MailMessagesToUsers")
                        .HasForeignKey("ToUserId")
                        .IsRequired();

                    b.Navigation("FromUser");

                    b.Navigation("ToUser");
                });

            modelBuilder.Entity("AnonymousMail.Shared.Models.User", b =>
                {
                    b.Navigation("MailMessagesFromUsers");

                    b.Navigation("MailMessagesToUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
