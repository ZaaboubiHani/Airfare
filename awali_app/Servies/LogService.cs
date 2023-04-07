using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.Servies
{
    static class LogService
    {
        static public async Task LogError(string errorMessage,object section)
        {
            var folder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Logs\\";
            Directory.CreateDirectory(folder);
            string fileName = "Error Log "+DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss")+" .txt";
            string fullPath = folder + fileName;
            
            await File.WriteAllTextAsync(fullPath, section.GetType() + " Section : " + errorMessage);
        }
    }
}
