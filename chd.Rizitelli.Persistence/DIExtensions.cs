using chd.Rizitelli.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Rizitelli.Persistence.Data
{
  public static class DIExtensions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
            {
                System.AppContext.SetSwitch("Microsoft.EntityFrameworkCore.Issue31751", true);
                options.UseModel(DataContextModel.Instance);
                options.UseSqlite(configuration.GetConnectionString(nameof(DataContext)));
            });
            return services;
        }
    }
}
