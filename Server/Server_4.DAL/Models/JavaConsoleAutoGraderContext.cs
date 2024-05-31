using Microsoft.EntityFrameworkCore;

namespace Server_4.DAL.Models;

public partial class JavaConsoleAutoGraderContext : DbContext
{
	public JavaConsoleAutoGraderContext()
	{
	}

	public JavaConsoleAutoGraderContext(DbContextOptions<JavaConsoleAutoGraderContext> options)
		: base(options)
	{
	}

	public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
	public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
	public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
	public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
	public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
	public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
	public virtual DbSet<Semester> Semesters { get; set; } = null!;
	public virtual DbSet<StudentSemesterSubmission> StudentSemesterSubmissions { get; set; } = null!;
	public virtual DbSet<StudentSubmissionDetail> StudentSubmissionDetails { get; set; } = null!;
	public virtual DbSet<TestCase> TestCases { get; set; } = null!;

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			optionsBuilder.UseSqlServer("server=(local);database=JavaConsoleAutoGrader;Trusted_Connection=True;uid=sa;pwd=sa;");
			optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
		}
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<AspNetRole>(entity =>
		{
			entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
				.IsUnique()
				.HasFilter("([NormalizedName] IS NOT NULL)");

			entity.Property(e => e.Id).ValueGeneratedNever();

			entity.Property(e => e.Name).HasMaxLength(256);

			entity.Property(e => e.NormalizedName).HasMaxLength(256);
		});

		modelBuilder.Entity<AspNetRoleClaim>(entity =>
		{
			entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

			entity.HasOne(d => d.Role)
				.WithMany(p => p.AspNetRoleClaims)
				.HasForeignKey(d => d.RoleId);
		});

		modelBuilder.Entity<AspNetUser>(entity =>
		{
			entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

			entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
				.IsUnique()
				.HasFilter("([NormalizedUserName] IS NOT NULL)");

			entity.Property(e => e.Id).ValueGeneratedNever();

			entity.Property(e => e.Email).HasMaxLength(256);

			entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

			entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

			entity.Property(e => e.UserName).HasMaxLength(256);

			entity.HasMany(d => d.Roles)
				.WithMany(p => p.Users)
				.UsingEntity<Dictionary<string, object>>(
					"AspNetUserRole",
					l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
					r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
					j =>
					{
						j.HasKey("UserId", "RoleId");

						j.ToTable("AspNetUserRoles");

						j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
					});
		});

		modelBuilder.Entity<AspNetUserClaim>(entity =>
		{
			entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

			entity.HasOne(d => d.User)
				.WithMany(p => p.AspNetUserClaims)
				.HasForeignKey(d => d.UserId);
		});

		modelBuilder.Entity<AspNetUserLogin>(entity =>
		{
			entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

			entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

			entity.HasOne(d => d.User)
				.WithMany(p => p.AspNetUserLogins)
				.HasForeignKey(d => d.UserId);
		});

		modelBuilder.Entity<AspNetUserToken>(entity =>
		{
			entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

			entity.HasOne(d => d.User)
				.WithMany(p => p.AspNetUserTokens)
				.HasForeignKey(d => d.UserId);
		});

		modelBuilder.Entity<Semester>(entity =>
		{
			entity.Property(e => e.SemesterName)
				.HasMaxLength(11)
				.IsFixedLength();
		});

		modelBuilder.Entity<StudentSemesterSubmission>(entity =>
		{
			entity.HasKey(e => new { e.StudentId, e.SemesterId });

			entity.HasOne(d => d.Semester)
				.WithMany(p => p.StudentSemesterSubmissions)
				.HasForeignKey(d => d.SemesterId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_StudentSemesterSubmissions_Semesters");

			entity.HasOne(d => d.Student)
				.WithMany(p => p.StudentSemesterSubmissions)
				.HasForeignKey(d => d.StudentId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_StudentSemesterSubmissions_AspNetUsers");
		});

		modelBuilder.Entity<StudentSubmissionDetail>(entity =>
		{
			entity.Property(e => e.GradingTime).HasColumnType("datetime");

			entity.Property(e => e.QuestionDescription).HasMaxLength(200);

			entity.Property(e => e.StudentNote).HasMaxLength(200);

			entity.HasOne(d => d.S)
				.WithMany(p => p.StudentSubmissionDetails)
				.HasForeignKey(d => new { d.StudentId, d.SemesterId })
				.HasConstraintName("FK_StudentSubmissionDetails_StudentSemesterSubmissions");
		});

		modelBuilder.Entity<TestCase>(entity =>
		{
			entity.HasOne(d => d.Semester)
				.WithMany(p => p.TestCases)
				.HasForeignKey(d => d.SemesterId)
				.HasConstraintName("FK_TestCases_Semesters");
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
