using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models;

[Table("Roles")]
public class Role : IdentityRole<int>
{
}
