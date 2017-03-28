using QuickFrame.ActiveDirectory.Api;
using QuickFrame.ActiveDirectory.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using static QuickFrame.ActiveDirectory.Api.Activeds;

namespace QuickFrame.ActiveDirectory
{
    public class DirectorySearcher
    {
		public string SearchBase { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string SearchFilter { get; set; } = "(objectClass=*)";
		public List<string> Attributes { get; private set; } = new List<string>();
		public SearchScope Scope { get; set; } = SearchScope.None;
		public int PageSize { get; set; }
		public int SizeLimit { get; set; }
		public DirectorySearcher() {

		}

		public DirectorySearcher(string searchBase)
			: this(searchBase, null, null, null, null, SearchScope.None, 0, 1000) {
		}

		public DirectorySearcher(string searchBase, SearchScope scope)
			: this(searchBase, null, null, null, null, scope, 0, 1000) {

		}

		public DirectorySearcher(string searchBase, string searchFilter)
			: this(searchBase, searchFilter, null, null, null, SearchScope.None, 0, 1000) {

		}

		public DirectorySearcher(string searchBase, int pageSize) 
			: this(searchBase, null, null, null, null, SearchScope.None, pageSize, 1000) {

		}

		public DirectorySearcher(string searchBase, string searchFilter, SearchScope scope) 
			: this(searchBase, searchFilter, null, null, null, scope, 0, 1000) {

		}

		public DirectorySearcher(string searchBase, string[] attributes)
			:this(searchBase, null, null, null, attributes, SearchScope.None, 0, 1000) {
		}

		public DirectorySearcher(string searchBase, string[] attributes, SearchScope scope)
			: this(searchBase, null, null, null, attributes, scope, 0, 1000) {

		}

		public DirectorySearcher(string searchBase, string searchFilter, string[] attributes)
			: this(searchBase, searchFilter, null, null, attributes, SearchScope.None, 0, 1000) {

		}

		public DirectorySearcher(string searchBase, string searchFilter, string[] attributes, SearchScope scope) 
			: this(searchBase, searchFilter, null, null, attributes, scope, 0, 1000) {

		}

		public DirectorySearcher(string searchBase, string searchFilter, string userName, string password, string[] attributes, SearchScope scope, int pageSize, int sizeLimit) {
			SearchBase = searchBase;
			if(!String.IsNullOrEmpty(searchFilter))
				SearchFilter = searchFilter;
			if(attributes != null && attributes.Any())
				Attributes = new List<string>(attributes);
			UserName = userName;
			Password = password;
			Scope = scope;
			PageSize = pageSize;
			SizeLimit = sizeLimit;
		}

		//public IEnumerable<DirectoryEntry> Search() => Search(null, null, null, null, null, SearchScope.Subtree, 0, 1000);
		//public IEnumerable<DirectoryEntry> Search(string searchFilter) => Search(null, searchFilter, null, null, null, SearchScope.None, 0, 1000);
		//public IEnumerable<DirectoryEntry> Search(string searchFilter, SearchScope scope) => Search(null, searchFilter, null, null, null, scope, 0, 1000);
		//public IEnumerable<DirectoryEntry> Search(string searchBase, string[] attributes) => Search(searchBase, null, null, null, attributes, SearchScope.None, 0, 1000);
		//public IEnumerable<DirectoryEntry> Search(string searchBase, string[] attributes, SearchScope scope) => Search(searchBase, null, null, null, attributes, scope, 0, 1000);
		//public IEnumerable<DirectoryEntry> Search(string searchBase, string searchFilter) => Search(searchBase, searchFilter, null, null, null, SearchScope.None, 0, 1000);
		//public IEnumerable<DirectoryEntry> Search(string searchBase, string searchFilter, SearchScope scope) => Search(searchBase, searchFilter, null, null, null, scope, 0, 1000);
		//public IEnumerable<DirectoryEntry> Search(string searchBase, string searchFilter, string[] attributes) => Search(searchBase, searchFilter, null, null, attributes, SearchScope.None, 0, 1000);
		//public IEnumerable<DirectoryEntry> Search(string searchBase, string searchFilter, string[] attributes, SearchScope scope) => Search(searchBase, searchFilter, null, null, attributes, scope, 0, 1000);
		//public IEnumerable<DirectoryEntry> Search(string searchBase, string searchFilter, string userName, string password, string[] attributes, SearchScope scope, int pageSize, int sizeLimit) {
		//	if(!Attributes.Contains("ADsPath"))
		//		Attributes.Add("ADsPath");
		//	if(attributes != null && attributes.Any())
		//		Attributes.AddRange(attributes);

		//	var sBase = searchBase ?? SearchBase;
		//	var filter = searchFilter ?? SearchFilter;
		//	var user = userName ?? UserName;
		//	var pass = password ?? Password;
		//	var searchScope = (scope == SearchScope.None ? (Scope == SearchScope.None ? SearchScope.Base : Scope) : scope);
		//	var page = pageSize != 0 ? pageSize : PageSize;
		//	var size = sizeLimit != 1000 ? sizeLimit : SizeLimit;

		//	IDirectorySearch ds = GetDirectorySearcher(SearchBase);

		//	var options = new List<ADS_SEARCHPREF_INFO>();

		//	if(Scope != SearchScope.None) {
		//		options.Add(new ADS_SEARCHPREF_INFO {
		//			dwSearchPref = ADS_SEARCHPREF.ADS_SEARCHPREF_SEARCH_SCOPE,
		//			dwStatus = ADS_STATUS.ADS_STATUS_NONE,
		//			vValue = new AdsValueHelper(ADS_SCOPE.ADS_SCOPE_SUBTREE, ADSTYPE.ADSTYPE_INTEGER).GetStruct()
		//		});
		//	}

		//	if(page != 0) {
		//		options.Add(new ADS_SEARCHPREF_INFO {
		//			dwSearchPref = ADS_SEARCHPREF.ADS_SEARCHPREF_PAGESIZE,
		//			dwStatus = ADS_STATUS.ADS_STATUS_NONE,
		//			vValue = new AdsValueHelper(page, ADSTYPE.ADSTYPE_INTEGER).GetStruct()
		//		});
		//	}

		//	if(size != 1000) {
		//		options.Add(new ADS_SEARCHPREF_INFO {
		//			dwSearchPref = ADS_SEARCHPREF.ADS_SEARCHPREF_SIZE_LIMIT,
		//			dwStatus = ADS_STATUS.ADS_STATUS_NONE,
		//			vValue = new AdsValueHelper(size, ADSTYPE.ADSTYPE_INTEGER).GetStruct()
		//		});
		//	}

		//	var optSize = Marshal.SizeOf<ADS_SEARCHPREF_INFO>();
		//	var optCount = options.Count;

		//	IntPtr ptrSearchPref = Marshal.AllocHGlobal((IntPtr)(optSize * optCount));
		//	IntPtr placeholder = ptrSearchPref;
		//	for(int i = 0; i < optCount; i++) {
		//		Marshal.StructureToPtr(options[i], placeholder, false);
		//		placeholder = IntPtr.Add(placeholder, optSize);
		//	}

		//	int hr = ds.SetSearchPreference(ptrSearchPref, optCount);

		//	if(Failed(hr))
		//		throw ComException.BuildComException(hr, ds);

		//	placeholder = ptrSearchPref;
		//	options.Clear();
		//	for(int i = 0; i < optCount; i++) {
		//		options.Add(Marshal.PtrToStructure<ADS_SEARCHPREF_INFO>(placeholder));
		//		placeholder = IntPtr.Add(placeholder, optSize);
		//	}

		//	IntPtr searchHandle = IntPtr.Zero;

		//	hr = ds.ExecuteSearch(SearchFilter, Attributes.ToArray(), 1, out searchHandle);

		//	if(Failed(hr))
		//		throw ComException.BuildComException(hr, ds);
		//	var ret = 0;
		//	var lastError = 0;
		//	while((ret = ds.GetNextRow(searchHandle)) != S_ADS_NOMORE_ROWS) {
		//		if(lastError == SIZE_LIMIT_EXCEEDED && ret == SIZE_LIMIT_EXCEEDED)
		//			break;
		//		lastError = ret;
		//		if(ret != SIZE_LIMIT_EXCEEDED) {
		//			DirectoryEntry de = new DirectoryEntry {
		//				CommonName = GetValue(ds, searchHandle, "cn").ToString()
		//			};
		//			yield return de;
		//		}
		//	}
		//}

		public object GetValue(IDirectorySearch search, IntPtr searchHandle, string columnName) {
			IntPtr name = Marshal.StringToCoTaskMemUni(columnName);
			IntPtr column = Marshal.AllocHGlobal(Marshal.SizeOf<ADS_SEARCH_COLUMN>());
			search.GetColumn(searchHandle, name, column);
			Marshal.FreeCoTaskMem(name);
			var col = (ADS_SEARCH_COLUMN)Marshal.PtrToStructure<ADS_SEARCH_COLUMN>(column);
			ADSVALUE value = Marshal.PtrToStructure<ADSVALUE>(col.pADsValues);
			Marshal.FreeHGlobal(column);
			return new AdsValueHelper(value).GetValue();
		}
	}
}
