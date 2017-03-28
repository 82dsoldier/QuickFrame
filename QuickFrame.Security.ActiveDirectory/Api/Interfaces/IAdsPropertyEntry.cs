using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace QuickFrame.Security.ActiveDirectory.Api.Interfaces
{
	[ComImport Guid("05792c8e-941f-11d0-8529-00c04fd8d503") InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IAdsPropertyEntry
    {
		int Clear();
		string Name { get; set; }
		ADSTYPE ADsType { get; set; }
		int ControlCode { get; set; }
		dynamic Values { get; set; }
    }
}
