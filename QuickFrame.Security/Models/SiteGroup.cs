using QuickFrame.Security.Common.Interfaces;
using QuickFrame.Security.Common.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickFrame.Security.Models
{
    public class SiteGroup : ISiteGroup
    {
		public byte[] Sid { get; set; }
		public string DisplayName { get; set; }
		public virtual ICollection<ISiteUser> Members { get; set; }
    }
}
