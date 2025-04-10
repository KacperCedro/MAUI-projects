﻿using System;
using System.Collections.Generic;

namespace QuizDatabaseClassLibrary.Models;

public partial class Answer
{
    public int Id { get; set; }

    public string AnswerContent { get; set; } = null!;

    public bool IsCorrect { get; set; }

    public int? QuestionId { get; set; }

    public bool IsChecked { get; set; }

    public virtual Question? Question { get; set; }
}
