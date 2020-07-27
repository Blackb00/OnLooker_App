using System.Security.Cryptography.X509Certificates;

namespace OnLooker.Core
{
    public class CJobGenerator
    { 
        public CJobGenerator(QueryInfo queryDataInput)
        {
            
        }

        public CJob Create()
        {
            CJob job = new CJob();
            return job;
        }
    }
}