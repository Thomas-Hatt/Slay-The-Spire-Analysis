using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpireAnalytics
{
    static class ClsWebGlobal
    {
        public static string DatabaseConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../Bin/Runs.accdb") + ";Persist Security Info=False;";
    }
}
