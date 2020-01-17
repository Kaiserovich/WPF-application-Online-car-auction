using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace ver03.DataBase
{
    public class Registr_new_User : Connect
    {
        Autorization check = new Autorization();

        public bool Regist(string login, string password1, string password2)
        {
            if (!check.Check_Login(login))
            {
                if (password1.Equals(password2))
                {
                    if (password1.Length >= 4)
                    {
                        string sql1 = "INSERT INTO `users` (`nick`, `password`) VALUES ('" + login + "', '" + password1.GetHashCode() + "');";
                        MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                        Open();
                        myCommand.ExecuteNonQuery();
                        Close();
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Пороль должен быть дриннее 4-х символов");
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Пороли не совпадают");
                    return false;
                }
            }
            else MessageBox.Show("Данный логин уже используется другим пользователем");
            return false;
        }
        public bool Fill_user_info(int id, string name, string telephone, string email)
        {
            string sql1 = "INSERT INTO users_info (`id`, `name`, `telephone`, `email`) VALUES ('" + id + "', '" + name + "','" + telephone.Replace(" ", "") + "', '" + email + "');";
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
                MessageBox.Show("Error: Ошибка добавления учётных данных");
                return false;
            }



        }
        public bool Create_log(int id)
        {
            string sql1 = "INSERT INTO users_log (`id`, `log_dt`) VALUES ('" + id + "', CURRENT_TIMESTAMP());";
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
                MessageBox.Show("Error: ошибка создания лога");
                return false;
            }
        }
    }
}
