using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using QuizDatabaseClassLibrary.Models;

namespace QuizDatabaseClassLibrary;

public partial class QuizDBContext : DbContext
{
    public QuizDBContext()
    {
    }

    public QuizDBContext(DbContextOptions<QuizDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=quizdb;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_polish_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("answers");

            entity.HasIndex(e => e.QuestionId, "FK_questionId");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.AnswerContent)
                .HasMaxLength(80)
                .HasColumnName("answerContent");
            entity.Property(e => e.IsCorrect).HasColumnName("isCorrect");
            entity.Property(e => e.QuestionId)
                .HasColumnType("int(11)")
                .HasColumnName("questionId");

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_questionId");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("questions");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Content)
                .HasMaxLength(80)
                .HasColumnName("content");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
