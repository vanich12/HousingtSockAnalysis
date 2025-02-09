using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using House.Common.EntityModels.PostgreSQL.Packt.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Packt.Shared
{
    public static class HouseContextExtensions
    {

        public static IServiceCollection AddHouseDBContext(
            this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<HouseDbContext>(options =>
                options.UseNpgsql(connectionString)
            );
            return services;
        }
    }
}
