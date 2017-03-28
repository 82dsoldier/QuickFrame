using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using QuickFrame.Data.Common;
using QuickFrame.Data.Common.Interfaces;
using QuickFrame.Security.Common;
using QuickFrame.Security.Common.Interfaces;
using QuickFrame.Security.Common.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickFrame.Security.ActiveDirectory
{
    public static class ServiceExtensions
    {
		public static IServiceCollection AddAdSecurity(this IServiceCollection services) {
			services.AddEntityFramework()
				.AddDbContext<SecurityContext>(ServiceLifetime.Singleton);
			services.AddScoped<ISecurityContext>(provider => provider.GetService<SecurityContext>());
			services.AddTransient<ILookupNormalizer, LookupNormalizer>()
				.AddTransient<IUserStore<ISiteUser>, ActiveDirectoryUserStore>();

			return services;
		}
    }
}
