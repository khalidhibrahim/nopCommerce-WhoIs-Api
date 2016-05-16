using Nop.Core;
using System;

namespace Nop.Plugin.Misc.WhoIs.Domain
{
    public interface IDescribableEntity
    {
        string Describe();
    }

    public class WhoIsRecord : BaseEntity
    {
        public virtual string IpAddress { get; set; }
        public virtual string Hostname { get; set; }
        public virtual string City { get; set; }
        public virtual string Region { get; set; }
        public virtual string CountryCode { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual DateTime TimeStamp { get; set; }
    }
}
