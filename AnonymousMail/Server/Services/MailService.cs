using AnonymousMail.Server.Data;
using AnonymousMail.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace AnonymousMail.Server.Services
{
    public class MailService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string UserId => _httpContextAccessor.HttpContext!.User.Claims.First(t => t.Type == ClaimTypes.NameIdentifier).Value;
        public MailService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<User>> SearchUsersAsync(string username)
        {
            if (username == null || username == string.Empty)
                return new List<User>();

            var users = (await _context.Users.ToListAsync())
                        .Where(user => user.Username.Contains(username, StringComparison.CurrentCultureIgnoreCase));
            return users;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var allUsers = await _context.Users.ToListAsync();
            return allUsers!;
        }

        public async Task<User?> GetUserDetailsAsync(string userId)
        {
            return await _context.Users.Where(user => user.Id == userId).FirstOrDefaultAsync();
        }

        public async Task<MailMessage> SaveMessageAsync(MailMessage message)
        {
            message.FromUserId = UserId;
            message.CreatedDate = DateTime.UtcNow;
            message.ToUser = await _context.Users.Where(user => user.Id == message.ToUserId).FirstAsync();
            _context.MailMessages.Add(message);
            await _context.SaveChangesAsync();

            return await _context.MailMessages
                .Include(t => t.ToUser)
                .Include(t => t.FromUser)
                .FirstAsync(t => t.Id == message.Id);
        }

        public async Task<List<MailMessage>> GetAllInputMessagesAsync()
        {
            var messages = await _context.MailMessages
                .Include(t => t.ToUser)
                .Include(t => t.FromUser)
                .Where(message => message.ToUserId == UserId)
                .ToListAsync();

            return messages;
        }

        public async Task<List<MailMessage>> GetAllOutputMessagesAsync()
        {
            var messages = await _context.MailMessages
                .Include(t => t.ToUser)
                .Include(t => t.FromUser)
                .Where(message => message.FromUserId == UserId)
                .ToListAsync();

            return messages;
        }
    }
}
