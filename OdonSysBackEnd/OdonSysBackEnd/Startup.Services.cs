﻿using Access.Admin;
using Microsoft.Extensions.DependencyInjection;

namespace OdonSysBackEnd
{
    public partial class Startup
    {
        public void InjectServices(IServiceCollection services)
        {
            ServiceInjection.ConfigureServices(services);
            Manager.Admin.ServiceInjection.ConfigureServices(services);
        }
    }
}
