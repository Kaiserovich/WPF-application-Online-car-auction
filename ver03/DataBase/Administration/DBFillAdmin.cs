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
    public class DBFillAdmin : ver03.DataBase.DBFill
    {
        public void UpdateUser(User user)
        {
            Open();
            string sql1 = "UPDATE users SET `nick`='"+user.nick+"', `password`='"+user.password+"',"+
                " `is_admin`='"+user.isAdmin+"' WHERE `id`='"+user.id+"';";
            try
            {
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                myCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка  \n " + ex.Message);
            }
            Close();
        }
        public void UpdateUsers(List<User> users)
        {
            Open();
            foreach (var user in users)
            {
                string sql1 = "UPDATE users SET `nick`='" + user.nick + "', `password`='" + user.password + "'," +
                " `is_admin`='" + user.isAdmin + "' WHERE `id`='" + user.id + "';";
                try
                {
                    MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                    myCommand.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка  \n " + ex.Message);
                }
            }
            Close();
        }
        public void UpdateUsersInfo(List<UserInfo> users)
        {
            Open();
            foreach (var user in users)
            {
                string sql1 = "UPDATE users_info SET `name`='"+user.name+"', `telephone`='"+user.telephone
                    +"', `email`='"+user.email+"' WHERE `id`='"+user.id+"';";
                try
                {
                    MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                    myCommand.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка  \n " + ex.Message);
                }
            }
            Close();
        }
        public void UpdateUsersLogs(List<UserLog> usersLogs)
        {
            Open();
            foreach (var userLog in usersLogs)
            {
                MessageBox.Show(userLog.log);
                string sql1 = "UPDATE users_log SET `log_dt`='"+userLog.log+"' WHERE `id`='"+userLog.id+"';";
                try
                {
                    MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                    myCommand.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка  \n " + ex.Message);
                }
            }
            Close();
        }
        public void UpdateAuctions(List<Auction> auctions)
        {
            Open();
            foreach (var auction in auctions)
            {
                string sql1 = "UPDATE auctions SET `price`="+auction.price+", `start_price`="+auction.start_price+" WHERE `id`="+auction.id+";";
                try
                {
                    MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                    myCommand.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка  \n " + ex.Message);
                }
            }
            Close();
        }
        public void UpdateCars(List<Car> cars)
        {
            Open();
            foreach (var car in cars)
            {
                string sql1 = "UPDATE cars SET `type`='"+car.type+"', `brend`='"+car.brend+"', `model`='"+car.model
                    +"', `year`='"+car.year.ToString()+"', `fuel`='"+car.fuel+"', `mileage`='"+car.mileage+
                    "', `context`='"+car.context+"' WHERE `id`='"+car.id+"';";
                try
                {
                    MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                    myCommand.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка  \n " + ex.Message);
                }
            }
            Close();
        }
        public void UpdateForumSections(List<ForumSection> sections)
        {
            Open();
            foreach (var section in sections)
            {
                string sql1 = "UPDATE forum_sections SET `name`='"+section.name+"', `color`='"+section.color.ToString()
                    +"' WHERE `id`='"+section.id.ToString()+"';";
                try
                {
                    MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                    myCommand.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка  \n " + ex.Message);
                }
            }
            Close();
        }
        public void UpdateForumTopics(List<ForumTopic> topics)
        {
            Open();
            foreach (var topic in topics)
            {
                string sql1 = "UPDATE forum_topics SET `name`='"+topic.name+"', `context`='"+topic.context+"' WHERE `id`='"+topic.id+"';";
                try
                {
                    MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                    myCommand.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка  \n " + ex.Message);
                }
            }
            Close();
        }
        public void UpdateForumAnswers(List<ForumAnswer> answers)
        {
            Open();
            foreach (var answer in answers)
            {
                string sql1 = "UPDATE forum_answers SET `context`='"+answer.context+ "' WHERE `id`='"+answer.id.ToString()+"';";
                    try
                {
                    MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                    myCommand.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка  \n " + ex.Message);
                }
            }
            Close();
        }


        public void DeleteAuction(int id)
        {
            Open();
            string sql1 = "DELETE FROM auctions WHERE `id`="+id.ToString()+";";
            try
            {
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                myCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка  \n " + ex.Message);
            }
            Close();
        }
        public void DeleteCar(int id)
        {
            Open();
            string sql1 = "DELETE FROM cars WHERE `id`=" + id.ToString() + ";";
            try
            {
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                myCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка  \n " + ex.Message);
            }
            Close();
        }
        public void DeleteForumTopic(int id)
        {
            Open();
            string sql1 = "DELETE FROM forum_topics WHERE `id`='"+id.ToString()+"'";
            try
            {
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                myCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка  \n " + ex.Message);
            }
            Close();
        }
        //DELETE FROM `car_auction`.`forum_answers` WHERE `id`='2';
        public void DeleteForumAnswer(int id)
        {
            Open();
            string sql1 = "DELETE FROM forum_answers WHERE `id`='"+id.ToString()+"'";
            try
            {
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                myCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка  \n " + ex.Message);
            }
            Close();
        }
        public void DeleteUserAnswersFromTopic(int id_user, int id_topic)
        {
            Open();
            string sql1 = "DELETE FROM forum_answers WHERE `id_author`='" + id_user.ToString() + "' and `id_topic`='" + id_topic.ToString() + "'";
            try
            {
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                myCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка  \n " + ex.Message);
            }
            Close();
        }

    }
}
