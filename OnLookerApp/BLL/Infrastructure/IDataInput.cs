using System;

namespace OnLooker.Core
{
    public interface IDataInput
    {
        CReport PutRequest(QueryInfo dataInput);
    }
}
