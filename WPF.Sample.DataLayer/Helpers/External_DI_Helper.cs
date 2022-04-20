using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ssn_AzureKeyVault.Extensions;

// 04/14/2022 10:36 pm - SSN

namespace WPF.Sample.DataLayer.Helpers
{
    public static class External_DI_Helper
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.Addssn_AzureKeyVaultServices();

            return services;

        }

    }
}
