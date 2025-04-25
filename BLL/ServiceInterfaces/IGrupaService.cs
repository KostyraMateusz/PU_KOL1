using BLL.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ServiceInterfaces
{
    public interface IGrupaService
    {
        Task<IEnumerable<GrupaResponseDTO>> GetAllAsync();
        Task<GrupaResponseDTO?> GetByIdAsync(int id);
        Task AddAsync(GrupaRequestDTO grupaDto);
        Task UpdateAsync(int id, GrupaRequestDTO grupaDto);
        Task DeleteAsync(int id);
        Task<string> GetFullGroupNameAsync(int grupaId);
    }
}
