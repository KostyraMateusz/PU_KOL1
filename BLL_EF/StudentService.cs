using BLL.DTOModels;
using BLL.ServiceInterfaces;
using DAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_EF
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentResponseDTO>> GetAllAsync()
        {
            return await _context.Studenci
                .Select(s => new StudentResponseDTO
                {
                    ID = s.ID,
                    Imie = s.Imie,
                    Nazwisko = s.Nazwisko,
                    IDGrupy = s.IDGrupy
                })
                .ToListAsync();
        }

        public async Task<StudentResponseDTO?> GetByIdAsync(int id)
        {
            var student = await _context.Studenci.FindAsync(id);
            if (student == null)
                return null;

            return new StudentResponseDTO
            {
                ID = student.ID,
                Imie = student.Imie,
                Nazwisko = student.Nazwisko,
                IDGrupy = student.IDGrupy
            };
        }

        public async Task AddAsync(StudentRequestDTO studentDto)
        {
            // Implementację z użyciem Entity Framework
            //var student = new Student
            //{
            //    Imie = studentDto.Imie,
            //    Nazwisko = studentDto.Nazwisko,
            //    IDGrupy = studentDto.IDGrupy
            //};

            //_context.Studenci.Add(student);
            //await _context.SaveChangesAsync();

            var imieParam = new SqlParameter("@Imie", studentDto.Imie);
            var nazwiskoParam = new SqlParameter("@Nazwisko", studentDto.Nazwisko);
            var idGrupyParam = new SqlParameter("@IDGrupy", (object?)studentDto.IDGrupy ?? DBNull.Value);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DodajStudenta @Imie, @Nazwisko, @IDGrupy",
                imieParam, nazwiskoParam, idGrupyParam
            );
        }

        public async Task UpdateAsync(int id, StudentRequestDTO studentDto)
        {
            var student = await _context.Studenci.FindAsync(id);
            if (student != null)
            {
                student.Imie = studentDto.Imie;
                student.Nazwisko = studentDto.Nazwisko;
                student.IDGrupy = studentDto.IDGrupy;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _context.Studenci.FindAsync(id);
            if (student != null)
            {
                _context.Studenci.Remove(student);
                await _context.SaveChangesAsync();
            }
        }
    }
}
