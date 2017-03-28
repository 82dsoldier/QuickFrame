using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace QuickFrame.ActiveDirectory.Api
{
	[StructLayout(LayoutKind.Sequential)]
	public struct ADS_SEARCH_COLUMN {
		[MarshalAs(UnmanagedType.LPWStr)]
		public string pszAttrName;
		[MarshalAs(UnmanagedType.U4)]
		public ADSTYPE dwADsType;
		public IntPtr pADsValues;
		public uint dwNumValues;
		public IntPtr hReserved;
	}
}
