using System.Web.Configuration;

namespace DrugTest.Business_Logic
{
    public static class AppSettings
    {
        public static string GetAppSettings(string sKey)
        {
            string sValue = null;

            // Magic string DrugTest. Url in IIS must have this name.
            var rootWebConfig = WebConfigurationManager.OpenWebConfiguration("/DrugTest");

            // Check if the AppSettings section has items
            if (rootWebConfig.AppSettings.Settings.Count > 0)
            {
                sValue = rootWebConfig.AppSettings.Settings[sKey].Value;
            }
            return sValue;
        }
    }
}