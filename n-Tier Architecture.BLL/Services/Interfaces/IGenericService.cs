using NTierArchitecture.DAL.DTO.Requests;
using NTierArchitecture.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.BLL.Services.Interfaces
{
    public interface IGenericService<TRequest, TResponse,TEntity>
    {
        int Create(TRequest request);
        IEnumerable<TResponse> GetAll(bool onlyActive=false );
        TResponse? GetById(int id);
        int Update(int id, TRequest request);

        int Delete(int id);
        bool ToggleStatus(int id);
    }
}
