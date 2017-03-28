using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickFrame.Security.ActiveDirectory.Api
{
    public enum SearchScope
    {
		None = -1,
		Base = ADS_SCOPE.ADS_SCOPE_BASE,
		OneLevel = ADS_SCOPE.ADS_SCOPE_ONELEVEL,
		Subtree = ADS_SCOPE.ADS_SCOPE_SUBTREE
    }
}
