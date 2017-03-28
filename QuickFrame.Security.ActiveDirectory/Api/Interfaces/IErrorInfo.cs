using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace QuickFrame.Security.ActiveDirectory.Api.Interfaces
{
	[ComImport, Guid("1CF2B120-547D-101B-8E65-08002B2BD119"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IErrorInfo : IUnknown
    {
		int GetGUID([Out]out Guid guid);
		int GetSource(ref IntPtr pBstrSource);
		int GetDescription(ref IntPtr pBstrDescription);
		int GetHelpFile(ref IntPtr pBstrHelpFile);
		int GetHelpContext(ref uint pdwHelpContext);
    }
}
