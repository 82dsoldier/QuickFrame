using QuickFrame.Data.Common.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickFrame.Security.Common.Interfaces
{
    public interface ISiteUser : IDataModel
    {
		byte[] Sid { get; set; }
		string DisplayName { get; set; }
		string FirstName { get; set; }
		string LastName { get; set; }
		string Email { get; set; }
		string Phone { get; set; }
		string UserName { get; set; }
    }
}
