using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp2
{
    internal class QuizQuestion : BindableObject
    {
        public string QuestionContent { get; set; }
        public List<QuizAnswear> Answears { get; set; }
        public QuizQuestion()
        {
            
        }
    }
}
