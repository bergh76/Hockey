using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Hockey.Data;

namespace Hockey.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HockeyWebPage.Models.ApplicationUser", b =>
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

            modelBuilder.Entity("HockeyWebPage.Models.CardManufacture", b =>
                {
                    b.Property<int>("CardManufactureId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("MakerDate");

                    b.Property<string>("MakerName");

                    b.HasKey("CardManufactureId");

                    b.ToTable("CardManufacture");
                });

            modelBuilder.Entity("HockeyWebPage.Models.Conference", b =>
                {
                    b.Property<int>("ConferenceId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConferenceName");

                    b.HasKey("ConferenceId");

                    b.ToTable("Conference");
                });

            modelBuilder.Entity("HockeyWebPage.Models.Division", b =>
                {
                    b.Property<int>("DivisionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DivisionName");

                    b.HasKey("DivisionId");

                    b.ToTable("Division");
                });

            modelBuilder.Entity("HockeyWebPage.Models.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImageName");

                    b.Property<string>("ImagePath");

                    b.Property<int>("PlayerId");

                    b.HasKey("ImageId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("HockeyWebPage.Models.Nationality", b =>
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

            modelBuilder.Entity("HockeyWebPage.Models.NhlTeam", b =>
                {
                    b.Property<int>("NhlTeamId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ConferenceId");

                    b.Property<int>("DivisionId");

                    b.Property<string>("NhlTeamName");

                    b.HasKey("NhlTeamId");

                    b.ToTable("NhlTeam");
                });

            modelBuilder.Entity("HockeyWebPage.Models.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CardManufactureId");

                    b.Property<int>("ConferenceId");

                    b.Property<int>("DivisionId");

                    b.Property<bool>("ISActive");

                    b.Property<bool>("ISSigned");

                    b.Property<int>("ImageId");

                    b.Property<int>("NationalityId");

                    b.Property<int>("NhlTeamId");

                    b.Property<DateTime>("PlayerAddDate");

                    b.Property<string>("PlayerCardId");

                    b.Property<string>("PlayerFirstName");

                    b.Property<string>("PlayerImage");

                    b.Property<int>("PlayerJersyNumber");

                    b.Property<string>("PlayerLastName");

                    b.Property<int>("PositionId");

                    b.Property<int>("SeasonId");

                    b.Property<int>("TeamImageId");

                    b.Property<decimal>("Value");

                    b.Property<int?>("_ImageImageId");

                    b.HasKey("PlayerId");

                    b.HasIndex("CardManufactureId");

                    b.HasIndex("ConferenceId");

                    b.HasIndex("DivisionId");

                    b.HasIndex("NationalityId");

                    b.HasIndex("NhlTeamId");

                    b.HasIndex("PositionId");

                    b.HasIndex("SeasonId");

                    b.HasIndex("TeamImageId");

                    b.HasIndex("_ImageImageId");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("HockeyWebPage.Models.Position", b =>
                {
                    b.Property<int>("PositionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PositionType");

                    b.HasKey("PositionId");

                    b.ToTable("Position");
                });

            modelBuilder.Entity("HockeyWebPage.Models.Season", b =>
                {
                    b.Property<int>("SeasonId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("SeasonName");

                    b.HasKey("SeasonId");

                    b.ToTable("Season");
                });

            modelBuilder.Entity("HockeyWebPage.Models.TeamImage", b =>
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

            modelBuilder.Entity("HockeyWebPage.Models.Player", b =>
                {
                    b.HasOne("HockeyWebPage.Models.CardManufacture", "_CardManufacture")
                        .WithMany()
                        .HasForeignKey("CardManufactureId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HockeyWebPage.Models.Conference", "_Conference")
                        .WithMany()
                        .HasForeignKey("ConferenceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HockeyWebPage.Models.Division", "_Division")
                        .WithMany()
                        .HasForeignKey("DivisionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HockeyWebPage.Models.Nationality", "_Nationality")
                        .WithMany()
                        .HasForeignKey("NationalityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HockeyWebPage.Models.NhlTeam", "_Team")
                        .WithMany()
                        .HasForeignKey("NhlTeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HockeyWebPage.Models.Position", "_Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HockeyWebPage.Models.Season", "_Season")
                        .WithMany()
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HockeyWebPage.Models.TeamImage", "_TeamImage")
                        .WithMany()
                        .HasForeignKey("TeamImageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HockeyWebPage.Models.Image", "_Image")
                        .WithMany()
                        .HasForeignKey("_ImageImageId");
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
                    b.HasOne("HockeyWebPage.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HockeyWebPage.Models.ApplicationUser")
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

                    b.HasOne("HockeyWebPage.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
