using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace CM.Models
{
    public partial class netcmsContext : DbContext
    {
        public netcmsContext()
        {
        }

        public netcmsContext(DbContextOptions<netcmsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CmArticlecategories> CmArticlecategories { get; set; }
        public virtual DbSet<CmArticles> CmArticles { get; set; }
        public virtual DbSet<CmManagers> CmManagers { get; set; }
        public virtual DbSet<CmMembers> CmMembers { get; set; }
        public virtual DbSet<CmRole> CmRole { get; set; }
        public virtual DbSet<CmRolecategories> CmRolecategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //                optionsBuilder.UseMySql("server=127.0.0.1;userid=root;pwd=root;port=3306;database=netcms;sslmode=none;");
                var config = new ConfigurationBuilder()
                             .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                             .AddJsonFile("appsettings.json")
                             .Build();
                optionsBuilder.UseMySql(config.GetConnectionString("MySqlConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CmArticlecategories>(entity =>
            {
                entity.ToTable("cm_articlecategories");

                entity.Property(e => e.Id).HasColumnType("bigint(20)");

                entity.Property(e => e.DisplaySequence).HasColumnType("bigint(20)");

                entity.Property(e => e.IsDefault).HasColumnType("bit(1)");

                entity.Property(e => e.Name).HasColumnType("varchar(100)");

                entity.Property(e => e.ParentCategoryId).HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<CmArticles>(entity =>
            {
                entity.ToTable("cm_articles");

                entity.Property(e => e.Id).HasColumnType("bigint(20)");

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Content).HasColumnType("mediumtext");

                entity.Property(e => e.ContentUrl).HasColumnType("varchar(255)");

                entity.Property(e => e.DisplaySequence).HasColumnType("bigint(20)");

                entity.Property(e => e.IconUrl).HasColumnType("varchar(100)");

                entity.Property(e => e.IsRelease).HasColumnType("tinyint(1)");

                entity.Property(e => e.MetaDescription)
                    .HasColumnName("Meta_Description")
                    .HasColumnType("text");

                entity.Property(e => e.MetaKeywords)
                    .HasColumnName("Meta_Keywords")
                    .HasColumnType("text");

                entity.Property(e => e.MetaTitle)
                    .HasColumnName("Meta_Title")
                    .HasColumnType("text");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<CmManagers>(entity =>
            {
                entity.ToTable("cm_managers");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Mobile).HasColumnType("varchar(50)");

                entity.Property(e => e.OpenId).HasColumnType("varchar(50)");

                entity.Property(e => e.PassWord).HasColumnType("varchar(255)");

                entity.Property(e => e.RealName).HasColumnType("varchar(20)");

                entity.Property(e => e.Remark).HasColumnType("varchar(255)");

                entity.Property(e => e.RoleId).HasColumnType("int(11)");

                entity.Property(e => e.UserName).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<CmMembers>(entity =>
            {
                entity.ToTable("cm_members");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Address).HasColumnType("varchar(200)");

                entity.Property(e => e.CellPhone).HasColumnType("varchar(100)");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DetailedAddress).HasColumnType("varchar(255)");

                entity.Property(e => e.Disabled)
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.IdCard).HasColumnType("varchar(50)");

                entity.Property(e => e.Integral)
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.LastLonginDate).HasColumnType("datetime");

                entity.Property(e => e.Nick).HasColumnType("varchar(100)");

                entity.Property(e => e.OpenId)
                    .IsRequired()
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Password).HasColumnType("varchar(100)");

                entity.Property(e => e.PasswordSalt).HasColumnType("varchar(100)");

                entity.Property(e => e.Photo).HasColumnType("varchar(1000)");

                entity.Property(e => e.RealName).HasColumnType("varchar(100)");

                entity.Property(e => e.SexType).HasColumnType("int(11)");

                entity.Property(e => e.State)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.UnionId).HasColumnType("varchar(100)");

                entity.Property(e => e.UnionOpenId).HasColumnType("varchar(200)");

                entity.Property(e => e.UserIntegral)
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.UserName).HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<CmRole>(entity =>
            {
                entity.ToTable("cm_role");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Rank)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<CmRolecategories>(entity =>
            {
                entity.ToTable("cm_rolecategories");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.CategorieId).HasColumnType("bigint(20)");

                entity.Property(e => e.RoleId).HasColumnType("bigint(20)");
            });
        }
    }
}
