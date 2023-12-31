using Microsoft.EntityFrameworkCore;

namespace Repositories.Models
{
	public partial class PRO192_Auto_GraderContext : DbContext
	{
		public PRO192_Auto_GraderContext()
		{
		}

		public PRO192_Auto_GraderContext(DbContextOptions<PRO192_Auto_GraderContext> options)
			: base(options)
		{
		}

		public virtual DbSet<Role> Roles { get; set; } = null!;
		public virtual DbSet<Semester> Semesters { get; set; } = null!;
		public virtual DbSet<StudentSemesterSubmission> StudentSemesterSubmissions { get; set; } = null!;
		public virtual DbSet<StudentSubmissionDetail> StudentSubmissionDetails { get; set; } = null!;
		public virtual DbSet<TestCase> TestCases { get; set; } = null!;
		public virtual DbSet<User> Users { get; set; } = null!;

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("server=(local);database=PRO192_Auto_Grader;Trusted_Connection=True;uid=sa;pwd=sa;");
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Role>(entity =>
			{
				entity.Property(e => e.Name).HasMaxLength(50);
			});

			modelBuilder.Entity<Semester>(entity =>
			{
				entity.Property(e => e.SemesterName)
					.HasMaxLength(50)
					.IsUnicode(false);
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
					.HasConstraintName("FK_StudentSemesterSubmissions_Users");
			});

			modelBuilder.Entity<StudentSubmissionDetail>(entity =>
			{
				entity.Property(e => e.GradingTime).HasColumnType("datetime");

				entity.Property(e => e.QuestionDescription).HasMaxLength(50);

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

			modelBuilder.Entity<User>(entity =>
			{
				entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

				entity.Property(e => e.Email)
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.Name)
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.Password).IsUnicode(false);

				entity.HasOne(d => d.Role)
					.WithMany(p => p.Users)
					.HasForeignKey(d => d.RoleId)
					.HasConstraintName("FK_User_Role");
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
