using DrugTest.Business_Logic;
using Hangfire;

namespace DrugTest.Hangfire
{
    public static class HangfireJobs
    {
        public static void StartJobs()
        {
            // Add job schedule based on Cron expression
            RecurringJob.AddOrUpdate(() => new GenerateBatch().Generate(), Cron.MonthInterval(3));
        }
    }
}