using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace QuickFrame.ActiveDirectory.Api
{
	[StructLayout(LayoutKind.Explicit)]
	public struct LARGE_INTEGER {
		[FieldOffset(0)]
		public uint LowPart;
		[FieldOffset(4)]
		public int HighPart;
		[FieldOffset(0)]
		public Int64 QuadPart;
	}
} 