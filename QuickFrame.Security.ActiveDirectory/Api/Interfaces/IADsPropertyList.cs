using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace QuickFrame.Security.ActiveDirectory.Api.Interfaces
{
	[ComImport Guid("c6f602b6-8f69-11d0-8528-00c04fd8d503") InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IADsPropertyList
    {
		[DispId(2)]
		int PropertyCount { get; }

		[DispId(6)]
		dynamic GetPropertyItem(string bstrName, int lnADsType);
		[DispId(0)]
		dynamic Item(object varIndex);
		[DispId(3)]
		dynamic Next();
		[DispId(9)]
		void PurgePropertyList();
		[DispId(7)]
		void PutPropertyItem(object varData);
		[DispId(5)]
		void Reset();
		[DispId(8)]
		void ResetPropertyItem(object varEntry);
		[DispId(4)]
		void Skip(int cElements);
	}
}
