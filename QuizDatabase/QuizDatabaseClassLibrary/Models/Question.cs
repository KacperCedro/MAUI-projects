using System;
using System.Collections.Generic;

namespace QuizDatabaseClassLibrary.Models;

public partial class Question
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();
}
