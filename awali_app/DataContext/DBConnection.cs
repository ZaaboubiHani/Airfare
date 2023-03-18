using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airfare.DataContext
{
    public static class DBConnection
    {
        public static SqlConnection getConnection()
        {
            return new SqlConnection($@"server = .\SQLEXPRESS; Initial Catalog = AirfareDB;Integrated Security = true ");
        }
    }
}
