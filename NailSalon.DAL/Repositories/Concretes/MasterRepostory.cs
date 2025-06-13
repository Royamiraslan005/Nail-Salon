using Microsoft.EntityFrameworkCore;
using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;
using NailSalon.DAL.Contexts;
using NailSalon.DAL.Repositories.Abstractions;


namespace NailSalon.DAL.Repositories.Concretes
{
    public class MasterRepository : IMasterRepository
    {

        private readonly AppDbContext _context;

        public MasterRepository(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<List<MasterVm>> GetAllAsync()
        {
            return await _context.Masters
                .Select(m => new MasterVm
                {
                    Id = m.Id,
                    FullName = m.FullName,

                    ImageUrl = m.ImageUrl
                }).ToListAsync();

        }
        public async Task<string> Create(Master master)
        {
          await  _context.Masters.AddAsync(master);
            return "elave edildi";
        }
        public void Update(Master master)
        {
             _context.Masters.Update(master);

        }
        public void SaveAllChange()
        {
            _context.SaveChanges();
        }

        public async Task<MasterVm> GetByIdAsync(int id)
        {
            var master = _context.Masters.Find(id);
            return new MasterVm()
            {
                FullName = master.FullName,
                Experience=master.Experience,
                Zodiac=master.Zodiac,
                Specialty=master.Specialty,
                ImageUrl=master.ImageUrl,
                Id=master.Id
            };
        }

        public async Task DeleteAsync(int id)
        {
            var master = await _context.Masters.FindAsync(id);
            if (master != null)
            {
                _context.Masters.Remove(master);
            }
        }

      
    }
}

