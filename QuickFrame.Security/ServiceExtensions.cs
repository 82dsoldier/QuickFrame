using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuickFrame.Data.Common.Interfaces;

namespace QuickFrame.Security {
	public static class ServiceExtensions {
		public static IServiceCollection AddSecurity(this IServiceCollection services, IConfigurationRoot configuration) {
			return services;
		}
	}
}