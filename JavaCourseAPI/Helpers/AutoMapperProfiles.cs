using AutoMapper;
using JavaCourseAPI.DTOs;
using JavaCourseAPI.DTOs.ExerciseDTOs;
using JavaCourseAPI.DTOs.NewQuizDTOs;
using JavaCourseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, RegisterDTO>().ReverseMap();

            CreateMap<UserQuizAnswers, AnswerQuizDTO>().ReverseMap();

            CreateMap<StudentGroup, StudentGroupDTO>().ReverseMap();

            CreateMap<GroupToCategory, StudentGroupDTO>()
                .ForMember(dest => dest.IdStudentGroup, opt => opt.MapFrom(src => src.IdStudentGroup))
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.IdStudentGroupNavigation.GroupName));

            CreateMap<Exercise, AddExerciseDTO>().ReverseMap();

            CreateMap<Exercise, ExerciseSimpleViewDTO>().ReverseMap();
            
            CreateMap<Exercise, ExerciseStudentSimpleViewDTO>().ReverseMap();

            CreateMap<Exercise, ExerciseForStudentDTO>().ReverseMap();

            CreateMap<AnswerClassStatistics, AnswerStatisticsDTO>().ReverseMap();

            CreateMap<ExerciseAnswer, ExerciseAnswerForTeacherDTO>()
                .ForMember(dest => dest.AlbumNo, opt => opt.MapFrom(src => src.IdUserNavigation.AlbumNo))
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.IdUserNavigation.IdStudentGroupNavigation.GroupName))
                .ForMember(dest => dest.IdExerciseAnswer, opt => opt.MapFrom(src => src.IdExerciseAnswer))
                .ForMember(dest => dest.ExerciseUserAnswer, opt => opt.MapFrom(src => src.ExerciseUserAnswer))
                .ForMember(dest => dest.ExerciseAnswerDate, opt => opt.MapFrom(src => src.ExerciseAnswerDate))
                .ForMember(dest => dest.PlagiarismByComputer, opt => opt.MapFrom(src => src.PlagiarismByComputer))
                .ForMember(dest => dest.PlagiarismByTeacher, opt => opt.MapFrom(src => src.PlagiarismByTeacher))
                .ForMember(dest => dest.WaitingForTeacherVerification, opt => opt.MapFrom(src => src.WaitingForTeacherVerification));

            CreateMap<ExerciseAnswerSimilarity, PlagiarismComparisonElementDTO>()
                .ForMember(dest => dest.AlbumNo, opt => opt.MapFrom(src => src.IdExerciseAnswerRoleModelNavigation.IdUserNavigation.AlbumNo))
                .ForMember(dest => dest.ExerciseUserAnswer, opt => opt.MapFrom(src => src.IdExerciseAnswerRoleModelNavigation.ExerciseUserAnswer))
                .ForMember(dest => dest.IdExerciseAnswer, opt => opt.MapFrom(src => src.IdExerciseAnswerRoleModelNavigation.IdExerciseAnswer));

            CreateMap<User, UsersForAdminPanel>()
                .ForMember(dest => dest.IdUser, opt => opt.MapFrom(src => src.IdUser))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(dest => dest.Imie, opt => opt.MapFrom(src => src.Imie))
                .ForMember(dest => dest.Nazwisko, opt => opt.MapFrom(src => src.Nazwisko))
                .ForMember(dest => dest.StudentGroup, opt => opt.MapFrom(src => src.IdStudentGroupNavigation))
                .ForMember(dest => dest.Admin, opt => opt.MapFrom(src => src.Admin))
                .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src => src.Teacher))
                .ForMember(dest => dest.AlbumNo, opt => opt.MapFrom(src => src.AlbumNo))
                .ForMember(dest => dest.RegisterDate, opt => opt.MapFrom(src => src.RegisterDate));

            CreateMap<CodeSplitedDTO, CodeAnalyzeInfoDTO>()
                .ForMember(dest => dest.classNameToDisplay, opt => opt.MapFrom(src => src.classInCode))
                .ForMember(dest => dest.classCyclomaticComplexity, opt => opt.MapFrom(src => src.classCyclomaticComplexity))
                .ForMember(dest => dest.functionsInClassToDisplay, opt => opt.MapFrom(src => src.functionsInClassToDisplay))
                .ForMember(dest => dest.functionsCyclomaticComplexity, opt => opt.MapFrom(src => src.functionsCyclomaticComplexity));

            CreateMap<Category, CategoriesDisplayDTO>()
               .ForMember(dest => dest.categoryName, opt => opt.MapFrom(src => src.CategoryName))
               .ForMember(dest => dest.idCategory, opt => opt.MapFrom(src => src.IdCategory))
               .ForMember(dest => dest.createdDate, opt => opt.MapFrom(src => src.CategoryCreatedDate));

            CreateMap<CategoryAddDTO, Category>()
               .ForMember(dest => dest.IdUser, opt => opt.MapFrom(src => src.idUser))
               .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.categoryName))
               .ForMember(dest => dest.CategoryCreatedDate, opt => opt.MapFrom(src => src.createdDate));

            CreateMap<NewQuizDTO, Quiz>()
              .ForMember(dest => dest.QuizName, opt => opt.MapFrom(src => src.quizName))
              .ForMember(dest => dest.PointsToPass, opt => opt.MapFrom(src => src.pointsToPass))
              .ForMember(dest => dest.IdCategory, opt => opt.MapFrom(src => src.categoryId))
              .ForMember(dest => dest.QuizCreatedDate, opt => opt.MapFrom(src => src.quizCreatedDate))
              .ForMember(dest => dest.TimeForQuiz, opt => opt.MapFrom(src => src.timeForQuiz))
              .ForMember(dest => dest.MaxPoints, opt => opt.MapFrom(src => src.maxPoints));

            CreateMap<QuestionSingleChoiceDTO, QuestionSingleChoice>()
              .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.question))
              .ForMember(dest => dest.CorrectAnswer, opt => opt.MapFrom(src => src.correctAnswer))
              .ForMember(dest => dest.PointsSc, opt => opt.MapFrom(src => src.pointsForQuestion));

            
            CreateMap<Quiz, QuizInCategoryTableDTO>()
              .ForMember(dest => dest.quizName, opt => opt.MapFrom(src => src.QuizName))
              .ForMember(dest => dest.quizDate, opt => opt.MapFrom(src => src.QuizCreatedDate));

            CreateMap<Quiz, QuizToAnswer>()
              .ForMember(dest => dest.maxPoints, opt => opt.MapFrom(src => src.MaxPoints))
              .ForMember(dest => dest.pointsToPass, opt => opt.MapFrom(src => src.PointsToPass))
              .ForMember(dest => dest.quizId, opt => opt.MapFrom(src => src.IdQuiz))
              .ForMember(dest => dest.quizTitle, opt => opt.MapFrom(src => src.QuizName))
              .ForMember(dest => dest.timeForQuiz, opt => opt.MapFrom(src => src.TimeForQuiz));

        }
    }
}
