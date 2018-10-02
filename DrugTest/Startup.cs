using DrugTest.Hangfire;
using Hangfire;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(DrugTest.Startup))]

namespace DrugTest
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseHangfireDashboard("/admin", new DashboardOptions
            {
                Authorization = new[] { new MyAuthorizationFilter() }
            });

            HangfireJobs.StartJobs();
        }
    }
}
