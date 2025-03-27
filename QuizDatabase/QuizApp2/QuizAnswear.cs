using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp2
{
    internal class QuizAnswear:BindableObject
    {
        public string AnswearContent { get; set; }
        private bool isChecked;

        
        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; OnPropertyChanged(); }
        }
        

        public bool IsCorrect { get; set; }
        public QuizAnswear()
        {
            
        }
    }
}
