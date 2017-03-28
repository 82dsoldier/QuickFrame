using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace QuickFrame.Security.ActiveDirectory.Api
{
	[StructLayout(LayoutKind.Sequential)]
    public struct ADS_OCTET_STRING {
		[MarshalAs(UnmanagedType.U4)]
		public uint dwLength;
		public IntPtr lpValue;
	}
}
