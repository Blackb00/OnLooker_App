using System;
using System.Collections.Generic;


namespace OnLooker.Core
{
    public class UserInfo
    {
        public Int32 ID { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public bool IsGettingEmail { get; set; }
        public List<CReport> Reports { get; set; }
    }
    
}
