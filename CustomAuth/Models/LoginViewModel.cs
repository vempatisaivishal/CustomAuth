using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CustomAuth.Models
{
    public class LoginViewModel
    {
        //[Required(ErrorMessage = "User Name is Required")]
        //[MaxLength(20, ErrorMessage = "Max Length is 20")]
        //[DisplayName("Username or Email")]
        public string UserNameOrEmail { get; set; }
        //[Required(ErrorMessage = "Password is Required")]
        //[StringLength(20, MinimumLength = 5, ErrorMessage = "Max Length is 20 and Minimum 5 need to be entered")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
