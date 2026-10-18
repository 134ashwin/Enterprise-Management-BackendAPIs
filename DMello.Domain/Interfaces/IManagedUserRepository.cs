using System.Collections.Generic;
using System.Threading.Tasks;
using DMello.Domain.Models;

namespace DMello.Domain.Interfaces
{
    public interface IManagedUserRepository
    {
        Task<List<ManagedUser>> GetAllAsync();
        Task<ManagedUser?> GetByIdAsync(int id);
        Task<ManagedUser?> GetByEmailAsync(string email);
        Task<ManagedUser> AddAsync(ManagedUser user);
        Task UpdateAsync(ManagedUser user);
        Task DeleteAsync(int id);
    }
}
