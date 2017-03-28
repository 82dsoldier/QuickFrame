using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace QuickFrame.Security.ActiveDirectory.Api.Interfaces
{

	[ComImport, Guid("DF0B3D60-548F-101B-8E65-08002B2BD119"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]

	public interface ISupportErrorInfo : IUnknown
    {
		int InterfaceSupportsErrorInfo(ref Guid riid);
    };
}
