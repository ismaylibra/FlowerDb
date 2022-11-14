using Microsoft.AspNetCore.Identity;

namespace FlowerFTB.Models.IdentityModels
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
    }
}
