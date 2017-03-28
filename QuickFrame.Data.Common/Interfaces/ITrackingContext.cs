using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickFrame.Data.Common.Interfaces
{
    public interface ITrackingContext
    {
		bool TrackChanges { get; set; }
		bool Notify { get; set; }
    }
}
