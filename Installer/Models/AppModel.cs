using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Installer.Models
{
    public class AppModel
    {
        public string DisplayName { get; set; }
        public string InstallationLocation { get; set; }
        public string AppVersion { get; set; }
        public string Id { get; set; }
    }
}
