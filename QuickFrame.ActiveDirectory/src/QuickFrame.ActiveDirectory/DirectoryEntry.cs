using QuickFrame.ActiveDirectory.Api;
using QuickFrame.ActiveDirectory.Api.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading.Tasks;
using static QuickFrame.ActiveDirectory.Api.Activeds;

namespace QuickFrame.ActiveDirectory
{
	public class DirectoryEntry : IDictionary<string, object> {
		private Dictionary<string, object> _properties = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
		private List<DirectoryEntry> _children = new List<DirectoryEntry>();
		public string Email { get { return _properties.ContainsKey("email") ? _properties["email"]?.ToString() : String.Empty; } }
		public SecurityIdentifier Sid { get { return _properties.ContainsKey("objectSid") ? new SecurityIdentifier((byte[])_properties["objectSid"], 0) : null; } }
		public string PhoneNumber { get { return _properties.ContainsKey("telephoneNumber") ? _properties["telephoneNumber"]?.ToString() : String.Empty; } }
		public string UserName { get { return _properties.ContainsKey("sAMAccountName") ?  _properties["sAMAccountName"]?.ToString() : String.Empty; } }
		public List<string> Department { get { return _properties.ContainsKey("department") ? (_properties["department"] is List<string>) ? (List<string>)_properties["department"] : new List<string> { _properties["department"].ToString() } : null; } }
		public string DisplayName { get { return _properties.ContainsKey("displayName") ? _properties["displayName"]?.ToString() : String.Empty; } }
		public string FirstName { get { return _properties.ContainsKey("givenName") ?  _properties["givenName"]?.ToString() : String.Empty; } }
		public string LastName { get { return _properties.ContainsKey("sn") ?  _properties["sn"]?.ToString() : String.Empty; } }
		public string DistinguishedName { get { return _properties.ContainsKey("distinguishedName") ?  _properties["distinguishedName"]?.ToString() : String.Empty; } set { _properties["distinguishedName"] = value; } }
		public ICollection<string> Keys {  get { return _properties.Keys; } }
		public ICollection<object> Values {  get { return _properties.Values; } }
		public List<DirectoryEntry> Children { get { return _children; } }
		public int Count {  get { return _properties.Count; } }

		public bool IsReadOnly { get { return false; } }

		object IDictionary<string, object>.this[string key]
		{
			get
			{
				if(_properties.ContainsKey(key))
					return _properties[key];
				return null;
			}
			set
			{
				_properties[key] = value;
			}
		}

		public object this[string key] { get { return _properties[key]; } set { _properties[key] = value; } }
		public void Add(string key, object value) {
			_properties.Add(key, value);
		}

		public bool ContainsKey(string key) {
			return _properties.ContainsKey(key);
		}

		public bool Remove(string key) {
			return _properties.Remove(key);
		}

		public bool TryGetValue(string key, out object value) {
			return _properties.TryGetValue(key, out value);
		}

		public void Add(KeyValuePair<string, object> item) {
			_properties.Add(item.Key, item.Value);
		}

		public void Clear() {
			_properties.Clear();
		}

		public bool Contains(KeyValuePair<string, object> item) {
			return _properties.Contains(item);
		}

		public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex) {
			throw new NotImplementedException();
		}

		public bool Remove(KeyValuePair<string, object> item) {
			return _properties.Remove(item.Key);
		}

		public IEnumerator<KeyValuePair<string, object>> GetEnumerator() {
			return _properties.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return this.GetEnumerator();
		}

		public DirectoryEntry(string dn) {
			_properties["distinguishedName"] = dn;
		}

