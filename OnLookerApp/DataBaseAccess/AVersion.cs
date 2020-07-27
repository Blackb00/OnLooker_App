using System;

namespace OnLooker.DataBaseAccess
{
    internal abstract class AVersion
    {
        private CMigrationGateway _migrationGateway = new CMigrationGateway();
        internal abstract String Major { get; }
        internal abstract String Minor { get; }
        internal abstract String FileNumber { get; }
        internal abstract String Comment { get; }
        internal abstract void GetVersion();

        protected void Upgrade()
        {
            _migrationGateway.Create(this);
        }
        internal Boolean IsLastVersion()
        {
            AVersion lastVersion = GetLastVersion();
            return lastVersion.Minor == Minor
                   && lastVersion.Major == Major
                   && lastVersion.FileNumber == FileNumber;
        }
        protected AVersion GetLastVersion()
        {
            AVersion lastVersion = _migrationGateway.GetLastVersion();
            return lastVersion;
        }
    }
}