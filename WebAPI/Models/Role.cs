using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models;

[Table("Roles")]
public class Role : IdentityRole<int>
{
}

public static class R
{
    public const string Student = "Student";
    public const string Instructor = "Instructor";
    public const string Admin = "Admin";
}
