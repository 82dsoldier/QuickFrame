using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace QuickFrame.ActiveDirectory.Api
{
	[StructLayout(LayoutKind.Sequential)]
    public struct ADS_SEARCHPREF_INFO
    {
		[MarshalAs(UnmanagedType.U4)]
		public ADS_SEARCHPREF dwSearchPref;
		public ADSVALUE vValue;
		[MarshalAs(UnmanagedType.U4)]
		public ADS_STATUS dwStatus;
    }
}
