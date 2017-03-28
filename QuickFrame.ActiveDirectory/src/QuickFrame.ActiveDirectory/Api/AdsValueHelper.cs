using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace QuickFrame.ActiveDirectory.Api
{
    public class AdsValueHelper
    {
		public ADSVALUE adsvalue;
		private GCHandle pinnedHandle;

		public AdsValueHelper(ADSVALUE adsvalue) {
			this.adsvalue = adsvalue;
		}

		public AdsValueHelper(object managedValue) {
			this.SetValue(managedValue, this.GetAdsTypeForManagedType(managedValue.GetType()));
		}

		public AdsValueHelper(object managedValue, ADSTYPE adsType) {
			this.SetValue(managedValue, adsType);
		}

		~AdsValueHelper() {
			if(this.pinnedHandle.IsAllocated) {
				this.pinnedHandle.Free();
			}
		}

		private ADSTYPE GetAdsTypeForManagedType(Type type) {
			if(type == typeof(int)) {
				return ADSTYPE.ADSTYPE_INTEGER;
			}
			if(type == typeof(long)) {
				return ADSTYPE.ADSTYPE_LARGE_INTEGER;
			}
			if(type == typeof(bool)) {
				return ADSTYPE.ADSTYPE_BOOLEAN;
			}
			return ADSTYPE.ADSTYPE_UNKNOWN;
		}

		public long LowInt64
		{
			get
			{
				return (long)((ulong)this.adsvalue.generic.a + (ulong)((long)this.adsvalue.generic.b << 32));
			}
			set
			{
				this.adsvalue.generic.a = (int)((ulong)value & (ulong)0xFFFFFFFF);
				this.adsvalue.generic.b = (int)(value >> 32);
			}
		}

		private static ushort HighOfInt(int i) {
			return (ushort)(i >> 16 & 65535);
		}

		private static ushort LowOfInt(int i) {
			return (ushort)(i & 65535);
		}

		public ADSVALUE GetStruct() {
			return this.adsvalue;
		}

		public object GetValue() {
			switch(this.adsvalue.dwType) {
				case 0: {
						throw new InvalidOperationException();
					}
				case ADSTYPE.ADSTYPE_DN_STRING:
				case ADSTYPE.ADSTYPE_CASE_EXACT_STRING:
				case ADSTYPE.ADSTYPE_CASE_IGNORE_STRING:
				case ADSTYPE.ADSTYPE_PRINTABLE_STRING:
				case ADSTYPE.ADSTYPE_NUMERIC_STRING:
				case ADSTYPE.ADSTYPE_OBJECT_CLASS: {
						return Marshal.PtrToStringUni(this.adsvalue.pointer);
					}
				case ADSTYPE.ADSTYPE_BOOLEAN: {
						return this.adsvalue.generic.a != 0;
					}
				case ADSTYPE.ADSTYPE_INTEGER: {
						return this.adsvalue.generic.a;
					}
				case ADSTYPE.ADSTYPE_OCTET_STRING:
				case ADSTYPE.ADSTYPE_PROV_SPECIFIC:
				case ADSTYPE.ADSTYPE_NT_SECURITY_DESCRIPTOR: {
						int num = (int)this.adsvalue.octetString.dwLength;
						byte[] numArray = new byte[num];
						Marshal.Copy(this.adsvalue.octetString.lpValue, numArray, 0, num);
						return numArray;
					}
				case ADSTYPE.ADSTYPE_UTC_TIME: {
						SYSTEMTIME systemTime = new SYSTEMTIME {
							wYear = AdsValueHelper.LowOfInt(this.adsvalue.generic.a),
							wMonth = AdsValueHelper.HighOfInt(this.adsvalue.generic.a),
							wDayOfWeek = AdsValueHelper.LowOfInt(this.adsvalue.generic.b),
							wDay = AdsValueHelper.HighOfInt(this.adsvalue.generic.b),
							wHour = AdsValueHelper.LowOfInt(this.adsvalue.generic.c),
							wMinute = AdsValueHelper.HighOfInt(this.adsvalue.generic.c),
							wSecond = AdsValueHelper.LowOfInt(this.adsvalue.generic.d),
							wMilliseconds = AdsValueHelper.HighOfInt(this.adsvalue.generic.d)
						};
						return new DateTime((int)systemTime.wYear, (int)systemTime.wMonth, (int)systemTime.wDay, (int)systemTime.wHour, (int)systemTime.wMinute, (int)systemTime.wSecond, (int)systemTime.wMilliseconds);
					}
				case ADSTYPE.ADSTYPE_LARGE_INTEGER: {
						return this.LowInt64;
					}
				case ADSTYPE.ADSTYPE_CASEIGNORE_LIST:
				case ADSTYPE.ADSTYPE_OCTET_LIST:
				case ADSTYPE.ADSTYPE_PATH:
				case ADSTYPE.ADSTYPE_POSTALADDRESS:
				case ADSTYPE.ADSTYPE_TIMESTAMP:
				case ADSTYPE.ADSTYPE_BACKLINK:
				case ADSTYPE.ADSTYPE_TYPEDNAME:
				case ADSTYPE.ADSTYPE_HOLD:
				case ADSTYPE.ADSTYPE_NETADDRESS:
				case ADSTYPE.ADSTYPE_REPLICAPOINTER:
				case ADSTYPE.ADSTYPE_FAXNUMBER:
				case ADSTYPE.ADSTYPE_EMAIL:
				case ADSTYPE.ADSTYPE_UNKNOWN: {
						return new NotImplementedException();
					}
				//case 27: {
				//		DnWithBinary dnWithBinary = new DnWithBinary();
				//		Marshal.PtrToStructure(this.adsvalue.pointer.@value, dnWithBinary);
				//		byte[] numArray1 = new byte[dnWithBinary.dwLength];
				//		Marshal.Copy(dnWithBinary.lpBinaryValue, numArray1, 0, dnWithBinary.dwLength);
				//		StringBuilder stringBuilder = new StringBuilder();
				//		StringBuilder stringBuilder1 = new StringBuilder();
				//		for(int i = 0; i < (int)numArray1.Length; i++) {
				//			string str = numArray1[i].ToString("X", CultureInfo.InvariantCulture);
				//			if(str.Length == 1) {
				//				stringBuilder1.Append("0");
				//			}
				//			stringBuilder1.Append(str);
				//		}
				//		stringBuilder.Append("B:");
				//		stringBuilder.Append(stringBuilder1.Length);
				//		stringBuilder.Append(":");
				//		stringBuilder.Append(stringBuilder1.ToString());
				//		stringBuilder.Append(":");
				//		stringBuilder.Append(Marshal.PtrToStringUni(dnWithBinary.pszDNString));
				//		return stringBuilder.ToString();
				//	}
				//case 28: {
				//		DnWithString dnWithString = new DnWithString();
				//		Marshal.PtrToStructure(this.adsvalue.pointer.@value, dnWithString);
				//		string stringUni = Marshal.PtrToStringUni(dnWithString.pszStringValue) ?? "";
				//		StringBuilder stringBuilder2 = new StringBuilder();
				//		stringBuilder2.Append("S:");
				//		stringBuilder2.Append(stringUni.Length);
				//		stringBuilder2.Append(":");
				//		stringBuilder2.Append(stringUni);
				//		stringBuilder2.Append(":");
				//		stringBuilder2.Append(Marshal.PtrToStringUni(dnWithString.pszDNString));
				//		return stringBuilder2.ToString();
				//	}
			}
			return new ArgumentException();
		}

		private void SetValue(object managedValue, ADSTYPE adstype) {
			object[] objArray;
			this.adsvalue = new ADSVALUE() {
				dwType = adstype
			};
			switch(adstype) {
				case ADSTYPE.ADSTYPE_CASE_IGNORE_STRING: {
						this.pinnedHandle = GCHandle.Alloc(managedValue, GCHandleType.Pinned);
						this.adsvalue.pointer = this.pinnedHandle.AddrOfPinnedObject();
						return;
					}
				case ADSTYPE.ADSTYPE_PRINTABLE_STRING:
				case ADSTYPE.ADSTYPE_NUMERIC_STRING:
				case ADSTYPE.ADSTYPE_OCTET_STRING:
				case ADSTYPE.ADSTYPE_UTC_TIME: {
						objArray = new object[] { string.Concat("0x", Convert.ToString((int)adstype, 16)) };
						throw new NotImplementedException();
					}
				case ADSTYPE.ADSTYPE_BOOLEAN: {
						if((bool)managedValue) {
							this.LowInt64 = (long)-1;
							return;
						}
						this.LowInt64 = (long)0;
						return;
					}
				case ADSTYPE.ADSTYPE_INTEGER: {
						this.adsvalue.generic.a = (int)managedValue;
						this.adsvalue.generic.b = 0;
						return;
					}
				case ADSTYPE.ADSTYPE_LARGE_INTEGER: {
						this.LowInt64 = (long)managedValue;
						return;
					}
				case ADSTYPE.ADSTYPE_PROV_SPECIFIC: {
						byte[] numArray = (byte[])managedValue;
						this.adsvalue.octetString.dwLength = (uint)numArray.Length;
						this.pinnedHandle = GCHandle.Alloc(numArray, GCHandleType.Pinned);
						this.adsvalue.octetString.lpValue = this.pinnedHandle.AddrOfPinnedObject();
						return;
					}
				default: {
						objArray = new object[] { string.Concat("0x", Convert.ToString((int)adstype, 16)) };
						throw new NotImplementedException();
					}
			}
		}
	}
}
