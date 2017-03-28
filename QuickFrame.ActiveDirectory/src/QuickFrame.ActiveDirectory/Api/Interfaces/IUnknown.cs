using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace QuickFrame.ActiveDirectory.Api.Interfaces
{
	[ComImport, Guid("00000000-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IUnknown
    {
		int QueryInterface(ref Guid riid, [Out]out object ppObject);
		ulong AddRef();
		ulong Release();
    }
}
