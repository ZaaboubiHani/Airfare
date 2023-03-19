using Airfare.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airfare.DataContext
{
    public static class Configuration
    {
        public static SeasonModel CurrentSeason { get; set; }
        public static UserModel CurrentUser { get; set; }
        public static string Server { get; set; }
    }
}
