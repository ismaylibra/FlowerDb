using System.ComponentModel.DataAnnotations;

namespace FlowerFTB.ViewModels
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
