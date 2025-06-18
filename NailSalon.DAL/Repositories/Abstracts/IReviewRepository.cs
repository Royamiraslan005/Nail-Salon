using NailSalon.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.DAL.Repositories.Abstracts
{
    public interface IReviewRepository
    {
        Task<List<ReviewVm>> GetAllAsync();
        Task<ReviewVm> GetByIdAsync(int id);
        Task CreateAsync(ReviewVm vm);
        Task DeleteAsync(int id);

    }
}
