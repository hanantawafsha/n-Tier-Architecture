using Mapster;
using n_Tier_Architecture.BLL.Services.Interfaces;
using n_Tier_Architecture.DAL.DTO.Responses;
using n_Tier_Architecture.DAL.Models;
using n_Tier_Architecture.DAL.Repositories.Classes;
using n_Tier_Architecture.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n_Tier_Architecture.BLL.Services.Classes
{
    public class GenericService<TRequest, TResponse, TEntity> : IGenericService<TRequest, TResponse, TEntity> where TEntity : BaseModel
    {
        private readonly IGenericRepository<TEntity> _genericRepository;

        public GenericService(IGenericRepository<TEntity> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public int Create(TRequest request)
        {
            var entity = request.Adapt<TEntity>();
            return _genericRepository.Add(entity);
        }

        public int Delete(int id)
        {
            var entity = _genericRepository.GetById(id);
            if (entity is null) return 0;
            return _genericRepository.Delete(entity);
        }

        public IEnumerable<TResponse> GetAll()
        {
            var entity = _genericRepository.GetAll();
            return entity.Adapt<IEnumerable<TResponse>>();
                }

        public TResponse? GetById(int id)
        {
            var entity = _genericRepository.GetById(id);
            return entity is null ? default : entity.Adapt<TResponse>();
        }

        public bool ToggleStatus(int id)
        {
            var entity = _genericRepository.GetById(id);
            if (entity is null) return false;
            entity.Status = entity.Status == Status.Active ? Status.Inactive : Status.Active;
            _genericRepository.Update(entity);
            return true;
        }

        public int Update(int id, TRequest request)
        {
            var entity = _genericRepository.GetById(id);
            if (entity is null) return 0;
           // entity.Name = request.Name;
           var updatedEntity = request.Adapt(entity);
            return _genericRepository.Update(updatedEntity);
        }
    }
}
