using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace ver03.DataBase
{
    class Autorization : Connect
    {
        public bool Check_Login(string nick)
        {
            Open();
            if (status)
            {
                string sql1 = "select nick from users where nick='" + nick + "';";
                bool find_user = false;

                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                while (reader.Read())
                {
                    if (nick == reader[0].ToString())
                    {
                        find_user = true;
                        break;
                    }
                }
                base.Close();
                reader.Close();
                if (!find_user)
                {
                    return false;
                }
                else return true;
            }
            else
            {
                MessageBox.Show("Проверьте соединение с сервером");
                base.Close();
                return false;
            }
        }
        public bool Check_Password(string nick, string password)
        {
            Open();
            if (status)
            {
                string sql1 = "select nick, password from users where nick='" + nick + "';";
                bool pass_valide = false;

                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                while (reader.Read())
                {
                    if (password.GetHashCode().ToString() == reader[1].ToString())
                    {
                        // MessageBox.Show("Добро пожаловать " + nick);
                        pass_valide = true;
                        break;
                    }
                }
                Close();
                reader.Close();
                if (!pass_valide)
                {
                    MessageBox.Show("Пороль не верный");
                    return false;
                }
                else  return true; 
            }
            else
            {
                MessageBox.Show("Проверьте соединение с сервером");
                base.Close();
                return false;
            }

        }
        public int Get_user_id(string nick)
        {
            Open();
            if (status)
            {
                string sql1 = "select id from users where nick='" + nick + "';";
                int id;
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                if (reader.Read())
                {
                    id = Convert.ToInt32(reader[0].ToString());
                }
                else id = -1;
                base.Close();
                reader.Close();

                if (id == -1)
                    return -1;
                else return id;

            }
            else
            {
                MessageBox.Show("Проверьте соединение с сервером");
                base.Close();
                return -1;
            }
        }
        public bool Update_log(int id)
        {
            string sql1 = "UPDATE `users_log` SET `log_dt`=CURRENT_TIMESTAMP WHERE `id`='" + id + "';";

            try
            {
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                Open();
                myCommand.ExecuteNonQuery();
                Close();
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка добавления обновления лога");
                return false;
            }
        }
        
    }
}
