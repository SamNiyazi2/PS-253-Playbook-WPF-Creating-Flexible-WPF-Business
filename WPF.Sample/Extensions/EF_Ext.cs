using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Sample.DataLayer;
using WPF.Sample.UserControls;
using WPF.Sample.ViewModelLayer;

namespace WPF.Sample.Extensions
{
    // 04/15/2022 02:16 am - SSN 
    public static class EF_Ext
    {
        public static IServiceCollection AddDbContext<TContext>(this IServiceCollection serviceCollection) where TContext : DbContext
        {
            return serviceCollection.AddScoped<SampleDbContext, SampleDbContext>();
        }


    }
}
