using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace TRASH.DomainObjects.Ports
{
    public interface IReadOnlyTRASHRepository
    {
        Task<trash> GetTRASH(long id);

        Task<IEnumerable<trash>> GetAllTRASHs();

        Task<IEnumerable<trash>> QueryTRASHs(ICriteria<trash> criteria);

    }

    public interface IRTRASHRepository
    {
        Task AddTRASH(trash trash);

        Task RemoveTRASH(trash trash);

        Task UpdateTRASH(trash trash);
    }
}
