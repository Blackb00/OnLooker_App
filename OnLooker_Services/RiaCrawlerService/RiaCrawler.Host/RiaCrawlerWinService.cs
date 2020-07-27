using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace RiaCrawler.Host
{
    public class RiaCrawlerWinService : ServiceBase
    {
        public ServiceHost serviceHost = null;

        public RiaCrawlerWinService()
        {
            ServiceName = "RiaCrawlerWCF";
        }

        public static void Main(string[] args)
        {
            ServiceBase.Run(new RiaCrawlerWinService());

        }

        protected override void OnStart(string[] args)
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }

            // Create a ServiceHost for the ProductsService type and 
            // provide the base address.
            serviceHost = new ServiceHost(typeof(RiaCrawlerWCF.RiaCrawlerService));

            // Open the ServiceHostBase to create listeners and start 
            // listening for messages.
            serviceHost.Open();
        }

        protected override void OnStop()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
        }
    }
}
