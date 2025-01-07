using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzApp
{
    internal class ViewModel:BindableObject
    {
        //public int CurrentIndex { get; set; }
        private int currentIndex;

        public int CurrentIndex
        {
            get { return currentIndex; }
            set { currentIndex = value; OnPropertyChanged(); }
        }

        public List<string> Questions { get; set; }
        //public string CurrentQuestion { get; set; }
        private string curentQuestion;

        public string CurrentQuestion
        {
            get { return curentQuestion; }
            set { curentQuestion = value; OnPropertyChanged(); }
        }

        public List<int> CorrectIndex { get; set; }
        public List<List<string>> Answares { get; set; }
        public List<int> CheckedAnswares { get; set; }

        private string answare0Content;

        public string Answare0Content
        {
            get { return answare0Content; }
            set { answare0Content = value; OnPropertyChanged(); }
        }
        private string answare1Content;

        public string Answare1Content
        {
            get { return answare1Content; }
            set { answare1Content = value; OnPropertyChanged(); }
        }
        private string answare2Content;

        public string Answare2Content
        {
            get { return answare2Content; }
            set { answare2Content = value; OnPropertyChanged(); }
        }
        private string answare3Content;

        public string Answare3Content
        {
            get { return answare3Content; }
            set { answare3Content = value; OnPropertyChanged(); }
        }

        private bool checked0;

        public bool Checked0
        {
            get { return checked0; }
            set { checked0 = value; OnPropertyChanged(); }
        }
        private bool checked1;

        public bool Checked1
        {
            get { return checked1; }
            set { checked1 = value; OnPropertyChanged(); }
        }
        private bool checked2;

        public bool Checked2
        {
            get { return checked2; }
            set { checked2 = value; OnPropertyChanged(); }
        }
        private bool checked3;

        public bool Checked3
        {
            get { return checked3; }
            set { checked3 = value; OnPropertyChanged(); }
        }

        private string stats;

        public string Stats
        {
            get { return stats; }
            set { stats = value; OnPropertyChanged(); }
        }

        public Command NextQuestionCommand { get; set; }
        public Command PrevQuestionCommand { get; set; }
        public Command CheckCommand { get; set; }
        public Command ResetCommand { get; set; }


        public ViewModel()
        {
            NextQuestionCommand = new Command(NextQuestion);

            PrevQuestionCommand = new Command(PrevQuestion);

            CheckCommand = new Command(CheckAnswares);

            ResetCommand = new Command(ResetAnsw);

            CurrentIndex = 0;

            Questions = new List<string>()
            {
                "2+2",
                "7-1",
                "4-3",
                "Co to binging"
            };
            Answares = new List<List<string>>()
            {
                new List<string>(){"1","2","3","4"},
                new List<string>(){"1","2","6","8"},
                new List<string>(){"1","2","3","4"},
                new List<string>(){"yyyyyyyyy maui","yyyyyyyy","yyyyyy","yyy"}
            };
            CorrectIndex = new List<int>()
            {
                3,2,0,0
            };
            CheckedAnswares = new List<int>() 
            {
                -1,-1,-1,-1
            };

            FillData();
        }

        private void ResetAnsw()
        {
            for (int i = 0; i < CheckedAnswares.Count; i++)
            {
                CheckedAnswares[i] = -1;
            }
            FillData();
        }

        private void CheckAnswares()
        {
            InsertUserAnsw(currentIndex);
            FillData();
            int points = 0;
            for (int i = 0; i < CorrectIndex.Count; i++)
            {
                if (CheckedAnswares[i] == CorrectIndex[i])
                {
                    points++;
                }
            }
            Stats = $"Wynik to {points}/{CorrectIndex.Count}";
        }

        private void NextQuestion()
        {
            InsertUserAnsw(currentIndex);

            if (CurrentIndex < Questions.Count -1)
            {
                CurrentIndex++;
            }
            else
                CurrentIndex = 0;
            

            FillData();
        }

        private void PrevQuestion()
        {
            InsertUserAnsw(currentIndex);

            if (CurrentIndex > 0)
            {
                CurrentIndex--;
            }
            else
                CurrentIndex = Questions.Count - 1;
            

            FillData();
        }

        private void InsertUserAnsw(int index)
        {
            if (checked0)
            {
                CheckedAnswares[index] = 0;
            }
            if (checked1)
            {
                CheckedAnswares[index] = 1;
            }
            if (checked2)
            {
                CheckedAnswares[index] = 2;
            }
            if (checked3)
            {
                CheckedAnswares[index] = 3;
            }
        }

        private void FillData()
        {

            CurrentQuestion = Questions[CurrentIndex];

            Answare0Content = Answares[CurrentIndex][0];
            Answare1Content = Answares[CurrentIndex][1];
            Answare2Content = Answares[CurrentIndex][2];
            Answare3Content = Answares[CurrentIndex][3];

            switch (CheckedAnswares[CurrentIndex])
            {
                case 0:
                    Checked0 = true; 
                    Checked1 = false;
                    Checked2 = false;
                    Checked3 = false;
                    break;

                case 1: 
                    Checked1 = true; 
                    Checked2 = false;
                    Checked3 = false;
                    checked0 = false;

                    break;

                case 2: 
                    Checked2 = true; 
                    checked3 = false;
                    checked1 = false;
                    Checked0 = false;
                    break;

                case 3: 
                    Checked3 = true; 
                    checked1 = false;
                    checked2 = false;
                    Checked0 = false;
                    break;

                default:
                    Checked0 = false;
                    Checked1 = false;
                    Checked2 = false;
                    Checked3 = false;

                    break;
            }

        }
    }
}
