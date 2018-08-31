using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BerryHomeController.Common.Services
{
    public interface IBerryApiService<T>
    {
        Task<ICollection<T>> GetAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> PostAsync(T data);
        Task PutAsync(Guid id, T data);
        Task DeleteAsync(Guid id);
    }
}
