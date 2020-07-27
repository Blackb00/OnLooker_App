using System;

namespace OnLooker.DataBaseAccess
{
    class CVersion: AVersion
    {
        internal override String Major { get; }
        internal override String Minor { get; }
        internal override String FileNumber { get; }
        internal override String Comment { get; }
        internal override void GetVersion()
        {
            throw new NotImplementedException();
        }

        public CVersion(String major, String minor, String fileNumber, String comment)
        {
            Major = major;
            Minor = minor;
            FileNumber = fileNumber;
            Comment = comment;
        }
    }
}
