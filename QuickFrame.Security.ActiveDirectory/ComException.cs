using QuickFrame.Security.ActiveDirectory.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace QuickFrame.Security.ActiveDirectory
{
    public class ComException : Exception
    {
		public static ComException BuildComException(int hresult) => BuildComException(hresult, null);

		public static ComException BuildComException(int hresult, IUnknown intf) {
			if(!(intf is IErrorInfo))
				return new ActiveDirectory.ComException(hresult);
			var errorInterface = intf as IErrorInfo;
			IntPtr ptr = IntPtr.Zero;
			errorInterface.GetDescription(ref ptr);
			var description = Marshal.PtrToStringBSTR(ptr);
			Marshal.FreeBSTR(ptr);
			ptr = IntPtr.Zero;
			errorInterface.GetSource(ref ptr);
			var source = Marshal.PtrToStringBSTR(ptr);
			Marshal.FreeBSTR(ptr);
			ptr = IntPtr.Zero;
			return new ComException(hresult, description, source);
		}

		public ComException(int hResult) 
			: this(hResult, null, null) {

		}
		public ComException(int hResult, string description, string source)
			: base(description) {
			HResult = hResult;
			Source = source;
		}
    }
}
