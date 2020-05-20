using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JavaCourseAPI.Models
{
    public partial class Magisterka_v1Context : DbContext
    {
        public Magisterka_v1Context()
        {
        }

        public Magisterka_v1Context(DbContextOptions<Magisterka_v1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<AnswerClassStatistics> AnswerClassStatistics { get; set; }
        public virtual DbSet<AnswerOrdering> AnswerOrdering { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Exercise> Exercise { get; set; }
        public virtual DbSet<ExerciseAnswer> ExerciseAnswer { get; set; }
        public virtual DbSet<ExerciseAnswerSimilarity> ExerciseAnswerSimilarity { get; set; }
        public virtual DbSet<GroupToCategory> GroupToCategory { get; set; }
        public virtual DbSet<PossibleAnswerSc> PossibleAnswerSc { get; set; }
        public virtual DbSet<QuestionOrdering> QuestionOrdering { get; set; }
        public virtual DbSet<QuestionSingleChoice> QuestionSingleChoice { get; set; }
        public virtual DbSet<Quiz> Quiz { get; set; }
        public virtual DbSet<StudentGroup> StudentGroup { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserQuizAnswers> UserQuizAnswers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Magisterka_v3;Trusted_Connection=True;");
                optionsBuilder.UseSqlServer("Server=tcp:magisterka-db-server.database.windows.net,1433;Initial Catalog=MagisterkaDB_v1;Persist Security Info=False;User ID=pumba;Password=Zaq12wsx;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<AnswerClassStatistics>(entity =>
            {
                entity.HasKey(e => e.IdAnswerClassStatistics)
                    .HasName("PK_Answer_Statistics");

                entity.ToTable("Answer_Class_Statistics");

                entity.Property(e => e.IdAnswerClassStatistics).HasColumnName("idAnswerClassStatistics");

                entity.Property(e => e.AbstractMethodes).HasColumnName("abstractMethodes");

                entity.Property(e => e.AnonymousClassQty).HasColumnName("anonymousClassQty");

                entity.Property(e => e.AssignmentsQty).HasColumnName("assignmentsQty");

                entity.Property(e => e.ComparisonQty).HasColumnName("comparisonQty");

                entity.Property(e => e.DefaultFields).HasColumnName("defaultFields");

                entity.Property(e => e.DefaultMethods).HasColumnName("defaultMethods");

                entity.Property(e => e.FinalFields).HasColumnName("finalFields");

                entity.Property(e => e.FinalMethodes).HasColumnName("finalMethodes");

                entity.Property(e => e.LambdasQty).HasColumnName("lambdasQty");

                entity.Property(e => e.LinesOfCode).HasColumnName("linesOfCode");

                entity.Property(e => e.LogStatementsQty).HasColumnName("logStatementsQty");

                entity.Property(e => e.LoopQty).HasColumnName("loopQty");

                entity.Property(e => e.MathOperationsQty).HasColumnName("mathOperationsQty");

                entity.Property(e => e.MaxNestedBlocks).HasColumnName("maxNestedBlocks");

                entity.Property(e => e.McCabeComplexity).HasColumnName("mcCabeComplexity");

                entity.Property(e => e.Modifiers).HasColumnName("modifiers");

                entity.Property(e => e.NumbersQty).HasColumnName("numbersQty");

                entity.Property(e => e.PrivateFields).HasColumnName("privateFields");

                entity.Property(e => e.ProtectedFileds).HasColumnName("protectedFileds");

                entity.Property(e => e.ProtectedMethods).HasColumnName("protectedMethods");

                entity.Property(e => e.PublicFields).HasColumnName("publicFields");

                entity.Property(e => e.PublicMethods).HasColumnName("publicMethods");

                entity.Property(e => e.ReturnQty).HasColumnName("returnQty");

                entity.Property(e => e.StaticFields).HasColumnName("staticFields");

                entity.Property(e => e.StaticInvocations).HasColumnName("staticInvocations");

                entity.Property(e => e.StaticMethods).HasColumnName("staticMethods");

                entity.Property(e => e.StringLiteralsQty).HasColumnName("stringLiteralsQty");

                entity.Property(e => e.SynchronizedFields).HasColumnName("synchronizedFields");

                entity.Property(e => e.SynchronizedMethods).HasColumnName("synchronizedMethods");

                entity.Property(e => e.TotalFields).HasColumnName("totalFields");

                entity.Property(e => e.TotalMethods).HasColumnName("totalMethods");

                entity.Property(e => e.TryCatchQty).HasColumnName("tryCatchQty");

                entity.Property(e => e.UniqueWordsQty).HasColumnName("uniqueWordsQty");

                entity.Property(e => e.VariablesQty).HasColumnName("variablesQty");
            });

            modelBuilder.Entity<AnswerOrdering>(entity =>
            {
                entity.HasKey(e => e.IdAnswerOrdering);

                entity.ToTable("Answer_Ordering");

                entity.HasIndex(e => e.IdQuestionOrdering)
                    .HasName("fkIdx_48");

                entity.Property(e => e.IdAnswerOrdering).HasColumnName("idAnswerOrdering");

                entity.Property(e => e.AnswerBlock)
                    .IsRequired()
                    .HasColumnName("answerBlock")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.AnswerPosition).HasColumnName("answerPosition");

                entity.Property(e => e.IdQuestionOrdering).HasColumnName("idQuestionOrdering");

                entity.HasOne(d => d.IdQuestionOrderingNavigation)
                    .WithMany(p => p.AnswerOrdering)
                    .HasForeignKey(d => d.IdQuestionOrdering)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_48");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory);

                entity.HasIndex(e => e.IdUser)
                    .HasName("fkIdx_13");

                entity.Property(e => e.IdCategory).HasColumnName("idCategory");

                entity.Property(e => e.CategoryCreatedDate)
                    .HasColumnName("categoryCreatedDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasColumnName("categoryName")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Category)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_13");
            });

            modelBuilder.Entity<Exercise>(entity =>
            {
                entity.HasKey(e => e.IdExercise);

                entity.HasIndex(e => e.IdCategory)
                    .HasName("fkIdx_81");

                entity.Property(e => e.IdExercise).HasColumnName("idExercise");

                entity.Property(e => e.ExerciseQuestion)
                    .IsRequired()
                    .HasColumnName("exerciseQuestion")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.IdCategory).HasColumnName("idCategory");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Exercise)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_81");
            });

            modelBuilder.Entity<ExerciseAnswer>(entity =>
            {
                entity.HasKey(e => e.IdExerciseAnswer);

                entity.ToTable("Exercise_Answer");

                entity.HasIndex(e => e.IdAnswerClassStatistics)
                    .HasName("fkIdx_104");

                entity.HasIndex(e => e.IdExercise)
                    .HasName("fkIdx_91");

                entity.HasIndex(e => e.IdUser)
                    .HasName("fkIdx_88");

                entity.Property(e => e.IdExerciseAnswer).HasColumnName("idExerciseAnswer");

                entity.Property(e => e.ExerciseAnswerDate)
                    .HasColumnName("exerciseAnswerDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.ExerciseUserAnswer)
                    .IsRequired()
                    .HasColumnName("exerciseUserAnswer")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.IdAnswerClassStatistics).HasColumnName("idAnswerClassStatistics");

                entity.Property(e => e.IdExercise).HasColumnName("idExercise");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.PlagiarismByComputer).HasColumnName("plagiarismByComputer");

                entity.Property(e => e.PlagiarismByTeacher).HasColumnName("plagiarismByTeacher");

                entity.Property(e => e.WaitingForTeacherVerification).HasColumnName("waitingForTeacherVerification");

                entity.HasOne(d => d.IdAnswerClassStatisticsNavigation)
                    .WithMany(p => p.ExerciseAnswer)
                    .HasForeignKey(d => d.IdAnswerClassStatistics)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_104");

                entity.HasOne(d => d.IdExerciseNavigation)
                    .WithMany(p => p.ExerciseAnswer)
                    .HasForeignKey(d => d.IdExercise)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_91");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.ExerciseAnswer)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_88");
            });

            modelBuilder.Entity<ExerciseAnswerSimilarity>(entity =>
            {
                entity.HasKey(e => e.IdSimilarity);

                entity.ToTable("Exercise_Answer_Similarity");

                entity.HasIndex(e => e.IdExerciseAnswerPlagiarism)
                    .HasName("fkIdx_130");

                entity.HasIndex(e => e.IdExerciseAnswerRoleModel)
                    .HasName("fkIdx_133");

                entity.Property(e => e.IdSimilarity).HasColumnName("idSimilarity");

                entity.Property(e => e.IdExerciseAnswerPlagiarism).HasColumnName("idExerciseAnswerPlagiarism");

                entity.Property(e => e.IdExerciseAnswerRoleModel).HasColumnName("idExerciseAnswerRoleModel");

                entity.HasOne(d => d.IdExerciseAnswerPlagiarismNavigation)
                    .WithMany(p => p.ExerciseAnswerSimilarityIdExerciseAnswerPlagiarismNavigation)
                    .HasForeignKey(d => d.IdExerciseAnswerPlagiarism)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_130");

                entity.HasOne(d => d.IdExerciseAnswerRoleModelNavigation)
                    .WithMany(p => p.ExerciseAnswerSimilarityIdExerciseAnswerRoleModelNavigation)
                    .HasForeignKey(d => d.IdExerciseAnswerRoleModel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_133");
            });

            modelBuilder.Entity<GroupToCategory>(entity =>
            {
                entity.HasKey(e => e.IdGroupToCategory)
                    .HasName("PK_GroupToCategory");

                entity.ToTable("Group_To_Category");

                entity.HasIndex(e => e.IdCategory)
                    .HasName("fkIdx_124");

                entity.HasIndex(e => e.IdStudentGroup)
                    .HasName("fkIdx_121");

                entity.Property(e => e.IdGroupToCategory).HasColumnName("idGroupToCategory");

                entity.Property(e => e.IdCategory).HasColumnName("idCategory");

                entity.Property(e => e.IdStudentGroup).HasColumnName("idStudentGroup");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.GroupToCategory)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_124");

                entity.HasOne(d => d.IdStudentGroupNavigation)
                    .WithMany(p => p.GroupToCategory)
                    .HasForeignKey(d => d.IdStudentGroup)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_121");
            });

            modelBuilder.Entity<PossibleAnswerSc>(entity =>
            {
                entity.HasKey(e => e.IdPossibleAnswerSc)
                    .HasName("PK_Possible_Answer");

                entity.ToTable("Possible_Answer_SC");

                entity.HasIndex(e => e.IdQuestionSingleChoice)
                    .HasName("fkIdx_36");

                entity.Property(e => e.IdPossibleAnswerSc).HasColumnName("idPossibleAnswerSC");

                entity.Property(e => e.IdQuestionSingleChoice).HasColumnName("idQuestionSingleChoice");

                entity.Property(e => e.PossibleAnswer)
                    .IsRequired()
                    .HasColumnName("possibleAnswer")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdQuestionSingleChoiceNavigation)
                    .WithMany(p => p.PossibleAnswerSc)
                    .HasForeignKey(d => d.IdQuestionSingleChoice)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_36");
            });

            modelBuilder.Entity<QuestionOrdering>(entity =>
            {
                entity.HasKey(e => e.IdQuestionOrdering);

                entity.ToTable("Question_Ordering");

                entity.HasIndex(e => e.IdQuiz)
                    .HasName("fkIdx_51");

                entity.Property(e => e.IdQuestionOrdering).HasColumnName("idQuestionOrdering");

                entity.Property(e => e.IdQuiz).HasColumnName("idQuiz");

                entity.Property(e => e.PointsO).HasColumnName("pointsO");

                entity.Property(e => e.QuestionOrdering1)
                    .IsRequired()
                    .HasColumnName("questionOrdering")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdQuizNavigation)
                    .WithMany(p => p.QuestionOrdering)
                    .HasForeignKey(d => d.IdQuiz)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_51");
            });

            modelBuilder.Entity<QuestionSingleChoice>(entity =>
            {
                entity.HasKey(e => e.IdQuestionSingleChoice);

                entity.ToTable("Question_Single_Choice");

                entity.HasIndex(e => e.IdQuiz)
                    .HasName("fkIdx_29");

                entity.Property(e => e.IdQuestionSingleChoice).HasColumnName("idQuestionSingleChoice");

                entity.Property(e => e.CorrectAnswer)
                    .IsRequired()
                    .HasColumnName("correctAnswer")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.IdQuiz).HasColumnName("idQuiz");

                entity.Property(e => e.PointsSc).HasColumnName("pointsSC");

                entity.Property(e => e.Question)
                    .IsRequired()
                    .HasColumnName("question")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdQuizNavigation)
                    .WithMany(p => p.QuestionSingleChoice)
                    .HasForeignKey(d => d.IdQuiz)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_29");
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.HasKey(e => e.IdQuiz);

                entity.HasIndex(e => e.IdCategory)
                    .HasName("fkIdx_21");

                entity.Property(e => e.IdQuiz).HasColumnName("idQuiz");

                entity.Property(e => e.IdCategory).HasColumnName("idCategory");

                entity.Property(e => e.MaxPoints).HasColumnName("maxPoints");

                entity.Property(e => e.PointsToPass).HasColumnName("pointsToPass");

                entity.Property(e => e.QuizCreatedDate)
                    .HasColumnName("quizCreatedDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.QuizName)
                    .IsRequired()
                    .HasColumnName("quizName")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TimeForQuiz).HasColumnName("timeForQuiz");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Quiz)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_21");
            });

            modelBuilder.Entity<StudentGroup>(entity =>
            {
                entity.HasKey(e => e.IdStudentGroup);

                entity.ToTable("Student_Group");

                entity.Property(e => e.IdStudentGroup).HasColumnName("idStudentGroup");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasColumnName("groupName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.HasIndex(e => e.IdStudentGroup)
                    .HasName("fkIdx_112");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.Admin).HasColumnName("admin");

                entity.Property(e => e.AlbumNo)
                    .HasColumnName("albumNo")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RegisterDate)
                    .HasColumnName("registerDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdStudentGroup).HasColumnName("idStudentGroup");

                entity.Property(e => e.Imie)
                    .IsRequired()
                    .HasColumnName("imie")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nazwisko)
                    .IsRequired()
                    .HasColumnName("nazwisko")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasColumnName("passwordHash")
                    .HasMaxLength(512);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasColumnName("passwordSalt")
                    .HasMaxLength(512);

                entity.Property(e => e.Teacher).HasColumnName("teacher");

                entity.HasOne(d => d.IdStudentGroupNavigation)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.IdStudentGroup)
                    .HasConstraintName("FK_112");
            });

            modelBuilder.Entity<UserQuizAnswers>(entity =>
            {
                entity.HasKey(e => e.IdUserQuizAnswers)
                    .HasName("PK_User_Answers");

                entity.ToTable("User_Quiz_Answers");

                entity.HasIndex(e => e.IdQuiz)
                    .HasName("fkIdx_62");

                entity.HasIndex(e => e.IdUser)
                    .HasName("fkIdx_59");

                entity.Property(e => e.IdUserQuizAnswers).HasColumnName("idUserQuizAnswers");

                entity.Property(e => e.AnsweredDate)
                    .HasColumnName("answeredDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdQuiz).HasColumnName("idQuiz");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.Passed).HasColumnName("passed");

                entity.Property(e => e.UserScore).HasColumnName("userScore");

                entity.HasOne(d => d.IdQuizNavigation)
                    .WithMany(p => p.UserQuizAnswers)
                    .HasForeignKey(d => d.IdQuiz)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_62");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserQuizAnswers)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_59");
            });
        }
    }
}
