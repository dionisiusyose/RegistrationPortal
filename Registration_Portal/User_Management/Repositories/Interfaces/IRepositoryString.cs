using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User_Management.Bases;

namespace User_Management.Repositories.Interfaces
{
    public interface IRepositoryString<T> where T : class, IEntityString
    {
        Task<List<T>> Get();
        Task<T> Get(string id);
        Task<T> Post(T entity);
        Task<T> Put(T entity);
        Task<T> Delete(string id);
    }
}
