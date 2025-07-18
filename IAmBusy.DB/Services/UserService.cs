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

    // ע���û�
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
            //Console.WriteLine("ע��ɹ�");
            return dto;
            // ������֤�ʼ�
            //await _emailService.SendVerificationEmailAsync(user.Email, await _userManager.GenerateEmailVerificationTokenAsync(user));
        }
        catch (Exception ex)
        {
            Console.WriteLine("ע��ʧ��: " + ex.Message);
            return null;
        }


    }

    // �û���¼
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

        // ����JWT����
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            //new Claim(ClaimTypes.Email, user.Email)
            //new Claim(ClaimTypes.Role, user.Roles.First()) // �����û��н�ɫ
        };

        var identity = new ClaimsIdentity(claims, "JWT");
        var principal = new ClaimsPrincipal(identity);
        await _signInManager.SignInAsync(user, isPersistent: true);

        return user;
    }

    // ����JWT����
    //private string GenerateJwtToken(User user)
    //{
    //    var securityKey = _config["Jwt:SecurityKey"];
    //    var issuer = _config["Jwt:Issuer"];
    //    var audience = _config["Jwt:Audience"];

    //    var tokenHandler = new JwtSecurityTokenHandler();
    //    var key = Encoding.ASCII.GetBytes(securityKey);
    //    var tokenDescriptor = new SecurityTokenDescriptor
    //    {
    //        Subject = new ClaimsIdentity(claims: GetClaims(user)),
    //        Expires = DateTime.UtcNow.AddDays(7),
    //        Issuer = issuer,
    //        Audience = audience,
    //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
    //    };

    //    return tokenHandler.CreateToken(tokenDescriptor);
    //}

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
        return Task.FromResult(true); // ������Ҫʵ��ɾ���û����߼�
    }

    // ����������Logout��VerifyEmail�ȣ�ʵ�������߼�...
}

