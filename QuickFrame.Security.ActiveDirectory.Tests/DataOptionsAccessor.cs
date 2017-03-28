using Microsoft.Extensions.Options;
using QuickFrame.Data.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickFrame.Security.ActiveDirectory.Tests
{
	public class DataOptionsAccessor : IOptions<DataOptions> {
		public DataOptions Value
		{
			get
			{
				return new DataOptions();
			}
		}
	}
}
