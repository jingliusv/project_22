using Microsoft.EntityFrameworkCore;
using project_22.Shared.Entity;

namespace project_22.Server.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<ServiceResponse<bool>> DeleteAsync(string email);
        Task<UserEntity> GetUserByIdAsync(int id);
        Task<ServiceResponse<string>> UpdateAsync(UpdateUserForm user, string email);
    }

    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(string email)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (userEntity != null)
            {
                _context.Users.Remove(userEntity);
                await _context.SaveChangesAsync();
                return new ServiceResponse<bool> { Data = true, Message = $"Användare med epost({email}) tas bort." };
            }
            return new ServiceResponse<bool> { Success = false, Message = "Tyvärr, vi kunde inte hittat den här användaren." };
        }

        public async Task<IEnumerable<User>> GetAllAsync()
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

        public async Task<UserEntity> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user != null)
                return user;

            return null!;
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