		public void Find(string filter) => Find(filter, null, SearchScope.Base, 0, 1000);
		public void Find(string[] properties) => Find(null, properties, SearchScope.Base, 0, 1000);
		public void Find(string filter, string[] properties) => Find(filter, properties, SearchScope.Base, 0, 1000);
		public void Find(string filter, string[] properties, SearchScope scope) => Find(filter, properties, scope, 0, 1000);
		public void Find(string filter, string[] properties, int pageSize) => Find(filter, properties, SearchScope.Base, pageSize, 1000);
		public void Find(string filter, string[] properties, SearchScope scope, int pageSize, int size) {
			var adProperties = new List<string>(properties);
			if(!adProperties.Contains("ADsPath"))
				adProperties.Add("ADsPath");
			if(!adProperties.Contains("distinguishedName"))
				adProperties.Add("distinguishedName");

			IDirectorySearch ds = GetDirectorySearcher($"LDAP://{_properties["distinguishedName"].ToString()}");

			var options = new List<ADS_SEARCHPREF_INFO>();

			if(scope != SearchScope.None) {
				options.Add(new ADS_SEARCHPREF_INFO {
					dwSearchPref = ADS_SEARCHPREF.ADS_SEARCHPREF_SEARCH_SCOPE,
					dwStatus = ADS_STATUS.ADS_STATUS_NONE,
					vValue = new AdsValueHelper(ADS_SCOPE.ADS_SCOPE_SUBTREE, ADSTYPE.ADSTYPE_INTEGER).GetStruct()
				});
			}

			if(pageSize != 0) {
				options.Add(new ADS_SEARCHPREF_INFO {
					dwSearchPref = ADS_SEARCHPREF.ADS_SEARCHPREF_PAGESIZE,
					dwStatus = ADS_STATUS.ADS_STATUS_NONE,
					vValue = new AdsValueHelper(pageSize, ADSTYPE.ADSTYPE_INTEGER).GetStruct()
				});
			}

			if(size != 1000) {
				options.Add(new ADS_SEARCHPREF_INFO {
					dwSearchPref = ADS_SEARCHPREF.ADS_SEARCHPREF_SIZE_LIMIT,
					dwStatus = ADS_STATUS.ADS_STATUS_NONE,
					vValue = new AdsValueHelper(size, ADSTYPE.ADSTYPE_INTEGER).GetStruct()
				});
			}

			var optSize = Marshal.SizeOf<ADS_SEARCHPREF_INFO>();
			var optCount = options.Count;

			IntPtr ptrSearchPref = Marshal.AllocHGlobal((IntPtr)(optSize * optCount));
			IntPtr placeholder = ptrSearchPref;
			for(int i = 0; i < optCount; i++) {
				Marshal.StructureToPtr(options[i], placeholder, false);
				placeholder = IntPtr.Add(placeholder, optSize);
			}

			int hr = ds.SetSearchPreference(ptrSearchPref, optCount);

			if(Failed(hr)) {
				Marshal.FreeHGlobal(ptrSearchPref);
				throw ComException.BuildComException(hr, ds);
			}

			placeholder = ptrSearchPref;
			options.Clear();
			for(int i = 0; i < optCount; i++) {
				var opt = Marshal.PtrToStructure<ADS_SEARCHPREF_INFO>(placeholder);
				placeholder = IntPtr.Add(placeholder, optSize);
				if(opt.dwStatus == ADS_STATUS.ADS_STATUS_INVALID_SEARCHPREF) {
					Marshal.FreeHGlobal(ptrSearchPref);
					throw new Exception("Invalid search preference specified.");
				}
				if(opt.dwStatus == ADS_STATUS.ADS_STATUS_INVALID_SEARCHPREFVALUE) {
					Marshal.FreeHGlobal(ptrSearchPref);
					throw new Exception("Invalid search preference value specified.");
				}
			}

			Marshal.FreeHGlobal(ptrSearchPref);

			IntPtr searchHandle = IntPtr.Zero;

			hr = ds.ExecuteSearch(filter, adProperties.ToArray(), adProperties.Count, out searchHandle);

			if(Failed(hr))
				throw ComException.BuildComException(hr, ds);

			var ret = 0;
			var lastError = 0;
			while((ret = ds.GetNextRow(searchHandle)) != S_ADS_NOMORE_ROWS) {
				if(lastError == SIZE_LIMIT_EXCEEDED && ret == SIZE_LIMIT_EXCEEDED)
					break;
				lastError = ret;
				if(ret != SIZE_LIMIT_EXCEEDED) {
					DirectoryEntry de = new DirectoryEntry(GetValueString(ds, searchHandle, "distinguishedName"));
					foreach(var obj in adProperties)
						de[obj] = GetValue(ds, searchHandle, obj);
					Children.Add(de);
				}
			}
		}

