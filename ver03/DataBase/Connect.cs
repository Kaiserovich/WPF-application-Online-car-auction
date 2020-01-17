using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ver03;
using System.Windows.Forms;

namespace ver03.DataBase
{
    public class Connect
    {
        string conStr = "Database=sql7240142; Data Source=sql7.freesqldatabase.com; User Id=sql7240142; Password=8BsukyFblI; charset=utf8";
        public bool status = false;

        public MySqlConnection conn;

        public Connect()
        {
            conn = new MySqlConnection(conStr);
        }
        public void Open()
        {
            if (conn.State != System.Data.ConnectionState.Open)
                try
            {
                conn.Open();
            }
            catch
            {
                //MessageBox.Show(@"Ошибка соединения с сервером");
            }
            if (conn.State.ToString() == "Open")
            {
                status = true;
                //MessageBox.Show("Open");
            }
            else
            {
                MessageBox.Show(@"Please check connection string");
            }
        }
        public void Close()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
                status = false;
                //MessageBox.Show("Close");
            }
        }

    }
}
