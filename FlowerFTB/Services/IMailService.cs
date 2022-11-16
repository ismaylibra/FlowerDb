using FlowerFTB.Data;

namespace FlowerFTB.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(RequestEmail requestEmail);
    }
}
