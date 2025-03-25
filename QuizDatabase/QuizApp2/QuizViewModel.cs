using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizDatabaseClassLibrary;
using QuizDatabaseClassLibrary.Models;

namespace QuizApp2
{
    internal class QuizViewModel:BindableObject
    {
        public int Points { get; set; }

        private string result;

        public string Result
        {
            get { return result; }
            set { result = value; OnPropertyChanged(); }
        }


        private int currentQuestionIndex;

        public int CurrentQuestionIndex
        {
            get { return currentQuestionIndex; }
            set { currentQuestionIndex = value; OnPropertyChanged(); }
        }


        private QuizQuestion currentQuestion;

        public QuizQuestion CurrentQuestion
        {
            get { return currentQuestion; }
            set {
                currentQuestion = value; 
                OnPropertyChanged(); 
            }
        }

        private List<QuizQuestion> questions;

        public List<QuizQuestion> Questions
        {
            get { return questions; }
            set { questions = value; }
        }

        public Command NextQuestionCommand { get; set; }
        public Command PreviousQuestionCommand { get; set; }
        public Command CheckQuestionsCommand { get; set; }
        public Command ClearQuestionsCommand { get; set; }

        public QuizDBContext dbContext { get; set; }

        public QuizViewModel()
        {

            dbContext = new QuizDBContext();

            Points = 0;

            NextQuestionCommand = new Command(NextQuestion);
            PreviousQuestionCommand = new Command(PreviousQuestion);
            CheckQuestionsCommand = new Command(ShowResult);
            ClearQuestionsCommand = new Command(ClearAnswears);

            CurrentQuestionIndex = 1;



            /*

            Questions = new List<QuizQuestion>() 
            {
                new QuizQuestion()
                {
                    QuestionContent = "Temperatura wrzenia wody",
                    Answears = new List<QuizAnswear>()
                    {
                        new QuizAnswear()
                        {
                            AnswearContent = "100F",
                            IsChecked = false,
                            IsCorrect = false,
                        },
                        new QuizAnswear()
                        {
                            AnswearContent = "100K",
                            IsChecked = false,
                            IsCorrect = false,
                        },
                        new QuizAnswear()
                        {
                            AnswearContent = "100C",
                            IsChecked = false,
                            IsCorrect = true,
                        }
                    }
                },
                new QuizQuestion()
                {
                    QuestionContent = "Temperatura zamarzania wody",
                    Answears = new List<QuizAnswear>()
                    {
                        new QuizAnswear()
                        {
                            AnswearContent = "0F",
                            IsChecked = false,
                            IsCorrect = false,
                        },
                        new QuizAnswear()
                        {
                            AnswearContent = "0C",
                            IsChecked = false,
                            IsCorrect = true,
                        },
                        new QuizAnswear()
                        {
                            AnswearContent = "0K",
                            IsChecked = false,
                            IsCorrect = false,
                        }
                    }
                },
            };
            */
            
            /*
            Questions = dbContext.Questions
                .Include(q => q.Answers)
                .Select(q => new QuizQuestion
            {
                QuestionContent = q.Content,
                Answears = q.Answers.Select(a => new QuizAnswear
                {
                    AnswearContent = a.AnswerContent,
                    IsCorrect = a.IsCorrect,
                    IsChecked = false
                }).ToList()
            }).ToList();  
            */


            //CurrentQuestion = Questions[CurrentQuestionIndex];
            CurrentQuestion = GetCurrentQuestion();
        }
        private QuizQuestion GetCurrentQuestion()
        {
            Question? question = dbContext.Questions
                .Include(q => q.Answers)
                .FirstOrDefault(q => q.Id == CurrentQuestionIndex);
            QuizQuestion quizQuestion = new QuizQuestion()
            {
                QuestionContent = question.Content,
                Answears = question.Answers.Select(a => new QuizAnswear()
                {
                    AnswearContent = a.AnswerContent,
                    IsCorrect = a.IsCorrect,
                    IsChecked = false
                }).ToList(),
            };

            return quizQuestion;
        }
        private void NextQuestion()
        {
            if (CurrentQuestionIndex == dbContext.Questions.Count() )
            {
                CurrentQuestionIndex = 1;
            }
            else
            {
                CurrentQuestionIndex++;
            }
            //CurrentQuestion = Questions[CurrentQuestionIndex];
            CurrentQuestion = GetCurrentQuestion();
        }
        private void PreviousQuestion()
        {
            if (CurrentQuestionIndex == 1)
            {
                CurrentQuestionIndex = dbContext.Questions.Count();
            }
            else
            {
                CurrentQuestionIndex--;
            }
            //CurrentQuestion = Questions[currentQuestionIndex];
            CurrentQuestion = GetCurrentQuestion();
        }
        private void SetResult()
        {
            Result = $"Wynik to {Points} / {Questions.Count} punktów, czyli {(float)(((float)Points/(float)Questions.Count) * 100.0)}%.";
        }
        private void CountPoints()
        {
            Points = 0;
            foreach (var question in Questions)
            {
                List<bool> checkedIdList = new List<bool>();
                List<bool> correctIdList = new List<bool>();
                foreach (var answear in question.Answears)
                {
                    checkedIdList.Add(answear.IsChecked);
                    correctIdList.Add(answear.IsCorrect);
                }
                if (correctIdList.SequenceEqual(checkedIdList))
                {
                    Points++;
                }
            }
        }
        private void ShowResult()
        {
            Points = 0;
            CountPoints();
            SetResult();
        }
        private void ClearAnswears()
        {
            foreach (var question in Questions)
            {
                foreach (var answear in question.Answears)
                {
                    answear.IsChecked = false;
                }
            }

            //CurrentQuestion = Questions[currentQuestionIndex];
            CurrentQuestion = GetCurrentQuestion();
            Points = 0;
            Result = string.Empty;
        }
    }
}
