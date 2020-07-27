using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLooker.Core.Interfaces
{
    public interface ITagGateway: IGateway<STag>
    {
        List<string> GetAllNames();
        int GetByName(string name);
    }
}
