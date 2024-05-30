using Smart.Shared.DTOs.UserDTO;
using Task = System.Threading.Tasks.Task;

namespace Smart.Core.Interface.Services
{
    public interface IConfirmEmailService
    {
        Task SendConfirmMailAsync(string userId);

        Task ConfirmEmailAsync(string userId, UserConfirmEmailDTO confirmEmailDTO);

        string DecodeUnicodeBase64(string input);
    }
}
