using System.Collections.Generic;


namespace OnLooker.Core
{
    public class UserInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsGettingEmail { get; set; }
        public List<CReport> Reports { get; set; }
    }
    
}
