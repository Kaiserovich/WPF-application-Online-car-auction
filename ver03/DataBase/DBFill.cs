using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using ver03.FileConstructions;

namespace ver03.DataBase
{
    public class DBFill : Connect
    {
        int id_author = 0;

        public DBFill(int id_author)
        {
            this.id_author = id_author;
        }
        public DBFill()
        {
            this.id_author = ver03.Properties.Settings.Default.UserID;
        }

        public bool CreateForumSection(string name_section, int color_style)
        {
            try
            {

                string sql1 = "INSERT INTO forum_sections(`name`, `color`) VALUES('" + name_section + "', '" + color_style + "'); ";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                Open();
                myCommand.ExecuteNonQuery();
                Close();
                return true;

            }
            catch
            {
                MessageBox.Show("Ошибка создания секции на форуме");
                return false;
            }
        }
        public bool CreateForumTopic(int id_author, int id_section, string name, string context)
        {
            try
            {
                string sql1 = "INSERT INTO forum_topics (`id_author`, `id_section`, `name`, `date_create`, `context`)" +
                   " VALUES ( '" + id_author.ToString() + "', '" + id_section.ToString() + "', '" + name + "', CURRENT_TIMESTAMP() , '" + context.ToString() + "')";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                Open();
                myCommand.ExecuteNonQuery();
                Close();
                return true;

            }
            catch
            {
                MessageBox.Show("Ошибка создания темы на форуме");
                return false;
            }
        }
        public bool CreateForumAnswer(int id_author, int id_topic, string context)
        {
            try
            {
                string sql1 = "INSERT INTO forum_answers (`id_author`, `id_topic`, `context`) VALUES ('" + id_author + 
                    "', '" + id_topic + "', '" + context + "');";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                Open();
                myCommand.ExecuteNonQuery();
                Close();
                return true;

            }
            catch
            {
                MessageBox.Show("Ошибка создания ответа на форуме");
                return false;
            }
        }
        public bool CreateNoteFromForumAnswer(ForumAnswer x, int id_author, string context, string title)
        {
            string sql1 = "INSERT INTO noties (`id_author`, `id_adress`, `tittle`, `context`) VALUES ('" + id_author.ToString()
                    + "', '" + x.id_author.ToString() + "', '" + title + "', '" + context + "');";

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
                MessageBox.Show("Ошибка создания ответа на форуме");
                Close();
                return false;
            }
        }
        public bool CreateCarFromAutoPark(CarAutopark c)
        {
            
            string sql1 = "INSERT INTO cars (`type`, `brend`, `model`, `year`, `fuel`, `mileage`, `context`, `id_user`) " +
                "VALUES ('"+c.type+"', '"+c.brend+"', '"+c.model+"', '"+c.year+"', '"+c.fuel+"', '"+c.mileage+"', " +
                "'"+c.context+ "' , " +"'" + c.id_user.ToString() + "');";

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
                MessageBox.Show("Ошибка создания авто в автопарке(объявления)");
                Close();
                return false;
            }
        }

        //TEST functions without Open()

        public void AuctionUpCarPrice(int _id, float persent, int buyer_id)
        {
            string sql1 = "select up_car_price(" + _id.ToString() + "," +
                    persent.ToString().Replace(",", ".") + ", " + buyer_id.ToString() + ");";

            try
            {
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка обновления цены + \n" + ex.Message);
            }
        }

        public void Test_SetAuctTimerByNow(int id)
        {
            string sql1 = "select set_test_time("+id.ToString()+");";

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

        public bool CreateAuction(Auction a)
        {

            string sql1 = "INSERT INTO auctions (`dt_start`, `dt_finish`, `price`, `start_price`, `auction_owner`, `car`, `last_buyer`) " +
                "VALUES ("+a.dt_start+", "+a.dt_finish+", "+a.price+", "+a.start_price+", "+a.owner+", "+
                a.car_id+", "+a.last_buyer+");";

            try
            {
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                Open();
                myCommand.ExecuteNonQuery();
                Close();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка создания аукциона \n" + ex.Message);
                Close();
                return false;
            }
        }
    }
}
