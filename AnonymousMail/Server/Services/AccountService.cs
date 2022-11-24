using AnonymousMail.Server.Data;
using AnonymousMail.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace AnonymousMail.Server.Services
{
    public class AccountService
    {
        private readonly AppDbContext _context;
        public AccountService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateOrGetUserAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(t => t.Username == username);
            if (user != null)
                return user;

            user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Username = username
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
