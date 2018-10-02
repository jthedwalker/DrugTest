using static DrugTest.Hangfire.Bootstrapper;

namespace DrugTest.Hangfire
{
    // This namespace and class name cannot change. The serviceAutoStartProviders element 
    // in the applicationHost.config file depends on this class.
    public class ApplicationPreload : System.Web.Hosting.IProcessHostPreloadClient
    {
        public void Preload(string[] parameters)
        {
            HangfireBootstrapper.Instance.Start();
        }
    }
}
