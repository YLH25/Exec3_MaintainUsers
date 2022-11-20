using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Homework.model.Utility;
using static Homework.model.Utility.SqlDbHelper;

namespace Exec3_MaintainUsers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SelectUsers();
        }
        public static void InsertUser(string account, string password, string name, int height, DateTime dateTime)
        {
            string sql = "insert into Users(account, password, name,Height,Birthday) values(@account, @password, @name,@Height,@Birthday)";
            var parameters = new SqlParameterBuilder()
                .AddNVarchar("account", 50, account)
                .AddNVarchar("password", 50, password)
                .AddNVarchar("name", 50, name)
                .AddNInt("Height", height)
                .AddNDateTime("Birthday", dateTime)
                .Build();
            new SqlDbHelper("default").ExecuteNonQuery(sql, parameters);
        }

        public static void DeleteUser(int num)
        {
            string sql = "delete from Users where id=@id";
            var parameters = new SqlParameterBuilder()
                .AddNInt("id", num)
                .Build();
            new SqlDbHelper("default").ExecuteNonQuery(sql, parameters);
        }

        public static void SelectUsers()
        {
            string sql = "SELECT *FROM Users1";
            DataTable users = new SqlDbHelper("default").Select(sql, null);
            foreach (DataRow row in users.Rows)
            {
                int id = row.Field<int>("id");
                string account = row.Field<string>("Account");
                string password = row.Field<string>("password");
                string name = row.Field<string>("name");
                int height = row.Field<int>("height");
                DateTime birthday = row.Field<DateTime>("birthday");

                Console.WriteLine($"{id}{account}{password}{name}{height}{birthday}");
            }
        }
        public static void UpdateUsers(int id, string account, string password, string name, int height, DateTime dateTime)
        {
            string sql = "update users1 set Account=@Account,Password=@Password,Name=@Name Height=@Height,Birthday=@Birthday  where Id=@Id";
            var parameters = new SqlParameterBuilder()
           .AddNInt("id", id)
           .AddNVarchar("account", 50, account)
           .AddNVarchar("password", 50, password)
           .AddNVarchar("name", 50, name)
           .AddNInt("Height", height)
           .AddNDateTime("Birthday", dateTime)

           .Build();
            new SqlDbHelper("default").ExecuteNonQuery(sql, parameters);
        }
    }
}
