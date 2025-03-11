using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp2
{
    internal class Question : BindableObject
    {
        public string QuestionContent { get; set; }
        public List<Answear> Answears { get; set; }
        public Question()
        {
            
        }
    }
}
