using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace QuickFrame.ActiveDirectory.Api
{
	[StructLayout(LayoutKind.Sequential)]
    public class ADS_PATH
    {
		public uint Type;
		[MarshalAs(UnmanagedType.LPWStr)]
		public string VolumeName;
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Path;
	}
}
