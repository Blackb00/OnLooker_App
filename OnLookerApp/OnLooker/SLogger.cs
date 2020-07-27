using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace OnLooker
{
    public static class SLogger
    {
        public static Logger Log { get; }
        static SLogger()
        {
            Log = LogManager.GetCurrentClassLogger();
        }
    }
}
