namespace WebAPI.Models;

public class Question
{
    public int Id { get; set; }
    public string Text { get; set; }
    public int ExamId { get; set; }
    public TestExam Exam { get; set; }

    public ICollection<Answer> Answers { get; set; }
}