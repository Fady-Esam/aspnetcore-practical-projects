using FruitsAppBackEnd.Models;

namespace BL.Interfaces
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterWithEmailAndPassword(UserModel UserModel);
        Task<AuthModel> LoginWithEmailAndPassword(UserModel UserModel);
        Task<AuthModel> ChangePassword(UserModel UserModel, string newPassword);
    }
}
