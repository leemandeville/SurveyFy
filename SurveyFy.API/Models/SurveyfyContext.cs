using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SurveyFy.API.Models
{
    public partial class SurveyfyContext : DbContext
    {
        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<Org> Org { get; set; }
        public virtual DbSet<OrgNode> OrgNode { get; set; }
        public virtual DbSet<Property> Property { get; set; }
        public virtual DbSet<PropertyOption> PropertyOption { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<QuestionType> QuestionType { get; set; }
        public virtual DbSet<Resource> Resource { get; set; }
        public virtual DbSet<Respondent> Respondent { get; set; }
        public virtual DbSet<RespondentStatus> RespondentStatus { get; set; }
        public virtual DbSet<Scale> Scale { get; set; }
        public virtual DbSet<ScaleItem> ScaleItem { get; set; }
        public virtual DbSet<Survey> Survey { get; set; }
        public virtual DbSet<SurveySection> SurveySection { get; set; }
        public virtual DbSet<SurveySectionQuestion> SurveySectionQuestion { get; set; }
        public virtual DbSet<SurveyTaker> SurveyTaker { get; set; }
        public virtual DbSet<SurveyTakerProperty> SurveyTakerProperty { get; set; }

        public SurveyfyContext(DbContextOptions<SurveyfyContext> options)
    : base(options)
{ }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=ACCESS-FKKZK32\SQL2016;Database=Surveyfy;User Id=Surveyfy;Password=1q2w3e4r;ConnectRetryCount=0");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answer)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Answer_Question");

                entity.HasOne(d => d.Respondent)
                    .WithMany(p => p.Answer)
                    .HasForeignKey(d => d.RespondentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Answer_Respondent");
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Org>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<OrgNode>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.OrgNode)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrgNode_Org");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_OrgNode_OrgNode");
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<PropertyOption>(entity =>
            {
                entity.HasIndex(e => e.PropertyId)
                    .HasName("IX_DemographicOption_DemographicId");

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.PropertyOption)
                    .HasForeignKey(d => d.PropertyId)
                    .HasConstraintName("FK_DemographicOption_Demographic_DemographicId");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasIndex(e => e.QuestionTypeId);

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.Question)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Question_Resource");

                entity.HasOne(d => d.Scale)
                    .WithMany(p => p.Question)
                    .HasForeignKey(d => d.ScaleId)
                    .HasConstraintName("FK_Question_Scale");
            });

            modelBuilder.Entity<QuestionType>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.HasIndex(e => e.LanguageId);

                entity.Property(e => e.Text).IsRequired();

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.Resource)
                    .HasForeignKey(d => d.LanguageId);
            });

            modelBuilder.Entity<Respondent>(entity =>
            {
                entity.Property(e => e.DateCompleted).HasColumnType("datetime");

                entity.Property(e => e.DateStarted)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Respondent)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Respondent_SurveyStatus");

                entity.HasOne(d => d.SurveyTaker)
                    .WithMany(p => p.Respondent)
                    .HasForeignKey(d => d.SurveyTakerId)
                    .HasConstraintName("FK_Respondent_SurveyTaker");
            });

            modelBuilder.Entity<RespondentStatus>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Scale>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ScaleItem>(entity =>
            {
                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.ScaleItem)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScaleItem_Resource");

                entity.HasOne(d => d.Scale)
                    .WithMany(p => p.ScaleItem)
                    .HasForeignKey(d => d.ScaleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScaleItem_Scale");
            });

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.Property(e => e.Guid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<SurveySection>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.SurveySection)
                    .HasForeignKey(d => d.SurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Survey_SurveySection");
            });

            modelBuilder.Entity<SurveySectionQuestion>(entity =>
            {
                entity.HasOne(d => d.Question)
                    .WithMany(p => p.SurveySectionQuestion)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveyQuestion_Question");

                entity.HasOne(d => d.SurveySection)
                    .WithMany(p => p.SurveySectionQuestion)
                    .HasForeignKey(d => d.SurveySectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveyQuestion_SurveySection");
            });

            modelBuilder.Entity<SurveyTaker>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Guid).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.SurveyTaker)
                    .HasForeignKey(d => d.SurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveyTaker_Survey");
            });

            modelBuilder.Entity<SurveyTakerProperty>(entity =>
            {
                entity.HasOne(d => d.Property)
                    .WithMany(p => p.SurveyTakerProperty)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveyTakerProperty_Property");

                entity.HasOne(d => d.PropertyOption)
                    .WithMany(p => p.SurveyTakerProperty)
                    .HasForeignKey(d => d.PropertyOptionId)
                    .HasConstraintName("FK_SurveyTakerProperty_PropertyOption");
            });
        }
    }
}
