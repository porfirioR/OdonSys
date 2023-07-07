using Host.Api.Middleware;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json.Converters;
using Utilities.Configurations;

namespace Host.Api
{
    public partial class Startup
    {
        public IConfiguration Configuration { get; }
        public MainConfiguration MainConfiguration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            AddConfigurations(services);

            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => builder
                                              .AllowAnyOrigin()
                                              .AllowAnyHeader()
                                              .AllowAnyMethod());
            });

            // partial startup
            ConfigureMappings(services);

            InjectServices(services);
            ConfigureAuthentication(services, Configuration);

            ConfigureAuthorization(services);
            // Configura all Authorization Handlers
            ConfigureAuthorizationHandlers(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseSwagger();

            app.UseSwaggerUI();

            app.UseExceptionHandler("/error");
            app.UseHsts();

            app.UseRouting();

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
