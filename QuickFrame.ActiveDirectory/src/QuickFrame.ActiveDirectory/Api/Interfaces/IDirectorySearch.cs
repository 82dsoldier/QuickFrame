using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace QuickFrame.ActiveDirectory.Api.Interfaces
{
	[ComImport, Guid("109BA8EC-92F0-11D0-A790-00C04FD8D5A8"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IDirectorySearch : IUnknown {

		int SetSearchPreference(
			[In] IntPtr pSearchPrefs,
			int dwNumPrefs);


		int ExecuteSearch(
			[In, MarshalAs(UnmanagedType.LPWStr)] string pszSearchFilter,
			[In, MarshalAs(UnmanagedType.LPArray)] string[] pAttributeNames,
			[In] int dwNumberAttributes,
			[Out] out IntPtr hSearchResult);


		int AbandonSearch([In] IntPtr hSearchResult);

		[return: MarshalAs(UnmanagedType.U4)]
		[PreserveSig]
		int GetFirstRow([In] IntPtr hSearchResult);

		[return: MarshalAs(UnmanagedType.U4)]
		[PreserveSig]
		int GetNextRow([In] IntPtr hSearchResult);

		[return: MarshalAs(UnmanagedType.U4)]
		[PreserveSig]
		int GetPreviousRow([In] IntPtr hSearchResult);

		[return: MarshalAs(UnmanagedType.U4)]
		[PreserveSig]
		int GetNextColumnName(
			[In] IntPtr hSearchResult,
			[Out] IntPtr ppszColumnName);


		void GetColumn(
			[In] IntPtr hSearchResult,
			[In] IntPtr szColumnName,
			[In] IntPtr pSearchColumn);


		void FreeColumn([In] IntPtr pSearchColumn);


		void CloseSearchHandle([In] IntPtr hSearchResult);
	}
}
