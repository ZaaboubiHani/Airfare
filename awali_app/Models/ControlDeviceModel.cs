using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.Models
{
    public class ControlDeviceModel
    {
        public string Id { get; set; }
        public string DeviceId { get; set; }
        public string MachineName { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