		private object GetValue(IDirectorySearch search, IntPtr searchHandle, string columnName) {
			try {
				IntPtr name = Marshal.StringToCoTaskMemUni(columnName);
				IntPtr column = Marshal.AllocHGlobal(Marshal.SizeOf<ADS_SEARCH_COLUMN>());
				search.GetColumn(searchHandle, name, column);
				Marshal.FreeCoTaskMem(name);
				var col = (ADS_SEARCH_COLUMN)Marshal.PtrToStructure<ADS_SEARCH_COLUMN>(column);
				ADSVALUE value = Marshal.PtrToStructure<ADSVALUE>(col.pADsValues);
				Marshal.FreeHGlobal(column);
				return new AdsValueHelper(value).GetValue();
			} catch { }
			return null;
		}
		private string GetValueString(IDirectorySearch search, IntPtr searchHandle, string columnName) {
			IntPtr name = Marshal.StringToCoTaskMemUni(columnName);
			IntPtr column = Marshal.AllocHGlobal(Marshal.SizeOf<ADS_SEARCH_COLUMN>());
			search.GetColumn(searchHandle, name, column);
			Marshal.FreeCoTaskMem(name);
			var col = (ADS_SEARCH_COLUMN)Marshal.PtrToStructure<ADS_SEARCH_COLUMN>(column);
			ADSVALUE value = Marshal.PtrToStructure<ADSVALUE>(col.pADsValues);
			Marshal.FreeHGlobal(column);
			return new AdsValueHelper(value).GetValue().ToString();
		}

		private byte[] GetValueByteArray(IDirectorySearch search, IntPtr searchHandle, string columnName) {
			IntPtr name = Marshal.StringToCoTaskMemUni(columnName);
			IntPtr column = Marshal.AllocHGlobal(Marshal.SizeOf<ADS_SEARCH_COLUMN>());
			search.GetColumn(searchHandle, name, column);
			Marshal.FreeCoTaskMem(name);
			var col = (ADS_SEARCH_COLUMN)Marshal.PtrToStructure<ADS_SEARCH_COLUMN>(column);
			ADSVALUE value = Marshal.PtrToStructure<ADSVALUE>(col.pADsValues);
			Marshal.FreeHGlobal(column);
			return ((byte[])new AdsValueHelper(value).GetValue());
		}

		private List<string> GetValues(IDirectorySearch search, IntPtr searchHandle, string columnName) {
			IntPtr name = Marshal.StringToCoTaskMemUni(columnName);
			IntPtr column = Marshal.AllocHGlobal(Marshal.SizeOf<ADS_SEARCH_COLUMN>());
			search.GetColumn(searchHandle, name, column);
			Marshal.FreeCoTaskMem(name);
			var col = Marshal.PtrToStructure<ADS_SEARCH_COLUMN>(column);
			IntPtr placeholder = col.pADsValues;
			var retVal = new List<string>();
			for(int i = 0; i < col.dwNumValues; i++) {
				ADSVALUE value = Marshal.PtrToStructure<ADSVALUE>(placeholder);
				retVal.Add(Marshal.PtrToStringUni(value.pointer));
				placeholder = IntPtr.Add(placeholder, Marshal.SizeOf<ADSVALUE>());
			}
			Marshal.FreeHGlobal(column);
			return retVal;
		}
	}
}
