using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace NoticeApp.Models
{
	/// <summary>
	/// [5] DbContext Class
	/// </summary>
	public class NoticeAppDbContext : DbContext
	{
		public NoticeAppDbContext()
		{
			// Empty
		}

		public NoticeAppDbContext(DbContextOptions<NoticeAppDbContext> options) : base(options) { }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
				optionsBuilder.UseSqlServer(connectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Notice>().Property(m => m.Created).HasDefaultValueSql("GetDate()");
		}

		public DbSet<Notice> Notices { get; set; }
	}
}
