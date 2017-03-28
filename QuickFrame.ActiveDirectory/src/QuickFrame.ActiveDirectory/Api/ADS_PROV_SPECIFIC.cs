using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace QuickFrame.ActiveDirectory.Api
{
	[StructLayout(LayoutKind.Sequential)]
    public struct ADS_PROV_SPECIFIC {
		[MarshalAs(UnmanagedType.U4)]
		public uint dwLength;
		public byte[] lpValue;
	}
}
