using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace QuickFrame.ActiveDirectory.Api
{
	[StructLayout(LayoutKind.Sequential)]
    public struct ADS_BACKLINK
    {
		public uint RemoteID;
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ObjectName;
    }
}
