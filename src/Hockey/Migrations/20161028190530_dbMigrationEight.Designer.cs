using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Hockey.Data;

namespace Hockey.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161028190530_dbMigrationEight")]
    partial class dbMigrationEight
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Hockey.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Hockey.Models.CardManufacture", b =>
                {
                    b.Property<int>("CardManufactureId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("MakerDate");

                    b.Property<string>("MakerName");

                    b.HasKey("CardManufactureId");

                    b.ToTable("CardManufacture");
                });

            modelBuilder.Entity("Hockey.Models.Conference", b =>
                {
                    b.Property<int>("ConferenceId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConferenceName");

                    b.HasKey("ConferenceId");

                    b.ToTable("Conference");
                });

            modelBuilder.Entity("Hockey.Models.Division", b =>
                {
                    b.Property<int>("DivisionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DivisionName");

                    b.HasKey("DivisionId");

                    b.ToTable("Division");
                });

            modelBuilder.Entity("Hockey.Models.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImageName");

                    b.Property<string>("ImagePath");

                    b.Property<int>("PlayerId");

                    b.HasKey("ImageId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("Hockey.Models.League", b =>
                {
                    b.Property<int>("LeagueId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LeagueName");

                    b.HasKey("LeagueId");

                    b.ToTable("League");
                });

            modelBuilder.Entity("Hockey.Models.Nationality", b =>
                {
                    b.Property<int>("NationalityId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("NationalityImageName");

                    b.Property<string>("NationalityImagePath");

                    b.Property<string>("NationalityIso");

                    b.Property<string>("NationalityName");

                    b.HasKey("NationalityId");

                    b.ToTable("Nationality");
                });

            modelBuilder.Entity("Hockey.Models.NhlPlayer", b =>
                {
                    b.Property<int>("NhlPlayerId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CardManufactureId");

                    b.Property<int>("ConferenceId");

                    b.Property<int>("DivisionId");

                    b.Property<bool>("ISActive");

                    b.Property<bool>("ISSigned");

                    b.Property<int>("LeagueId");

                    b.Property<int>("NationalityId");

                    b.Property<string>("NhlPlayerCardId");

                    b.Property<DateTime>("PlayerAddDate");

                    b.Property<string>("PlayerFirstName");

                    b.Property<string>("PlayerImage");

                    b.Property<int>("PlayerJersyNumber");

                    b.Property<string>("PlayerLastName");

                    b.Property<int>("PositionId");

                    b.Property<int>("SeasonId");

                    b.Property<int>("TeamId");

                    b.Property<int>("TeamImageId");

                    b.Property<decimal>("Value");

                    b.HasKey("NhlPlayerId");

                    b.HasIndex("CardManufactureId");

                    b.HasIndex("ConferenceId");

                    b.HasIndex("DivisionId");

                    b.HasIndex("LeagueId");

                    b.HasIndex("NationalityId");

                    b.HasIndex("PositionId");

                    b.HasIndex("SeasonId");

                    b.HasIndex("TeamId");

                    b.HasIndex("TeamImageId");

                    b.ToTable("NhlPlayer");
                });

            modelBuilder.Entity("Hockey.Models.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CardManufactureId");

                    b.Property<int>("ConferenceId");

                    b.Property<int>("DivisionId");

                    b.Property<bool>("ISActive");

                    b.Property<bool>("ISSigned");

                    b.Property<int>("ImageId");

                    b.Property<int>("LeagueId");

                    b.Property<int>("NationalityId");

                    b.Property<DateTime>("PlayerAddDate");

                    b.Property<string>("PlayerCardId");

                    b.Property<string>("PlayerFirstName");

                    b.Property<string>("PlayerImage");

                    b.Property<int>("PlayerJersyNumber");

                    b.Property<string>("PlayerLastName");

                    b.Property<int>("PositionId");

                    b.Property<int>("SeasonId");

                    b.Property<int>("TeamId");

                    b.Property<int>("TeamImageId");

                    b.Property<decimal>("Value");

                    b.Property<int?>("_ImageImageId");

                    b.HasKey("PlayerId");

                    b.HasIndex("CardManufactureId");

                    b.HasIndex("ConferenceId");

                    b.HasIndex("DivisionId");

                    b.HasIndex("LeagueId");

                    b.HasIndex("NationalityId");

                    b.HasIndex("PositionId");

                    b.HasIndex("SeasonId");

                    b.HasIndex("TeamId");

                    b.HasIndex("TeamImageId");

                    b.HasIndex("_ImageImageId");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("Hockey.Models.Position", b =>
                {
                    b.Property<int>("PositionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PositionType");

                    b.HasKey("PositionId");

                    b.ToTable("Position");
                });

            modelBuilder.Entity("Hockey.Models.Season", b =>
                {
                    b.Property<int>("SeasonId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("SeasonName");

                    b.HasKey("SeasonId");

                    b.ToTable("Season");
                });

            modelBuilder.Entity("Hockey.Models.ShlPlayer", b =>
                {
                    b.Property<int>("ShlPlayerId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CardManufactureId");

                    b.Property<bool>("ISActive");

                    b.Property<bool>("ISSigned");

                    b.Property<int>("ImageId");

                    b.Property<int>("LeagueId");

                    b.Property<int>("NationalityId");

                    b.Property<DateTime>("PlayerAddDate");

                    b.Property<string>("PlayerCardId");

                    b.Property<string>("PlayerFirstName");

                    b.Property<string>("PlayerImage");

                    b.Property<int>("PlayerJersyNumber");

                    b.Property<string>("PlayerLastName");

                    b.Property<int>("PositionId");

                    b.Property<int>("SeasonId");

                    b.Property<int>("TeamId");

                    b.Property<int>("TeamImageId");

                    b.Property<decimal>("Value");

                    b.HasKey("ShlPlayerId");

                    b.HasIndex("CardManufactureId");

                    b.HasIndex("ImageId");

                    b.HasIndex("LeagueId");

                    b.HasIndex("NationalityId");

                    b.HasIndex("PositionId");

                    b.HasIndex("SeasonId");

                    b.HasIndex("TeamId");

                    b.HasIndex("TeamImageId");

                    b.ToTable("ShlPlayer");
                });

            modelBuilder.Entity("Hockey.Models.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("LeagueId");

                    b.Property<string>("TeamName");

                    b.HasKey("TeamId");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("Hockey.Models.TeamImage", b =>
                {
                    b.Property<int>("TeamImageId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TeamImageName");

                    b.Property<string>("TeamImagePath");

                    b.HasKey("TeamImageId");

                    b.ToTable("TeamImage");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Hockey.Models.NhlPlayer", b =>
                {
                    b.HasOne("Hockey.Models.CardManufacture", "_CardManufacture")
                        .WithMany()
                        .HasForeignKey("CardManufactureId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hockey.Models.Conference", "_Conference")
                        .WithMany()
                        .HasForeignKey("ConferenceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hockey.Models.Division", "_Division")
                        .WithMany()
                        .HasForeignKey("DivisionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hockey.Models.League", "_Leauge")
                        .WithMany()
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hockey.Models.Nationality", "_Nationality")
                        .WithMany()
                        .HasForeignKey("NationalityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hockey.Models.Position", "_Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hockey.Models.Season", "_Season")
                        .WithMany()
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hockey.Models.Team", "_Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hockey.Models.TeamImage", "_TeamImage")
                        .WithMany()
                        .HasForeignKey("TeamImageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hockey.Models.Player", b =>
                {
                    b.HasOne("Hockey.Models.CardManufacture", "_CardManufacture")
                        .WithMany()
                        .HasForeignKey("CardManufactureId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hockey.Models.Conference", "_Conference")
                        .WithMany()
                        .HasForeignKey("ConferenceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hockey.Models.Division", "_Division")
                        .WithMany()
                        .HasForeignKey("DivisionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hockey.Models.League", "_Leauge")
                        .WithMany()
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hockey.Models.Nationality", "_Nationality")
                        .WithMany()
                        .HasForeignKey("NationalityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hockey.Models.Position", "_Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hockey.Models.Season", "_Season")
                        .WithMany()
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hockey.Models.Team", "_Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hockey.Models.TeamImage", "_TeamImage")
                        .WithMany()
                        .HasForeignKey("TeamImageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hockey.Models.Image", "_Image")
                        .WithMany()
                        .HasForeignKey("_ImageImageId");
                });

            modelBuilder.Entity("Hockey.Models.ShlPlayer", b =>
                {
                    b.HasOne("Hockey.Models.CardManufacture", "_CardManufacture")
                        .WithMany()
                        .HasForeignKey("CardManufactureId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hockey.Models.Image", "_Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hockey.Models.League", "_Leauge")
                        .WithMany()
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hockey.Models.Nationality", "_Nationality")
                        .WithMany()
                        .HasForeignKey("NationalityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hockey.Models.Position", "_Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hockey.Models.Season", "_Season")
                        .WithMany()
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hockey.Models.Team", "_Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hockey.Models.TeamImage", "_TeamImage")
                        .WithMany()
                        .HasForeignKey("TeamImageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Hockey.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Hockey.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hockey.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
