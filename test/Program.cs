using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Homework.model.Utility;

namespace test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string sql1 = "insert into Users(account, password, name) values('@account', '@password', '@name')";
            string account1 = "qaxx";
            string password1 = "pass";
            string name1 = "name";

            var dbHelper = new SqlDbHelper("default");

            var parameters1 = new SqlParameterBuilder()
                .AddNVarchar("account", 50, account1)
                .AddNVarchar("password", 50, password1)
                .AddNVarchar("name", 50, name1)
                .Build();
            dbHelper.ExecuteNonQuery(sql1, parameters1);
        }
    }
}
