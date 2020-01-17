using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ver03.FileConstructions.Administartion;
using System.Windows;
using MySql.Data.MySqlClient;

namespace ver03.DataBase.Administration
{
    public class DBLoadAdmin : ver03.DataBase.DBLoad
    {
        public UserInfo GetUsersInfoById(int id)
        {
            UserInfo user_inf = null;
            Open();
            if (status)
            {
                string sql1 = "SELECT * from users_info where id = "+id.ToString()+";";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                if (reader.Read())
                    user_inf = (new UserInfo(Convert.ToInt32(reader[0].ToString()), reader[1].ToString(),
                        reader[2].ToString(), reader[3].ToString()));
                base.Close();
                reader.Close();
                return user_inf;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения info for user");
                base.Close();
                return null;
            }
        }


        public List<User> GetUsersTable()
        {
            List<User> users = new List<User>();
            Open();
            if (status)
            {
                string sql1 = "SELECT * from users;";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                while (reader.Read())
                    users.Add(new User(Convert.ToInt32(reader[0].ToString()), reader[1].ToString(),
                        reader[2].ToString(), Convert.ToInt32(reader[3].ToString())));
                base.Close();
                reader.Close();
                return users;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения таблицы users");
                base.Close();
                return null;
            }
        }
        public List<UserInfo> GetUsersInfoTable()
        {
            List<UserInfo> users = new List<UserInfo>();
            Open();
            if (status)
            {
                string sql1 = "SELECT * from users_info;";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                while (reader.Read())
                    users.Add(new UserInfo(Convert.ToInt32(reader[0].ToString()), reader[1].ToString(),
                        reader[2].ToString(),reader[3].ToString()));
                base.Close();
                reader.Close();
                return users;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения таблицы users_info");
                base.Close();
                return null;
            }
        }
        public List<UserLog> GetUsersLogsTable()
        {
            List<UserLog> usersLogs = new List<UserLog>();
            Open();
            if (status)
            {
                string sql1 = "SELECT * from users_log;";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                while (reader.Read())
                    usersLogs.Add(new UserLog(Convert.ToInt32(reader[0].ToString()), reader[1].ToString()));
                base.Close();
                reader.Close();
                return usersLogs;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения таблицы users_log");
                base.Close();
                return null;
            }
        }
        public List<Auction> GetAuctionsTable()
        {
            List<Auction> auctions = new List<Auction>();
            Open();
            if (status)
            {
                string sql1 = "select id, dt_start, dt_finish, price, car,start_price,last_buyer, auction_owner from auctions;";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                while (reader.Read())
                    auctions.Add(new Auction(Convert.ToInt32(reader[0].ToString()), reader[1].ToString(),
                        reader[2].ToString(), Convert.ToInt32(reader[3].ToString()), Convert.ToInt32(reader[4].ToString()),
                        Convert.ToInt32(reader[5].ToString()), Convert.ToInt32(reader[6].ToString()),
                        Convert.ToInt32(reader[7].ToString())));
                base.Close();
                reader.Close();
                return auctions;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения тем форума");
                return null;
            }
        }
        public List<Car> GetCarsTable()
        {
            List<Car> cars = new List<Car>();
            Open();
            if (status)
            {
                string sql1 = "select * from cars;";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                while (reader.Read())
                    cars.Add(new Car(Convert.ToInt32(reader[0].ToString()), reader[1].ToString(),
                        reader[2].ToString(), reader[3].ToString(), Convert.ToInt32(reader[4].ToString()),
                       reader[5].ToString(), Convert.ToInt32(reader[6].ToString()), reader[7].ToString(),
                       Convert.ToInt32(reader[8].ToString())));
                base.Close();
                reader.Close();
                return cars;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения table of cars");
                base.Close();
                return null;
            }
        }
        public List<ForumSection> GetForumSectionsTable()
        {
            List<ForumSection> sections = new List<ForumSection>();
            Open();
            if (status)
            {
                string sql1 = "select * from forum_sections;";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                while (reader.Read())
                    sections.Add(new ForumSection(Convert.ToInt32(reader[0].ToString()), reader[1].ToString(),
                        Convert.ToInt32(reader[2].ToString())));
                base.Close();
                reader.Close();
                return sections;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения ftable forum_sections");
                base.Close();
                return null;
            }
        }
        public List<ForumTopic> GetForumTopicsTable()
        {
            List<ForumTopic> sections = new List<ForumTopic>();
            Open();
            if (status)
            {
                string sql1 = "select * from forum_topics;";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                while (reader.Read())
                    sections.Add(new ForumTopic(Convert.ToInt32(reader[0].ToString()), Convert.ToInt32(reader[1].ToString()),
                        Convert.ToInt32(reader[2].ToString()), reader[3].ToString(), reader[4].ToString(), reader[5].ToString()));
                base.Close();
                reader.Close();
                return sections;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения ftable forum_topics");
                base.Close();
                return null;
            }
        }
        public List<ForumAnswer> GetForumAnswersTable()
        {
            List<ForumAnswer> answers = new List<ForumAnswer>();
            Open();
            if (status)
            {
                string sql1 = "select * from forum_answers;";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                while (reader.Read())
                    answers.Add(new ForumAnswer(Convert.ToInt32(reader[0].ToString()), Convert.ToInt32(reader[1].ToString()),
                        reader[2].ToString(), reader[3].ToString(), Convert.ToInt32(reader[4].ToString())));
                base.Close();
                reader.Close();
                return answers;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения ftable forum_answers");
                base.Close();
                return null;
            }
        }
    }
}
