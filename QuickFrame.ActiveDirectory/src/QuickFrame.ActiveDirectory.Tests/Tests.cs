using QuickFrame.ActiveDirectory;
using QuickFrame.ActiveDirectory.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests
{
    public class Tests
    {
        [Fact]
		[MTAThread]
        public void Test1() 
        {
			DirectoryEntry de = new DirectoryEntry("DC=DEAC,DC=PAD,DC=LOCAL");
			de.Find("(&(objectClass=user)(displayName=B*))", new string[] { "cn", "displayName", "mail", "telephoneNumber", "objectSid", "sAMAccountName", "givenName", "sn", "department" }, SearchScope.Subtree, 1000, 0);
			foreach(var obj in de.Children)
				Console.WriteLine(obj.DisplayName);
			//DirectorySearcher searcher = new DirectorySearcher("LDAP://DC=DEAC,DC=PAD,DC=LOCAL", "(objectClass=user)", null, null, new string[] { "cn", "email", "ADsPath" }, QuickFrame.ActiveDirectory.Api.SearchScope.Subtree, 0, 1000);
			//IEnumerable<DirectoryEntry> deList = searcher.Search();
			//var x = deList.Count();
        }
    }
}
