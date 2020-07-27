using System;
using System.Drawing.Text;
using OnLooker.DataBaseAccess.Gateways;

namespace OnLooker.DataBaseAccess
{
    internal abstract class AVersion
    {
        internal abstract String Major { get; }
        internal abstract String Minor { get; }
        internal abstract String FileNumber { get; }
        internal abstract String Comment { get; }
        internal abstract void GetVersion();

        protected void Upgrade()
        {
            CMigrationGateway migrationGateway = new CMigrationGateway();
            migrationGateway.Create(this);
        }
    }
}