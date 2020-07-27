using System.Collections.Generic;

namespace OnLooker.Core.Interfaces
{
    public interface IGateway<T>
    {
        T Get(int id);
        List<T> GetAll();
        int Create(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
