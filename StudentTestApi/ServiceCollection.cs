using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Services;
using DataAccessLayer;
using BusinessLayer;
namespace StudentTestApi
{
    public static class ServiceCollection
    {
        public static void ServicesForDAL(this IServiceCollection services)
        {
            services.AddScoped<IStudentServices, StudentDAL>();
           
        }

    }
}
