using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace QuickFrame.Security.ActiveDirectory.Api.Interfaces
{
	[Guid("FD8256D0-FD15-11CE-ABC4-02608C9E7553")]
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	public interface IAds {
		[DispId(5)]
		string ADsPath { get; }
		[DispId(3)]
		string Class { get; }
		[DispId(4)]
		string GUID { get; }
		[DispId(2)]
		string Name { get; }
		[DispId(6)]
		string Parent { get; }
		[DispId(7)]
		string Schema { get; }

		[DispId(10)]
		dynamic Get(string bstrName);
		[DispId(12)]
		dynamic GetEx(string bstrName);
		[DispId(8)]
		void GetInfo();
		[DispId(14)]
		void GetInfoEx(object vProperties, int lnReserved);
		[DispId(11)]
		void Put(string bstrName, object vProp);
		[DispId(13)]
		void PutEx(int lnControlCode, string bstrName, object vProp);
		[DispId(9)]
		void SetInfo();
	}
}
