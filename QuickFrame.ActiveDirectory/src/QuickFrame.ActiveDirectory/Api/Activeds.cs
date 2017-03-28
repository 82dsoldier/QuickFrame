using QuickFrame.ActiveDirectory.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace QuickFrame.ActiveDirectory.Api {

	public static class Activeds {
		public static Guid IID_IDirectorySearch = new Guid("109BA8EC-92F0-11D0-A790-00C04FD8D5A8");
		public static Guid IID_IAds = new Guid("FD8256D0-FD15-11CE-ABC4-02608C9E7553");

		public static long S_ADS_NOMORE_ROWS = 0x00005012L;
		public const int SIZE_LIMIT_EXCEEDED = -2147016669;
		public const int RO_E_CLOSED = unchecked((int)0x80000013);
		public const int E_BOUNDS = unchecked((int)0x8000000B);
		public const int E_CHANGED_STATE = unchecked((int)0x8000000C);
		public const int E_FAIL = unchecked((int)0x80004005);
		public const int E_POINTER = unchecked((int)0x80004003);
		public const int E_NOTIMPL = unchecked((int)0x80004001);
		public const int REGDB_E_CLASSNOTREG = unchecked((int)0x80040154);
		public const int COR_E_AMBIGUOUSMATCH = unchecked((int)0x8000211D);
		public const int COR_E_APPDOMAINUNLOADED = unchecked((int)0x80131014);
		public const int COR_E_APPLICATION = unchecked((int)0x80131600);
		public const int COR_E_ARGUMENT = unchecked((int)0x80070057);
		public const int COR_E_ARGUMENTOUTOFRANGE = unchecked((int)0x80131502);
		public const int COR_E_ARITHMETIC = unchecked((int)0x80070216);
		public const int COR_E_ARRAYTYPEMISMATCH = unchecked((int)0x80131503);
		public const int COR_E_BADIMAGEFORMAT = unchecked((int)0x8007000B);
		public const int COR_E_TYPEUNLOADED = unchecked((int)0x80131013);
		public const int COR_E_CANNOTUNLOADAPPDOMAIN = unchecked((int)0x80131015);
		public const int COR_E_COMEMULATE = unchecked((int)0x80131535);
		public const int COR_E_CONTEXTMARSHAL = unchecked((int)0x80131504);
		public const int COR_E_DATAMISALIGNED = unchecked((int)0x80131541);
		public const int COR_E_TIMEOUT = unchecked((int)0x80131505);
		public const int COR_E_CUSTOMATTRIBUTEFORMAT = unchecked((int)0x80131605);
		public const int COR_E_DIVIDEBYZERO = unchecked((int)0x80020012); // DISP_E_DIVBYZERO
		public const int COR_E_DUPLICATEWAITOBJECT = unchecked((int)0x80131529);
		public const int COR_E_EXCEPTION = unchecked((int)0x80131500);
		public const int COR_E_EXECUTIONENGINE = unchecked((int)0x80131506);
		public const int COR_E_FIELDACCESS = unchecked((int)0x80131507);
		public const int COR_E_FORMAT = unchecked((int)0x80131537);
		public const int COR_E_INDEXOUTOFRANGE = unchecked((int)0x80131508);
		public const int COR_E_INSUFFICIENTMEMORY = unchecked((int)0x8013153D);
		public const int COR_E_INSUFFICIENTEXECUTIONSTACK = unchecked((int)0x80131578);
		public const int COR_E_INVALIDCAST = unchecked((int)0x80004002);
		public const int COR_E_INVALIDCOMOBJECT = unchecked((int)0x80131527);
		public const int COR_E_INVALIDFILTERCRITERIA = unchecked((int)0x80131601);
		public const int COR_E_INVALIDOLEVARIANTTYPE = unchecked((int)0x80131531);
		public const int COR_E_INVALIDOPERATION = unchecked((int)0x80131509);
		public const int COR_E_INVALIDPROGRAM = unchecked((int)0x8013153A);
		public const int COR_E_KEYNOTFOUND = unchecked((int)0x80131577);
		public const int COR_E_MARSHALDIRECTIVE = unchecked((int)0x80131535);
		public const int COR_E_MEMBERACCESS = unchecked((int)0x8013151A);
		public const int COR_E_METHODACCESS = unchecked((int)0x80131510);
		public const int COR_E_MISSINGFIELD = unchecked((int)0x80131511);
		public const int COR_E_MISSINGMANIFESTRESOURCE = unchecked((int)0x80131532);
		public const int COR_E_MISSINGMEMBER = unchecked((int)0x80131512);
		public const int COR_E_MISSINGMETHOD = unchecked((int)0x80131513);
		public const int COR_E_MISSINGSATELLITEASSEMBLY = unchecked((int)0x80131536);
		public const int COR_E_MULTICASTNOTSUPPORTED = unchecked((int)0x80131514);
		public const int COR_E_NOTFINITENUMBER = unchecked((int)0x80131528);
		public const int COR_E_PLATFORMNOTSUPPORTED = unchecked((int)0x80131539);
		public const int COR_E_NOTSUPPORTED = unchecked((int)0x80131515);
		public const int COR_E_NULLREFERENCE = unchecked((int)0x80004003);
		public const int COR_E_OBJECTDISPOSED = unchecked((int)0x80131622);
		public const int COR_E_OPERATIONCANCELED = unchecked((int)0x8013153B);
		public const int COR_E_OUTOFMEMORY = unchecked((int)0x8007000E);
		public const int COR_E_OVERFLOW = unchecked((int)0x80131516);
		public const int COR_E_RANK = unchecked((int)0x80131517);
		public const int COR_E_REFLECTIONTYPELOAD = unchecked((int)0x80131602);
		public const int COR_E_RUNTIMEWRAPPED = unchecked((int)0x8013153E);
		public const int COR_E_SAFEARRAYRANKMISMATCH = unchecked((int)0x80131538);
		public const int COR_E_SAFEARRAYTYPEMISMATCH = unchecked((int)0x80131533);
		public const int COR_E_SAFEHANDLEMISSINGATTRIBUTE = unchecked((int)0x80131623);
		public const int COR_E_SECURITY = unchecked((int)0x8013150A);
		public const int S_OK = 0;

		[DllImport("activeds.dll", CharSet = CharSet.Unicode)]
		public static extern int ADsOpenObject(string path, string userName, string password, int flags, [In][Out] ref Guid iid, [Out, MarshalAs(UnmanagedType.Interface)]out object ppObject);

		public static IDirectorySearch GetDirectorySearcher(string path) {
			object ppObject = null;
			int hr = ADsOpenObject(path, null, null, 1, ref IID_IDirectorySearch, out ppObject);
			if(ppObject == null)
				throw ComException.BuildComException(hr);
			return ppObject as IDirectorySearch;
		}

		public static IAds GetAds(string path) {
			object ppObject = null;
			int hr = ADsOpenObject(path, null, null, 1, ref IID_IAds, out ppObject);
			if(ppObject == null)
				throw ComException.BuildComException(hr);
			return ppObject as IAds;
		}
		public static bool Succeded(int hresult) => hresult >= 0;
		public static bool Failed(int hresult) => hresult < 0;
	}
}