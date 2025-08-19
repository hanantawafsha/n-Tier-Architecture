using n_Tier_Architecture.DAL.DTO.Requests;
using n_Tier_Architecture.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n_Tier_Architecture.BLL.Services.Interfaces
{
    public interface IGenericService<TRequest, TResponse,TEntity>
    {
        int Create(TRequest request);
        IEnumerable<TResponse> GetAll();
        TResponse? GetById(int id);
        int Update(int id, TRequest request);

        int Delete(int id);
        bool ToggleStatus(int id);
    }
}
