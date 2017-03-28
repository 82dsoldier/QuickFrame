using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace QuickFrame.ActiveDirectory.Api
{
	[StructLayout(LayoutKind.Sequential)]
    public struct ADS_TYPEDNAME
    {
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ObjectName;
		public uint Level;
		public uint Interval;
    }
}
