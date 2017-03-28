using QuickFrame.Data.Common.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickFrame.Security.Common.Interfaces
{
    public interface IUserClaim<TUserKey> : IDataModelCore
    {
		TUserKey UserId { get; set; }
		int ClaimId { get; set; }
    }
}
