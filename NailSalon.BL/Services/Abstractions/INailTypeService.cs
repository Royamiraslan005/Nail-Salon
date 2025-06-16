using Microsoft.AspNetCore.Http;
using NailSalon.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Abstractions
{
    public interface INailTypeService
    {
        Task<List<NailType>> GetAllAsync();
        Task<NailType> GetByIdAsync(int id);
        Task CreateAsync(NailType model, IFormFile formFile, string wwwRootPath);
        Task UpdateAsync(NailType model, IFormFile formFile, string wwwRootPath);
        Task DeleteAsync(int id, string wwwRootPath);
    }

}
