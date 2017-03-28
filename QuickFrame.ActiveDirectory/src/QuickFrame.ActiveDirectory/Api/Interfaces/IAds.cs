using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace QuickFrame.ActiveDirectory.Api.Interfaces
{
	[Guid("FD8256D0-FD15-11CE-ABC4-02608C9E7553")]
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	public interface IAds {
		string ADsPath
		{
			
			get;
		}

		string Class
		{
			
			get;
		}

		string GUID
		{
			
			get;
		}

		string Name
		{
			
			get;
		}

		string Parent
		{
			
			get;
		}

		string Schema
		{
			
			get;
		}

		
		object Get([In] string bstrName);

		
		int GetEx([In] string bstrName, out object value);

		
		void GetInfo();

		
		void GetInfoEx([In] object vProperties, [In] int lnReserved);

		
		void Put([In] string bstrName, [In] object vProp);

		
		void PutEx([In] int lnControlCode, [In] string bstrName, [In] object vProp);

		
		void SetInfo();
	}
}
