using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        private Question currentQuestion;

        public Question CurrentQuestion
        {
            get { return currentQuestion; }
            set {
                currentQuestion = value; 
                OnPropertyChanged(); 
            }
        }

        private List<Question> questions;

        public List<Question> Questions
        {
            get { return questions; }
            set { questions = value; }
        }

        public Command NextQuestionCommand { get; set; }
        public Command PreviousQuestionCommand { get; set; }
        public Command CheckQuestionsCommand { get; set; }
        public Command ClearQuestionsCommand { get; set; }

        public QuizViewModel()
        {
            Points = 0;

            NextQuestionCommand = new Command(NextQuestion);
            PreviousQuestionCommand = new Command(PreviousQuestion);
            CheckQuestionsCommand = new Command(ShowResult);
            ClearQuestionsCommand = new Command(ClearAnswears);

            CurrentQuestionIndex = 0;

            Questions = new List<Question>() 
            {
                new Question()
                {
                    QuestionContent = "Temperatura wrzenia wody",
                    Answears = new List<Answear>()
                    {
                        new Answear()
                        {
                            AnswearContent = "100F",
                            IsChecked = false,
                            IsCorrect = false,
                        },
                        new Answear()
                        {
                            AnswearContent = "100K",
                            IsChecked = false,
                            IsCorrect = false,
                        },
                        new Answear()
                        {
                            AnswearContent = "100C",
                            IsChecked = false,
                            IsCorrect = true,
                        }
                    }
                },
                new Question()
                {
                    QuestionContent = "Temperatura zamarzania wody",
                    Answears = new List<Answear>()
                    {
                        new Answear()
                        {
                            AnswearContent = "0F",
                            IsChecked = false,
                            IsCorrect = false,
                        },
                        new Answear()
                        {
                            AnswearContent = "0C",
                            IsChecked = false,
                            IsCorrect = true,
                        },
                        new Answear()
                        {
                            AnswearContent = "0K",
                            IsChecked = false,
                            IsCorrect = false,
                        }
                    }
                },
            };

            CurrentQuestion = Questions[CurrentQuestionIndex];
        }
        private void NextQuestion()
        {
            if (CurrentQuestionIndex == Questions.Count - 1)
            {
                CurrentQuestionIndex = 0;
            }
            else
            {
                CurrentQuestionIndex++;
            }
            CurrentQuestion = Questions[CurrentQuestionIndex];
        }
        private void PreviousQuestion()
        {
            if (CurrentQuestionIndex == 0)
            {
                CurrentQuestionIndex = Questions.Count - 1;
            }
            else
            {
                CurrentQuestionIndex--;
            }
            CurrentQuestion = Questions[currentQuestionIndex];
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

            currentQuestion = Questions[currentQuestionIndex];
            Points = 0;
            Result = string.Empty;
        }
    }
}
