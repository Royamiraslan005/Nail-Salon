using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.ViewModels;
using NailSalon.DAL.Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Concretes
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repo;

        public ReviewService(IReviewRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ReviewVm>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task CreateAsync(ReviewVm vm) => await _repo.CreateAsync(vm);
        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<ReviewVm> GetByIdAsync(int id)
        {
            var review = await _repo.GetByIdAsync(id);

            if (review == null)
                return null;

            return new ReviewVm
            {
                Id = review.Id,
                FullName = review.FullName,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt
            };
        }

    }

}
