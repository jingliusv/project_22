using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using project_22.Shared.Entity;
using project_22.Shared.Form;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace project_22.Server.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> RegisterAsync(UserRegisterForm user);
        Task<bool> UserExists(string email);
        Task<ServiceResponse<string>> Login(string email, string password);
        int GetUserId();
    }
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(DataContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier));


        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var response = new ServiceResponse<string>();
            
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                response.Success = false;
                response.Message = "Användaren hittas inte.";
            }
            else if(!CompareSecurePassword(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Felt lösenord.";
            }
            else
            {
                response.Data = CreateToken(user);
            }

            return response;
        }

        public async Task<ServiceResponse<int>> RegisterAsync(UserRegisterForm user)
        {
            if(await UserExists(user.Email))
            {
                return new ServiceResponse<int> 
                { 
                    Success = false, 
                    Message = "Det finns redan en användare med den här namnet."
                };
            }

            CreateSecurePassword(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var userEntity = new UserEntity
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                TelephoneNumber = user.TelephoneNumber,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                StreetName = user.StreetName,
                PostalCode = user.PostalCode,
                City = user.City,
            };
            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();

            return new ServiceResponse<int> { Data = userEntity.Id, Message = "Registreringen lyckades!" };
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _context.Users.AnyAsync(u => u.Email == email))
                return true;

            return false;
        }

        private void CreateSecurePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool CompareSecurePassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            bool response = false;

            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var _hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                if (_hash.SequenceEqual(passwordHash))
                    response = true;
                else
                    response = false;
            }

            return response;
        }

        private string CreateToken(UserEntity user)
        {
            List<Claim> claims = new List<Claim> 
            { 
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }
}
