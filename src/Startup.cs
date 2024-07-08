using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CreditCardPaymentApi
{
    public class CreditCardPaymentsApi
    {
        public IConfiguration Configuration { get; }

        public CreditCardPaymentsApi(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Register DbContext
            services.AddDbContext<CreditLineDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            // Register other services as needed
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Production error handler
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            // HTTPS redirection
            app.UseHttpsRedirection();

            // Routing
            app.UseRouting();

            // Authentication and Authorization (if needed)
            // app.UseAuthentication();
            // app.UseAuthorization();

            // Endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
