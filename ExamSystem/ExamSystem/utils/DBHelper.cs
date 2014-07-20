using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace ExamSystem.utils {
    public class DBHelper {
        private static readonly String connStr = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;

        public bool checkConnection() {
            MySqlConnection conn = new MySqlConnection(connStr);

            bool flag = true;
            try {
                conn.Open();
            } catch (Exception) {
                flag = false;
            } finally {
                conn.Close();
            }

            return flag;
        }


    }
}
