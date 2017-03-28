using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace QuickFrame.ActiveDirectory.Api
{
	[StructLayout(LayoutKind.Explicit)]
	public struct ADSVALUE {
		[FieldOffset(0)]
		[MarshalAs(UnmanagedType.U4)]
		public ADSTYPE dwType;

		[FieldOffset(4)]
		internal int pad;

		[FieldOffset(8)]
		public IntPtr pointer;

		[FieldOffset(8)]
		public ADS_OCTET_STRING octetString;

		[FieldOffset(8)]
		public ADS_GENERIC generic;
	}
}
