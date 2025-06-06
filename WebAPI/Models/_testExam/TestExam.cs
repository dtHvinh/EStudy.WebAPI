﻿using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models._others;

namespace WebAPI.Models._testExam;

[Table("TestExams")]
public class TestExam
{
    public int Id { get; set; }
    public required string Title { get; set; }

    [ForeignKey(nameof(Creator))]
    public int CreatorId { get; set; }
    public User Creator { get; set; } = default!;

    public bool IsPublic { get; set; }

    public ICollection<Question> Questions { get; set; } = default!;
}
