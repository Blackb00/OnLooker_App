using System;
using System.Collections.Generic;

namespace OnLooker.Core
{
    public interface ITagGateway: IGateway<CTag>
    {
        List<string> GetAllNames();
        Int32 GetByName(String name);
    }
}
