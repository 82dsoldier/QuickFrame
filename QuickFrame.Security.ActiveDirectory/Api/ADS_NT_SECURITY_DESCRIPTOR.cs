using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace QuickFrame.Security.ActiveDirectory.Api
{
	[StructLayout(LayoutKind.Sequential)]
    public struct ADS_NT_SECURITY_DESCRIPTOR
    {
		public uint dwLength;
		public byte[] lpValue;
    }
}
