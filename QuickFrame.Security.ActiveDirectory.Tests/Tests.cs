#define TESTING

using Microsoft.Extensions.Options;
using QuickFrame.Data.Configuration;
using QuickFrame.Security.ActiveDirectory;
using QuickFrame.Security.ActiveDirectory.Tests;
using QuickFrame.Security.Common.Interfaces;
using System;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
	public class Tests
    {
        [Fact]
        public void DirectoryEntryCreation() 
        {
			DirectoryEntry de = new DirectoryEntry("CN=Burchett\\, Drew,OU=FPDP,OU=Employees,OU=Users,OU=DEAC,DC=DEAC,DC=PAD,DC=LOCAL");
			Assert.True(de != null);
			Assert.True(de != default(DirectoryEntry));
			Assert.True(de.FirstName == "Drew");
			Assert.True(de.LastName == "Burchett");
			Assert.True(de.Email.Equals("Drew.Burchett@ffspaducah.com", StringComparison.CurrentCultureIgnoreCase));
        }

		[Fact]
		public void FindById() {
			DirectoryEntry de = DirectoryEntry.FindById("LDAP://DC=DEAC,DC=PAD,DC=LOCAL", new SecurityIdentifier("S-1-5-21-2390488714-2608659811-2265349600-4828"));
			Assert.True(de != null);
			Assert.True(de != default(DirectoryEntry));
			Assert.True(de.FirstName == "Drew");
			Assert.True(de.LastName == "Burchett");
			Assert.True(de.Email.Equals("Drew.Burchett@ffspaducah.com", StringComparison.CurrentCultureIgnoreCase));
		}

		[Fact]
		public void FindByIdAsync() {
			var accessor = new DataOptionsAccessor();
			var adStore = new ActiveDirectoryUserStore(accessor);
			Task<ISiteUser> deTask = adStore.FindByIdAsync("S-1-5-21-2390488714-2608659811-2265349600-4828", new CancellationToken());
			deTask.Wait();
			DirectoryEntry de = deTask.Result as DirectoryEntry;
			Assert.True(de != null);
			Assert.True(de != default(DirectoryEntry));
			Assert.True(de.FirstName == "Drew");
			Assert.True(de.LastName == "Burchett");
			Assert.True(de.Email.Equals("Drew.Burchett@ffspaducah.com", StringComparison.CurrentCultureIgnoreCase));

		}

		[Fact]
		public void FindByName() {
			var accessor = new DataOptionsAccessor();
			var adStore = new ActiveDirectoryUserStore(accessor);
			Task<ISiteUser> deTask = adStore.FindByNameAsync(@"DREW.BURCHETT", new CancellationToken());
			deTask.Wait();
			DirectoryEntry de = deTask.Result as DirectoryEntry;
			Assert.True(de != null);
			Assert.True(de != default(DirectoryEntry));
			Assert.True(de.FirstName == "Drew");
			Assert.True(de.LastName == "Burchett");
			Assert.True(de.Email.Equals("Drew.Burchett@ffspaducah.com", StringComparison.CurrentCultureIgnoreCase));
		}
	}
}
