using AutoMapper;
using JavaCourseAPI.DTOs;
using JavaCourseAPI.DTOs.NewQuizDTOs;
using JavaCourseAPI.Models;
using JavaCourseAPI.Repositories.QuizRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaCourseAPI.Services.QuizServices
{
    public class QuizService : IQuizService
    {
        private readonly IMapper _mapper;
        private readonly IQuizRepository _quizRepository;

        public QuizService(IQuizRepository quizRepository, IMapper mapper)
        {
            _quizRepository = quizRepository;
            _mapper = mapper;
        }

        public async Task AddQuizAsync(NewQuizDTO newQuizDTO)
        {
            Quiz quiz = _mapper.Map<Quiz>(newQuizDTO);
            await  _quizRepository.AddQuizAsync(quiz);

            if (newQuizDTO.questionsSingleChoice.Count() > 0)
            {
                for (int i = 0; i < newQuizDTO.questionsSingleChoice.Count(); i++)
                {
                    QuestionSingleChoice questionSC = _mapper.Map<QuestionSingleChoice>(newQuizDTO.questionsSingleChoice[i]);
                    questionSC.IdQuiz = quiz.IdQuiz;               
                    await _quizRepository.AddQuestionSingleChoiceAsync(questionSC);

                    for (int j = 0; j < newQuizDTO.questionsSingleChoice[i].possibleAnswers.Count(); j++)
                    {
                        PossibleAnswerSc possibleAnswerSC = new PossibleAnswerSc();
                        possibleAnswerSC.IdQuestionSingleChoice = questionSC.IdQuestionSingleChoice;
                        possibleAnswerSC.PossibleAnswer = newQuizDTO.questionsSingleChoice[i].possibleAnswers[j];

                        await _quizRepository.AddPossibleAnswerSC(possibleAnswerSC);
                    }
                }
            }

            if (newQuizDTO.questionBlockOrdering.Count() > 0)
            {
                for (int i = 0; i < newQuizDTO.questionBlockOrdering.Count(); i++)
                {
                    QuestionOrdering questionO = new QuestionOrdering();
                    questionO.IdQuiz = quiz.IdQuiz;
                    questionO.QuestionOrdering1 = newQuizDTO.questionBlockOrdering[i].question;
                    questionO.PointsO = newQuizDTO.questionBlockOrdering[i].pointsForQuestion;
                    await _quizRepository.AddQuestionOrderingAsync(questionO);

                    for (int j = 0; j < newQuizDTO.questionBlockOrdering[i].answers.Count(); j++)
                    {
                        AnswerOrdering answerBlock = new AnswerOrdering();
                        answerBlock.IdQuestionOrdering = questionO.IdQuestionOrdering;
                        answerBlock.AnswerBlock = newQuizDTO.questionBlockOrdering[i].answers[j].answerBlock;
                        answerBlock.AnswerPosition = newQuizDTO.questionBlockOrdering[i].answers[j].answerPosition;

                        await _quizRepository.AddAnswerBlock(answerBlock);
                    }
                }
            }
        }

        public async Task<List<QuizInCategoryTableDTO>> GetQuizesInCategory(int categoryId)
        {
            var quizes = await _quizRepository.GetQuizesInCategory(categoryId);
            List<QuizInCategoryTableDTO> quizesInCategory = _mapper.Map<List<QuizInCategoryTableDTO>>(quizes);
            return quizesInCategory;
        }

        public async Task<QuizToAnswer> GetQuizToAnswerAsync(int quizId)
        {
            var quiz = await _quizRepository.GetQuizToAnswerAsync(quizId);
            QuizToAnswer quizToAnswer = _mapper.Map<QuizToAnswer>(quiz);
            quizToAnswer.quizQuestions = new List<QuizQuestion>();

            var singleQuestions = await _quizRepository.GetSingleChoiceQuestionsAsync(quizId);
            var orderingQuestions = await _quizRepository.GetOrderingQuestionsAsync(quizId);

            if (singleQuestions.Count > 0)
            {
                for (int i = 0; i < singleQuestions.Count; i++)
                {
                    var lista = singleQuestions[i].PossibleAnswerSc.ToArray<PossibleAnswerSc>();

                    QuizQuestion quizQuestion = new QuizQuestion();
                    quizQuestion.isAnswered = false;
                    quizQuestion.isAnsweredCorrectly = false;
                    quizQuestion.questionType = "single-choice";
                    quizQuestion.question = singleQuestions[i].Question;
                    quizQuestion.correctAnswer = singleQuestions[i].CorrectAnswer;
                    quizQuestion.pointsForQuestion = singleQuestions[i].PointsSc;
                    quizQuestion.userAnswer = "";
                    quizQuestion.possibleAnswers = new List<string>();

                    Random random = new Random();
                    //może być źle przedział podany->
                    int correctAnswerNumber = random.Next(0, singleQuestions[i].PossibleAnswerSc.Count());
                    for (int j = 0; j < singleQuestions[i].PossibleAnswerSc.Count(); j++)
                    {
                        //w losowym miejscu umieszcza prawidłową odpowiedź
                        if (j == correctAnswerNumber)
                        {
                            quizQuestion.possibleAnswers.Add(quizQuestion.correctAnswer);
                        }
                        quizQuestion.possibleAnswers.Add(lista[j].PossibleAnswer);
                    }
                    quizToAnswer.quizQuestions.Add(quizQuestion);
                }            
            }

            if (orderingQuestions.Count > 0)
            {
                for (int i = 0; i < orderingQuestions.Count; i++)
                {
                    var blocksList = orderingQuestions[i].AnswerOrdering.ToList<AnswerOrdering>();
                    QuizQuestion quizQuestion = new QuizQuestion();
                    quizQuestion.isAnswered = false;
                    quizQuestion.isAnsweredCorrectly = false;
                    quizQuestion.questionType = "order-type";
                    quizQuestion.question = orderingQuestions[i].QuestionOrdering1;
                    quizQuestion.pointsForQuestion = orderingQuestions[i].PointsO;
                    quizQuestion.possibleAnswers = new List<string>();
                    quizQuestion.correctOrder = new List<string>();

                    for (int j = 0; j < orderingQuestions[i].AnswerOrdering.Count(); j++)
                    {
                        quizQuestion.correctOrder.Add(blocksList[j].AnswerBlock);
                    }

                    Random random = new Random();
                    for (int j = 0; j < orderingQuestions[i].AnswerOrdering.Count(); j++)
                    {
                        //może być źle przedział podany->
                        int randomIndex = random.Next(0, blocksList.Count());
                        quizQuestion.possibleAnswers.Add(blocksList[randomIndex].AnswerBlock);
                        blocksList.RemoveAt(randomIndex);
                    }
                    quizToAnswer.quizQuestions.Add(quizQuestion);
                }            
            }
            return quizToAnswer;
        }

        public async Task SaveAnswerForQuizAsync(AnswerQuizDTO answerQuiz)
        {
            UserQuizAnswers userAnswerCheck = await _quizRepository.CheckIfUserAnswerExistAsync(answerQuiz.idUser, answerQuiz.idQuiz);

            if (userAnswerCheck == null)
            {
                UserQuizAnswers userAnswer = _mapper.Map<UserQuizAnswers>(answerQuiz);
                await _quizRepository.SaveAnswerForQuizAsync(userAnswer);
            }
            else 
            {
                userAnswerCheck.UserScore = answerQuiz.userScore;
                userAnswerCheck.Passed = answerQuiz.passed;
                userAnswerCheck.AnsweredDate = answerQuiz.answeredDate;

                await _quizRepository.UpdateUserAnswerAsync(userAnswerCheck);
            }          
        }
    }
}
