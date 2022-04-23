using Microsoft.EntityFrameworkCore;

namespace project_22.Server.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
    }

    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
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
    }
}
