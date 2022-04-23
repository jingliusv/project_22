using Microsoft.EntityFrameworkCore;

namespace project_22.Server.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<ServiceResponse<bool>> Delete(string email);
        Task<ServiceResponse<string>> UpdateAsync(UpdateUserForm user, string email);
    }

    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;

        public UserService(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ServiceResponse<bool>> Delete(string email)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (userEntity != null)
            {
                _context.Users.Remove(userEntity);
                await _context.SaveChangesAsync();
                return new ServiceResponse<bool> { Data = true, Message = $"Användare med epost({email}) tas bort." };
            }
            return new ServiceResponse<bool> { Data = false, Message = "Tyvärr, vi kunde inte hittat den här användaren." };
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = new List<User>();
            foreach (var u in await _context.Users.ToListAsync())
            {
                users.Add(new User
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    TelephoneNumber = u.TelephoneNumber,
                    StreetName = u.StreetName,
                    PostalCode = u.PostalCode,
                    City = u.City
                });
            }
            return users;
        }

        public async Task<ServiceResponse<string>> UpdateAsync(UpdateUserForm user, string email)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (userEntity != null)
            {
                userEntity.FirstName = user.FirstName;
                userEntity.LastName = user.LastName;
                userEntity.TelephoneNumber = user.TelephoneNumber;
                userEntity.StreetName = user.StreetName;
                userEntity.PostalCode = user.PostalCode;
                userEntity.City = user.City;
                _context.Entry(userEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new ServiceResponse<string> { Data = userEntity.Email, Message = "Uppdateringen lyckades!" };
            }
            return new ServiceResponse<string> { Success = false, Message = "Tyvärr, vi kunde inte uppdatera!" };
        }
    }
}
