using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickFrame.Data.Common.Interfaces.Models
{
    public interface IDataModelDeletable
    {
		bool IsDeleted { get; set; }
    }
}
