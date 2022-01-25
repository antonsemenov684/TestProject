using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TestProject.DB.Models
{
	public partial class Context : DbContext
	{
		public Context()
		{
		}

		public Context(DbContextOptions<Context> options)
			: base(options)
		{
		}

		public virtual DbSet<Developer> Developers { get; set; }
		public virtual DbSet<Game> Games { get; set; }
		public virtual DbSet<GameGenreLink> GameGenreLinks { get; set; }
		public virtual DbSet<Genre> Genres { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(ConfigurationManager.AppSettings["connectionString"]);
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

			modelBuilder.Entity<Developer>(entity =>
			{
				entity.ToTable("Developer");

				entity.Property(e => e.Id).ValueGeneratedNever();

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(255);
			});

			modelBuilder.Entity<Game>(entity =>
			{
				entity.ToTable("Game");

				entity.Property(e => e.Id).ValueGeneratedNever();

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(255);

				entity.HasOne(d => d.Developer)
					.WithMany(p => p.Games)
					.HasForeignKey(d => d.DeveloperId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Game_Developer");
			});

			modelBuilder.Entity<GameGenreLink>(entity =>
			{
				entity.ToTable("GameGenreLink");

				entity.Property(e => e.Id).ValueGeneratedNever();

				entity.HasOne(d => d.Game)
					.WithMany(p => p.GameGenreLinks)
					.HasForeignKey(d => d.GameId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_GameGenreLink_Game");

				entity.HasOne(d => d.Genre)
					.WithMany(p => p.GameGenreLinks)
					.HasForeignKey(d => d.GenreId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_GameGenreLink_Genre");
			});

			modelBuilder.Entity<Genre>(entity =>
			{
				entity.ToTable("Genre");

				entity.Property(e => e.Id).ValueGeneratedNever();

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(255);
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
