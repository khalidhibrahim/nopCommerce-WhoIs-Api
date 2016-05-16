using Nop.Plugin.Misc.WhoIs.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Nop.Plugin.Misc.WhoIs.Data
{
    public class WhoIsMap : EntityTypeConfiguration<WhoIsRecord>
    {
        public WhoIsMap()
        {
            ToTable("WhoIs");

            HasKey(m => m.Id);

            Property(m => m.IpAddress);
            Property(m => m.Hostname);
            Property(m => m.City);
            Property(m => m.Region);
            Property(m => m.CountryCode);
            Property(m => m.CompanyName);
            Property(m => m.TimeStamp);
        }
    }
}
