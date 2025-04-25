using BLL.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ServiceInterfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentResponseDTO>> GetAllAsync();
        Task<StudentResponseDTO?> GetByIdAsync(int id);
        Task AddAsync(StudentRequestDTO studentDto);
        Task UpdateAsync(int id, StudentRequestDTO studentDto);
        Task DeleteAsync(int id);
    }
}
