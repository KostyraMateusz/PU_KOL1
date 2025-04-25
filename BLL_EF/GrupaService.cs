using BLL.DTOModels;
using BLL.ServiceInterfaces;
using DAL;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_EF
{
    public class GrupaService : IGrupaService
    {
        private readonly AppDbContext _context;

        public GrupaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GrupaResponseDTO>> GetAllAsync()
        {
            return await _context.Grupy
                .Select(g => new GrupaResponseDTO
                {
                    ID = g.ID,
                    Nazwa = g.Nazwa,
                    RodzicID = g.RodzicID
                })
                .ToListAsync();
        }

        public async Task<GrupaResponseDTO?> GetByIdAsync(int id)
        {
            var grupa = await _context.Grupy.FindAsync(id);
            if (grupa == null)
                return null;

            return new GrupaResponseDTO
            {
                ID = grupa.ID,
                Nazwa = grupa.Nazwa,
                RodzicID = grupa.RodzicID
            };
        }

        public async Task AddAsync(GrupaRequestDTO grupaDto)
        {
            var grupa = new Grupa
            {
                Nazwa = grupaDto.Nazwa,
                RodzicID = grupaDto.RodzicID
            };

            _context.Grupy.Add(grupa);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, GrupaRequestDTO grupaDto)
        {
            var grupa = await _context.Grupy.FindAsync(id);
            if (grupa != null)
            {
                grupa.Nazwa = grupaDto.Nazwa;
                grupa.RodzicID = grupaDto.RodzicID;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var grupa = await _context.Grupy.FindAsync(id);
            if (grupa != null)
            {
                _context.Grupy.Remove(grupa);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> GetFullGroupNameAsync(int grupaId)
        {
            var builder = new Stack<string>();
            var current = await _context.Grupy.FindAsync(grupaId);

            while (current != null)
            {
                builder.Push(current.Nazwa);
                if (current.RodzicID == null)
                    break;

                current = await _context.Grupy.FindAsync(current.RodzicID.Value);
            }

            return string.Join(" / ", builder);
        }
    }
}
