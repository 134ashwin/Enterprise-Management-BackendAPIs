using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMello.Domain.Interfaces;
using DMello.Domain.Models;
using DMello.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DMello.Infrastructure.Repositories
{
    public class ManagedUserRepository : IManagedUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ManagedUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ManagedUser>> GetAllAsync()
        {
            return await _context.ManagedUsers
                .Where(u => !u.IsDeleted)
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();
        }

        public async Task<ManagedUser?> GetByIdAsync(int id)
        {
            return await _context.ManagedUsers
                .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        }

        public async Task<ManagedUser?> GetByEmailAsync(string email)
        {
            return await _context.ManagedUsers
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower() && !u.IsDeleted);
        }

        public async Task<ManagedUser> AddAsync(ManagedUser user)
        {
            await _context.ManagedUsers.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateAsync(ManagedUser user)
        {
            _context.ManagedUsers.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.ManagedUsers.FindAsync(id);
            if (user != null)
            {
                user.IsDeleted = true;
                user.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
    }
}
