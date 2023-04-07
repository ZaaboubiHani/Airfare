using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.Servies
{
    public class ConfigurationServices
    {
        public async Task SetValue(string key, string value)
        {
            await Task.Run(() =>
            {
                try
                {
                    System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    var setting = config.AppSettings.Settings;
                    if (setting[key] == null)
                    {
                        setting.Add(key, value);
                    }
                    else
                    {
                        setting[key].Value = value;
                    }
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
                }
                catch (ConfigurationErrorsException e)
                {

                    LogService.LogError(e.Message, this);
                    Console.WriteLine("Error writing app settings");
                }
            });
        }

        public async Task<string> GetValue(string key)
        {
            string result = string.Empty;
            await Task.Run(async () =>
            {
                try
                {
                    var appSettings = ConfigurationManager.AppSettings;
                    result = appSettings[key] ?? "Not Found";
                    if (result == "Not Found")
                    {
                        await SetValue("DatabaseName", "AwaliApp");
                    }
                    Console.WriteLine(result);
                }
                catch (ConfigurationErrorsException e)
                {

                    LogService.LogError(e.Message, this);
                    Console.WriteLine("Error reading app settings");
                }
            });
            return result;
        }
    }
}
