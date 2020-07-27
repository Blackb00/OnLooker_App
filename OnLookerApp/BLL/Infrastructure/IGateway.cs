using System;
using System.Collections.Generic;

namespace OnLooker.Core
{
    public interface IGateway<T>
    {
        T Get(Int32 id);
        List<T> GetAll();
        Int32 Create(T entity);
        void Update(T entity);
        void Delete(Int32 id);
    }
}
