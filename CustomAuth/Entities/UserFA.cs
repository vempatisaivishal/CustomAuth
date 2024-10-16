using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CustomAuth.Entities
{
    [Index(nameof(Email),IsUnique =true)]
    [Index(nameof(UserName),IsUnique =true)]
    public class UserFA
    {
        [Key]
        public int Id { get; set; }
        //[Required(ErrorMessage = "First Name is Required")]
        //[MaxLength(50, ErrorMessage = "Max Length is 50")]
        public string FirstName { get; set; }
        //[Required(ErrorMessage = "Last Name is Required")]
        //[MaxLength(50, ErrorMessage = "Max Length is 50")]
        public string LastName { get; set; }
        //[Required(ErrorMessage = "Email is Required")]
        //[MaxLength(100, ErrorMessage = "Max Length is 100")]
        public string Email { get; set; }
        //[Required(ErrorMessage = "User Name is Required")]
        //[MaxLength(20, ErrorMessage = "Max Length is 20")]
        public string UserName { get; set; }
        //[Required(ErrorMessage = "Password is Required")]
        //[StringLength(256, MinimumLength = 5, ErrorMessage = "Max Length is 20 and Minimum 5 need to be entered")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }
        //[Compare("Password", ErrorMessage = "Please recheck the correct password")]
        
        //[Required(ErrorMessage = "Date of Birth is Required")]
        //[DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        //[Required(ErrorMessage = "Gender is Required")]
        public string Gender { get; set; }
        
        //public List<string> GenderOptions { get; } = new List<string> { "Male", "Female", "Other" };
        //[Required(ErrorMessage = "At least one skill must be selected")]
        public List<string> Skills { get; set; } = new List<string>();
        
        //public List<string> SkillOptions { get; } = new List<string> { "C#", "Java", "Python", "JavaScript", "HTML/CSS" };
        //[Required(ErrorMessage = "Role is Required")]
        public string Role { get; set; } 
       
        //public List<string> RoleOptions { get; } = new List<string> { "User", "Admin", "Moderator" };
    }
}
