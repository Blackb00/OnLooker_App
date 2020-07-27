using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using OnLooker.DataBaseAccess.Versions;


namespace OnLooker.DataBaseAccess
{
    public class CDbDeploy
    {
        public static void Create()
        {
            CV_0000 version = new CV_0000();
            version.GetVersion();
            Update();
        }
        public static void Update()                         //todo: implement logic to determine the number of last version
        {
            CV_0001 version = new CV_0001();
            version.GetVersion();
        }
    }
}
