using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using GoogleGson;
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
        public List<int> IdList { get; set; }
        
        public QuizViewModel()
        {
            dbContext = new QuizDBContext();

            Points = 0;

            NextQuestionCommand = new Command(NextQuestion);
            PreviousQuestionCommand = new Command(PreviousQuestion);
            CheckQuestionsCommand = new Command(ShowResult);
            ClearQuestionsCommand = new Command(ClearAnswears);
            

            CurrentQuestionIndex = 1;

            //CurrentQuestion = Questions[CurrentQuestionIndex];
            CurrentQuestion = GetCurrentQuestion();
        }
        
        private QuizQuestion GetCurrentQuestion()
        {
            Question question = dbContext.Questions
                .Include(q => q.Answers)
                .FirstOrDefault(q => q.Id == currentQuestionIndex);
            QuizQuestion quizQuestion = new QuizQuestion()
            {
                QuestionContent = question.Content,
                Answears = question.Answers.Select(a => new QuizAnswear()
                {
                    AnswearContent = a.AnswerContent,
                    IsCorrect = a.IsCorrect,
                    IsChecked = a.IsChecked
                }).ToList(),
            };


            return quizQuestion;
        }
        private void NextQuestion()
        {
            SaveCheckedAnswers();
            if (CurrentQuestionIndex == dbContext.Questions.Count())
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
            SaveCheckedAnswers();
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
            Result = $"Wynik to {Points} / {dbContext.Questions.Count()} punktów, czyli {(float)(((float)Points/(float)dbContext.Questions.Count()) * 100.0)}%.";
        }
        /*
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
        */
        private void CountPoints()
        {
            Points = 0;

            foreach (var question in dbContext.Questions)
            {
                List<bool> checkedIdList = new List<bool>();
                List<bool> correctIdList = new List<bool>();
                foreach (var answer in question.Answers)
                {
                    checkedIdList.Add(answer.IsChecked);
                    correctIdList.Add(answer.IsCorrect);
                }
                if (correctIdList.SequenceEqual(checkedIdList))
                {
                    Points++;
                }
            }

        }
        private void ShowResult()
        {
            SaveCheckedAnswers();
            Points = 0;
            CountPoints();
            SetResult();
        }
        private void ClearAnswears()
        {
            foreach (var item in dbContext.Questions)
            {
                foreach (var index in item.Answers)
                {
                    index.IsChecked = false;
                }
            }
            dbContext.SaveChanges();

            //CurrentQuestion = Questions[currentQuestionIndex];
            CurrentQuestion = GetCurrentQuestion();
            Points = 0;
            Result = string.Empty;
        } 
        private void SaveCheckedAnswers()
        {
            var newList = dbContext.Questions
                .Include(x => x.Answers)
                .FirstOrDefault(q => q.Id == currentQuestionIndex)
                .Answers.ToList();

            int counter = 0;
            foreach (var item in newList)
            {
                item.IsChecked = currentQuestion.Answears[counter].IsChecked;
                counter++;
            }

            dbContext.Questions.FirstOrDefault(q => q.Id == currentQuestionIndex)
                .Answers = newList;

            dbContext.SaveChanges();
        }
    }
}
