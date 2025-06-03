using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models._others;

[Table("Roles")]
public class Role : IdentityRole<int>
{
}
