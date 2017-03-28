using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickFrame.Security.Common.Interfaces.Models
{
    public interface ISiteGroup
    {
		byte[] Sid { get; set; }
		string DisplayName { get; set; }
		ICollection<ISiteUser> Members { get; set; }
    }
}
