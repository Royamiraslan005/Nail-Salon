using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;

namespace NailSalon.DAL.Repositories.Abstractions
{
    public interface IMasterRepository 
    {
        Task<List<MasterVm>> GetAllAsync();
        Task<MasterVm> GetByIdAsync(int id);

        Task<string> Create(Master master);
         void Update(Master master);
        void SaveAllChange();
        Task DeleteAsync(int id);
    }
}
