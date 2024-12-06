using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egzamin
{
    internal class EgzaminViewModel : BindableObject
    {
        public Command ThrowDiceCommand { get; set; }
        public Command ResetScoreCommand { get; set; }

        private string photoPath1;

        public string PhotoPath1
        {
            get { return photoPath1; }
            set { photoPath1 = value; OnPropertyChanged(); }
        }

        private string photoPath2;
        public string PhotoPath2
        {
            get { return photoPath2; }
            set { photoPath2 = value; OnPropertyChanged(); }
        }
        private string photoPath3;

        public string PhotoPath3
        {
            get { return photoPath3; }
            set { photoPath3 = value; OnPropertyChanged(); }
        }
        private string photoPath4;

        public string PhotoPath4
        {
            get { return photoPath4; }
            set { photoPath4 = value; OnPropertyChanged(); }
        }
        private string photoPath5;

        public string PhotoPath5
        {
            get { return photoPath5; }
            set { photoPath5 = value; OnPropertyChanged(); }
        }



        private string firstScore;

        public string FirstScore
        {
            get { return firstScore; }
            set 
            { 
                firstScore = value; 
                OnPropertyChanged();
            }
        }

        private string secondScore;

        public string SecondScore
        {
            get { return secondScore; }
            set 
            { 
                secondScore = value; 
                OnPropertyChanged();
            }
        }
        private int totalScore;

        public int TotalScore
        {
            get { return totalScore; }
            set { totalScore = value; OnPropertyChanged(); }
        }


        public EgzaminViewModel()
        {
            ThrowDiceCommand = new Command(throwDice);
            ResetScoreCommand = new Command(resetScorePanel);
            PhotoPath1 = @"question.jpg";
            PhotoPath2 = @"question.jpg";
            PhotoPath3 = @"question.jpg";
            PhotoPath4 = @"question.jpg";
            PhotoPath5 = @"question.jpg";
            FirstScore = "Wynik tego losowania:";
            SecondScore = "Wynik gry:";

        }
        
        private void throwDice()
        {
            Random random = new Random();

            List<int> cubes = new List<int>() {
                random.Next(1, 6),
                random.Next(1, 6),
                random.Next(1, 6),
                random.Next(1, 6),
                random.Next(1, 6),
            };
            
            PhotoPath1 = $@"k{cubes[0]}.jpg";
            PhotoPath2 = $@"k{cubes[1]}.jpg";
            PhotoPath3 = $@"k{cubes[2]}.jpg";
            PhotoPath4 = $@"k{cubes[3]}.jpg";
            PhotoPath5 = $@"k{cubes[4]}.jpg";

            int tmpFirstScore = 0;

            for (int i = 1; i < 7; i++)
            {
                if (cubes.FindAll((cube) => cube == i).Count() > 1)
                {
                    foreach (var item in cubes.FindAll((cube) => cube == i))
                    {
                        tmpFirstScore += i;//* cubes.FindAll((cube) => cube == i).Count();
                    }
                }
            }
            FirstScore = $"Wynik tego losowania: {tmpFirstScore}";
            TotalScore += tmpFirstScore;
            SecondScore = $"Wynik gry: {TotalScore}";
        }
        private void resetScorePanel()
        {
            PhotoPath1 = @"question.jpg";
            PhotoPath2 = @"question.jpg";
            PhotoPath3 = @"question.jpg";
            PhotoPath4 = @"question.jpg";
            PhotoPath5 = @"question.jpg";
            FirstScore = "Wynik tego losowania:";
            SecondScore = "Wynik gry:";
            totalScore  = 0;
        }
    }
}
