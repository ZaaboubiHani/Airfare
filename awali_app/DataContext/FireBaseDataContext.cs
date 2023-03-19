using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Airfare.DataContext
{
    internal class FireBaseDataContext
    {
        string path = AppDomain.CurrentDomain.BaseDirectory + @"airfare.json";
        public FirestoreDb Database { get; }
        public FireBaseDataContext()
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            Database = FirestoreDb.Create("airfare-3be92");
        }
    }
}
