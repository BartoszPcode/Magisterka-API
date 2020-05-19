using AutoMapper;
using JavaCourseAPI.DTOs.ExerciseDTOs;
using JavaCourseAPI.Models;
using JavaCourseAPI.Repositories.ExerciseRepositories;
using JavaCourseAPI.Services.TestCodeServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.Services.ExerciseServices
{
    public class ExerciseService : IExerciseService
    {
        private readonly IMapper _mapper;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly ITestCodeService _testCodeService;

        public ExerciseService(IExerciseRepository exerciseRepository, 
                               IMapper mapper, 
                               ITestCodeService testCodeService)
        {
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
            _testCodeService = testCodeService;
        }

        public async Task<List<ExerciseSimpleViewDTO>> GetExercisesInCategoryAsync(int categoryId)
        {
            var exercises = await _exerciseRepository.GetExercisesInCategoryAsync(categoryId);
            List<ExerciseSimpleViewDTO> listToView = _mapper.Map<List<ExerciseSimpleViewDTO>>(exercises);

            if (listToView.Count() > 0)
            {
                for (int i = 0; i < listToView.Count(); i++)
                {
                    listToView[i].exerciseClick = false;
                    listToView[i].exerciseNumber = ++i;
                    i--;
                }
            }          
            return listToView;
        }

        public async Task<ExerciseForStudentDTO> GetExerciseForStudentAsync(int exerciseId) 
        {
            Exercise exercise = await _exerciseRepository.GetExerciseForStudentAsync(exerciseId);
            ExerciseForStudentDTO exerciseForStudent = _mapper.Map<ExerciseForStudentDTO>(exercise);
            return exerciseForStudent;
        }

        public async Task<List<ExerciseStudentSimpleViewDTO>> GetExercisesUserViewAsync(int categoryId, int userId) 
        {
            var exercises = await _exerciseRepository.GetExercisesInCategoryAsync(categoryId);
            List<ExerciseStudentSimpleViewDTO> listToView = _mapper.Map<List<ExerciseStudentSimpleViewDTO>>(exercises);

            if (listToView.Count > 0)
            {
                for (int i = 0; i < listToView.Count; i++)
                {
                    listToView[i].exerciseNumber = ++i;
                    i--;
                    listToView[i].exerciseClick = false;
                    
                    ExerciseAnswer exerciseAnswer = await _exerciseRepository.GetUserAnswerForExerciseAsync(listToView[i].IdExercise, userId);

                    if (exerciseAnswer != null)
                    {
                        if (exerciseAnswer.WaitingForTeacherVerification == true)
                        {
                            listToView[i].score = "waiting";
                        }
                        else
                        {
                            if (exerciseAnswer.PlagiarismByTeacher == true)
                            {
                                listToView[i].score = "faild";
                            }
                            else
                            {
                                listToView[i].score = "complete";
                            }//tutaj dodać kolejny warunek jak po prostu zła odpowiedź
                        }
                    }
                    else
                    {
                        listToView[i].score = "empty";
                    }
                }
            }
            return listToView;
        }

        public async Task AddExerciseAsync(AddExerciseDTO addExerciseDTO)
        {
            Exercise exercise = _mapper.Map<Exercise>(addExerciseDTO);
            await _exerciseRepository.AddExerciseAsync(exercise);
        }

        public async Task SaveExerciseAnswerAsync(ExerciseAnswerDTO exerciseAnswerDTO)
        {
            var allInfo = await _testCodeService.GenerateAllInfo(exerciseAnswerDTO.ExerciseUserAnswer);
            AnswerStatisticsDTO answerStats = new AnswerStatisticsDTO();

            if (allInfo.classesInfo.Count > 1)
            {
                for (int i = 1; i < allInfo.classesInfo.Count; i++)
                {
                    answerStats.McCabeComplexity = answerStats.McCabeComplexity + Int32.Parse(allInfo.classesInfo[i][4]);

                    answerStats.TotalMethods = answerStats.TotalMethods + Int32.Parse(allInfo.classesInfo[i][8]);
                    answerStats.StaticMethods = answerStats.StaticMethods + Int32.Parse(allInfo.classesInfo[i][9]);
                    answerStats.PublicMethods = answerStats.PublicMethods + Int32.Parse(allInfo.classesInfo[i][10]);
                    answerStats.FinalMethodes = answerStats.FinalMethodes + Int32.Parse(allInfo.classesInfo[i][15]);
                    answerStats.ProtectedMethods = answerStats.ProtectedMethods + Int32.Parse(allInfo.classesInfo[i][12]);
                    answerStats.DefaultMethods = answerStats.DefaultMethods + Int32.Parse(allInfo.classesInfo[i][13]);
                    answerStats.AbstractMethodes = answerStats.AbstractMethodes + Int32.Parse(allInfo.classesInfo[i][14]);
                    answerStats.SynchronizedMethods = answerStats.SynchronizedMethods + Int32.Parse(allInfo.classesInfo[i][16]);

                    answerStats.TotalFields = answerStats.TotalFields + Int32.Parse(allInfo.classesInfo[i][17]);
                    answerStats.StaticFields = answerStats.StaticFields + Int32.Parse(allInfo.classesInfo[i][18]);
                    answerStats.PublicFields = answerStats.PublicFields + Int32.Parse(allInfo.classesInfo[i][19]);
                    answerStats.PrivateFields = answerStats.PrivateFields + Int32.Parse(allInfo.classesInfo[i][20]);
                    answerStats.ProtectedFileds = answerStats.ProtectedFileds + Int32.Parse(allInfo.classesInfo[i][21]);
                    answerStats.DefaultFields = answerStats.DefaultFields + Int32.Parse(allInfo.classesInfo[i][22]);
                    answerStats.FinalFields = answerStats.FinalFields + Int32.Parse(allInfo.classesInfo[i][23]);
                    answerStats.SynchronizedFields = answerStats.SynchronizedFields + Int32.Parse(allInfo.classesInfo[i][24]);

                    answerStats.StaticInvocations = answerStats.StaticInvocations + Int32.Parse(allInfo.classesInfo[i][25]);

                    answerStats.LinesOfCode = answerStats.LinesOfCode + Int32.Parse(allInfo.classesInfo[i][26]);
                    answerStats.ReturnQty = answerStats.ReturnQty + Int32.Parse(allInfo.classesInfo[i][27]);
                    answerStats.LoopQty = answerStats.LoopQty + Int32.Parse(allInfo.classesInfo[i][28]);
                    answerStats.ComparisonQty = answerStats.ComparisonQty + Int32.Parse(allInfo.classesInfo[i][29]);
                    answerStats.TryCatchQty = answerStats.TryCatchQty + Int32.Parse(allInfo.classesInfo[i][30]);
                    answerStats.StringLiteralsQty = answerStats.StringLiteralsQty + Int32.Parse(allInfo.classesInfo[i][32]);
                    answerStats.NumbersQty = answerStats.NumbersQty + Int32.Parse(allInfo.classesInfo[i][33]);
                    answerStats.AssignmentsQty = answerStats.AssignmentsQty + Int32.Parse(allInfo.classesInfo[i][34]);
                    answerStats.MathOperationsQty = answerStats.MathOperationsQty + Int32.Parse(allInfo.classesInfo[i][35]);
                    answerStats.VariablesQty = answerStats.VariablesQty + Int32.Parse(allInfo.classesInfo[i][36]);
                    answerStats.MaxNestedBlocks = answerStats.MaxNestedBlocks + Int32.Parse(allInfo.classesInfo[i][37]);
                    answerStats.AnonymousClassQty = answerStats.AnonymousClassQty + Int32.Parse(allInfo.classesInfo[i][38]);
                    answerStats.LambdasQty = answerStats.LambdasQty + Int32.Parse(allInfo.classesInfo[i][40]);
                    answerStats.UniqueWordsQty = answerStats.UniqueWordsQty + Int32.Parse(allInfo.classesInfo[i][41]);
                    answerStats.Modifiers = answerStats.Modifiers + Int32.Parse(allInfo.classesInfo[i][42]);
                    answerStats.LogStatementsQty = answerStats.LogStatementsQty + Int32.Parse(allInfo.classesInfo[i][43]);

                }
            }        
            //LoopQty  //LinesOfCode   //comparisonsQty
            //numbersQty //mathOperationsQty  //variablesQty

            List<int> answerIdsPlagiarism = new List<int>();
            var allAnswerStatsData = await _exerciseRepository.GetAllStatsAsync(exerciseAnswerDTO.IdExercise);
            //Math.Ceiling(0.5) // 1   //Math.Round(0.5, MidpointRounding.AwayFromZero); // 1    //Math.Floor(0.5); // 0

            //5% na wszystko i 3% na linie kodu
            int methodesRange = (int)Math.Round(answerStats.TotalMethods * 0.1, MidpointRounding.AwayFromZero);
            int fieldsRange = (int)Math.Round(answerStats.TotalFields * 0.05, MidpointRounding.AwayFromZero);
            int linesOfCodeRange = (int)Math.Round(answerStats.LinesOfCode * 0.05, MidpointRounding.AwayFromZero);
            int loopQtyRange = (int)Math.Round(answerStats.LoopQty * 0.05, MidpointRounding.AwayFromZero);
            int comparisonsQtyRange = (int)Math.Round(answerStats.ComparisonQty * 0.05, MidpointRounding.AwayFromZero);

            if (allAnswerStatsData != null)
            {
                for (int i = 0; i < allAnswerStatsData.Count; i++)
                {
                    int comparisonCounter = 0;

                    if (CompareValues(answerStats.TotalMethods, allAnswerStatsData[i].IdAnswerClassStatisticsNavigation.TotalMethods, methodesRange))
                    {
                        comparisonCounter++;
                    }

                    if (CompareValues(answerStats.TotalFields, allAnswerStatsData[i].IdAnswerClassStatisticsNavigation.TotalFields, fieldsRange))
                    {
                        comparisonCounter++;
                    }

                    if (CompareValues(answerStats.LinesOfCode, allAnswerStatsData[i].IdAnswerClassStatisticsNavigation.LinesOfCode, linesOfCodeRange))
                    {
                        comparisonCounter++;
                    }

                    if (CompareValues(answerStats.LoopQty, allAnswerStatsData[i].IdAnswerClassStatisticsNavigation.LoopQty, loopQtyRange))
                    {
                        comparisonCounter++;
                    }

                    if (CompareValues(answerStats.ComparisonQty, allAnswerStatsData[i].IdAnswerClassStatisticsNavigation.ComparisonQty, comparisonsQtyRange))
                    {
                        comparisonCounter++;
                    }

                    if (comparisonCounter >= 3)
                    {
                        answerIdsPlagiarism.Add(allAnswerStatsData[i].IdExerciseAnswer);
                    }

                }
            }

            ExerciseAnswer exerciseAnswer = new ExerciseAnswer();
            exerciseAnswer.IdUser = exerciseAnswerDTO.IdUser;
            exerciseAnswer.IdExercise = exerciseAnswerDTO.IdExercise;
            exerciseAnswer.ExerciseUserAnswer = exerciseAnswerDTO.ExerciseUserAnswer;
            exerciseAnswer.ExerciseAnswerDate = exerciseAnswerDTO.ExerciseAnswerDate;
            exerciseAnswer.WaitingForTeacherVerification = true;
            exerciseAnswer.PlagiarismByTeacher = false;

            if (answerIdsPlagiarism.Count > 0)
            {
                exerciseAnswer.PlagiarismByComputer = true;

            }
            else
            {
                exerciseAnswer.PlagiarismByComputer = false;
            }

            AnswerClassStatistics answerStatsSave = _mapper.Map<AnswerClassStatistics>(answerStats);
            await _exerciseRepository.SaveStatistics(answerStatsSave);
            exerciseAnswer.IdAnswerClassStatistics = answerStatsSave.IdAnswerClassStatistics;

            await _exerciseRepository.SaveAnswerAsync(exerciseAnswer);

            if (answerIdsPlagiarism.Count > 0)
            {
                for (int i = 0; i < answerIdsPlagiarism.Count; i++) 
                {
                    ExerciseAnswerSimilarity exerciseSimilarity = new ExerciseAnswerSimilarity();
                    exerciseSimilarity.IdExerciseAnswerPlagiarism = exerciseAnswer.IdExerciseAnswer;
                    exerciseSimilarity.IdExerciseAnswerRoleModel = answerIdsPlagiarism[i];

                    await _exerciseRepository.SaveExerciseSimilarity(exerciseSimilarity);
                }
            }
        }

        public async Task<List<ExerciseAnswerForTeacherDTO>> GetExerciseAnswersAsync(int exerciseId)
        {
            List<ExerciseAnswer> exerciseAnswers = await _exerciseRepository.GetExerciseAnswersAsync(exerciseId);
            List<ExerciseAnswerForTeacherDTO> exerciseAnswersDTO = _mapper.Map<List<ExerciseAnswerForTeacherDTO>>(exerciseAnswers);

            if (exerciseAnswersDTO.Count > 0)
            {
                int number = 1;
                for (int i =0; i < exerciseAnswersDTO.Count; i++)
                {
                    exerciseAnswersDTO[i].Number = number++;
                    List<ExerciseAnswerSimilarity> answerSimilarity = await _exerciseRepository.GetExerciseAnswerSimilarityAsync(exerciseAnswersDTO[i].IdExerciseAnswer);
                    List<PlagiarismComparisonElementDTO> comaprisonElements = _mapper.Map<List<PlagiarismComparisonElementDTO>>(answerSimilarity);

                    exerciseAnswersDTO[i].PlagiarismElements = comaprisonElements;
                }
            }

            return exerciseAnswersDTO;
        }

        public async Task ExerciseAnswerPassAsync(ExerciseAnswerPass exerciseAnswerPass)
        {
            ExerciseAnswer exerciseAnswer = await _exerciseRepository.GetExerciseAnswerAsync(exerciseAnswerPass.idExerciseAnswer);
            exerciseAnswer.WaitingForTeacherVerification = false;
            if (exerciseAnswerPass.pass == true)
            {
                exerciseAnswer.PlagiarismByTeacher = false;
            }
            else 
            {
                exerciseAnswer.PlagiarismByTeacher = true;
            }
            await _exerciseRepository.UpdateExerciseAnswerAsync(exerciseAnswer);
        }

        private bool CompareValues(int firstValue, int secondValue, int range) 
        {
            // firstValue w przedziale <secondValue - range , secondValue + range>
            if ( firstValue >= secondValue - range && firstValue <= secondValue + range)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
