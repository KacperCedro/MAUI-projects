using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp2
{
    internal class Answear:BindableObject
    {
        public string AnswearContent { get; set; }
        public bool IsChecked { get; set; }
        public bool IsCorrect { get; set; }
        public Answear()
        {
            
        }
    }
}
