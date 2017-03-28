using QuickFrame.Data.Common.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickFrame.Security.Common.Interfaces
{
    public interface ISiteClaim : IDataModel
    {
		byte[] Claim { get; set; }
    }
}
