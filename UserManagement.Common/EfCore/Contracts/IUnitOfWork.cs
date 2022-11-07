using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UserManagement.Common.EfCore.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }
        int Save();
        Task<int> SaveAsync();
    }
}