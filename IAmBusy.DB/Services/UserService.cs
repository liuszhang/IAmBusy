using System.Security.Claims;
using IAmBusy.Model.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly MainDbContext _dbContext;

    public UserService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        MainDbContext dbContext)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _dbContext = dbContext;
        _dbContext.Database.EnsureCreatedAsync();
    }

    // 注册用户
    public async Task<User?> RegisterUserAsync(User dto)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user != null)
            {
                await _userManager.ChangePasswordAsync(user, user.Password, dto.Password);
                return user;
            }
            var result = await _userManager.CreateAsync(dto, dto.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.Description);
                }
                return null;
            }
            //Console.WriteLine("注册成功");
            return dto;
            // 发送验证邮件
            //await _emailService.SendVerificationEmailAsync(user.Email, await _userManager.GenerateEmailVerificationTokenAsync(user));
        }
        catch (Exception ex)
        {
            Console.WriteLine("注册失败: " + ex.Message);
            return null;
        }


    }

    // 用户登录
    public async Task<User?> LoginUserAsync(User dto)
    {
        var user = await _userManager.FindByNameAsync(dto.UserName);
        if (user == null)
        {
            return null;
        }
        Console.WriteLine("user: " + user?.UserName);
        Console.WriteLine("password: " + user?.Password);
        Console.WriteLine("user: " + dto?.UserName);
        Console.WriteLine("password: " + dto?.Password);
        var result1 = await _userManager.CheckPasswordAsync(user, dto.Password);
        Console.WriteLine("result1: " + result1);
        if (!result1)
        {
            return null;
        }           

        // 生成JWT令牌
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            //new Claim(ClaimTypes.Email, user.Email)
            //new Claim(ClaimTypes.Role, user.Roles.First()) // 假设用户有角色
        };

        var identity = new ClaimsIdentity(claims, "JWT");
        var principal = new ClaimsPrincipal(identity);
        await _signInManager.SignInAsync(user, isPersistent: true);

        return user;
    }

    public async Task LogoutUserAsync(string userId)
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<IEnumerable<User>?> GetAllUsers()
    {
        return await _userManager.Users.Include(u=>u.UserTasks).ToListAsync();
    }

    public Task<bool> DeleteUserAsync(int userId)
    {
        return Task.FromResult(true); // 这里需要实现删除用户的逻辑
    }

}

