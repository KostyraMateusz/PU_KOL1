using BLL.DTOModels;
using BLL.ServiceInterfaces;
using DAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_EF
{
    public class HistoriaService : IHistoriaService
    {
        private readonly AppDbContext _context;

        public HistoriaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HistoriaResponseDTO>> GetPagedAsync(int pageNumber, int pageSize)
        {
            // Implementację z użyciem Entity Framework
            //    return await _context.Historie
            //        .OrderByDescending(h => h.Data)
            //        .Skip((pageNumber - 1) * pageSize)
            //        .Take(pageSize)
            //        .Select(h => new HistoriaResponseDTO
            //        {
            //            ID = h.ID,
            //            Imie = h.Imie,
            //            Nazwisko = h.Nazwisko,
            //            IDGrupy = h.IDGrupy,
            //            TypAkcji = h.TypAkcji,
            //            Data = h.Data
            //        })
            //        .ToListAsync();

            var pageParam = new SqlParameter("@Page", pageNumber);
            var sizeParam = new SqlParameter("@PageSize", pageSize);

            var entries = await _context.Historie
                .FromSqlRaw("EXEC PobierzHistorie @Page, @PageSize", pageParam, sizeParam)
                .AsNoTracking()
                .ToListAsync();

            return entries.Select(h => new HistoriaResponseDTO
            {
                ID = h.ID,
                Imie = h.Imie,
                Nazwisko = h.Nazwisko,
                IDGrupy = h.IDGrupy,
                TypAkcji = h.TypAkcji,
                Data = h.Data
            });
        }
    }
}
