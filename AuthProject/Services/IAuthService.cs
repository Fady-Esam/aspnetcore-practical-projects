using AuthProject.Helpers;

namespace AuthProject.Services
{
    public interface IAuthService
    {
        Task<AuthModel> Register(RegisterModel registerModel);
        Task<AuthModel> LogIn(LogInModel logInModel);
        Task<string> AddRole(RoleModel roleModel);
    }
}
