using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models;

[Table("StudyTopics")]
public class StudyTopic
{
    public int Id { get; set; }
    public required string Name { get; set; }               // Regular Soccer term in english
    public string? Description { get; set; }
    public required string Category { get; set; }           // Reading, Listening,...
    public required string Level { get; set; }              // "TOEIC", "IELTS", or "TOEFL",...
    public required string LanguageCode { get; set; }       // 'en', 'jp', 'vi', 'ja',...

    public ICollection<StudyActivity> Activities { get; set; } = default!;
}