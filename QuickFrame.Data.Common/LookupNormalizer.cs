using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using QuickFrame.Data.Common.Interfaces;

namespace QuickFrame.Data.Common
{
	public class LookupNormalizer : ILookupNormalizer {
		public string Normalize(string key) {
			return key.ToString();
		}
	}
}
