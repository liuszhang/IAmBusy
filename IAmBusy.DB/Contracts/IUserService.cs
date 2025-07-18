using IAmBusy.Model.Models;


public interface IUserService
{
    Task<User?> RegisterUserAsync(User dto);
    Task<User?> LoginUserAsync(User dto);
    Task LogoutUserAsync(string userId);
    Task<bool> DeleteUserAsync(int userId);
    //Task<bool> VerifyEmailAsync(string userId);
    //Task ResetPasswordAsync(ResetPasswordDto dto);
    Task<IEnumerable<User>?> GetAllUsers();
}

