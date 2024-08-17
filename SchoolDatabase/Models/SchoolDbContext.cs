﻿using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System;

namespace SchoolDatabase.Models
{
    public class SchoolDbContext
    {
        private static string User { get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Database { get { return "schooldb"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

        protected static string ConnectionString 
        {
            get
            {
                return "server=" + Server
                    + "; user=" + User
                    + "; database=" + Database
                    + "; port=" + Port
                    + "; password=" + Password
                    + "; convert zero datetime=True";
            }
        }

        public object Teacher { get; internal set; }

        public MySqlConnection AccessDatabase()
        {
            return new MySqlConnection(ConnectionString);
        }

        internal void SaveChanges()
        {
            throw new NotImplementedException();
        }

        internal object Entry(Teacher teacher)
        {
            throw new NotImplementedException();
        }
    }
}
