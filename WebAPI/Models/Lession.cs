namespace WebAPI.Models;

public class Lesson
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string VideoUrl { get; set; }
    public int ChapterId { get; set; }
    public Chapter Chapter { get; set; }
}
