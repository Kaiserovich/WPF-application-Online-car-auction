using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using ver03.FileConstructions;

namespace ver03.DataBase
{
    public class DBLoad : Connect
    {
        public int id { get; private set; }
        public string nick { get; private set; }

        public DBLoad(int id)
        {
            this.id = id;
            this.nick = Get_user_nick();
        }
        public DBLoad()
        {
            this.id = ver03.Properties.Settings.Default.UserID;
            this.nick = Get_user_nick();
        }

        public string Get_user_nick()
        {
            Open();
            if (status)
            {
                string sql1 = "select nick from users where id='" + id.ToString() + "';";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                if (reader.Read())
                    nick = reader[0].ToString();
                else
                    //MessageBox.Show("Error: Nickname не найден");
                base.Close();
                reader.Close();
                return nick;
            }
            else
            {
                MessageBox.Show("Проверьте соединение с сервером: ошибка получения nick");
                base.Close();
                return null;
            }
        }
        public string Get_user_nick(int id)
        {
            Open();
            if (status)
            {
                string sql1 = "select nick from users where id='" + id.ToString() + "';";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                if (reader.Read())
                    nick = reader[0].ToString();
                else
                    MessageBox.Show("Error: Nickname не найден");
                base.Close();
                reader.Close();
                return nick;
            }
            else
            {
                MessageBox.Show("Проверьте соединение с сервером: ошибка получения nick");
                base.Close();
                return null;
            }
        }

        public List<ForumTopic> GetAllTopicks()
        {
            List<ForumTopic> topicks = new List<ForumTopic>();
            Open();
            if (status)
            {
                string sql1 = "select forum_topics.id, forum_topics.id_author, users.nick, forum_topics.id_section, forum_topics.name,"+
 " forum_topics.date_create, forum_topics.context from forum_topics inner join users on "+
 "forum_topics.id_author = users.id; ";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                while (reader.Read())               
                    topicks.Add(new ForumTopic(Convert.ToInt32(reader[0]), Convert.ToInt32(reader[1]), reader[2].ToString(),
                        Convert.ToInt32(reader[3]), reader[4].ToString(), reader[5].ToString(), reader[6].ToString()));                
                base.Close();
                reader.Close();
                return topicks;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения тем форума");
                return null;
            }
        }
        public ForumTopic GetTopick(int id)
        {
            ForumTopic topick = null;
            Open();
            if (status)
            {
                string sql1 = "select forum_topics.id, forum_topics.id_author, users.nick, forum_topics.id_section, forum_topics.name," +
 " forum_topics.date_create, forum_topics.context from forum_topics inner join users on " +
 "forum_topics.id_author = users.id where forum_topics.id = "+id.ToString()+"; ";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                while (reader.Read())
                    topick = new ForumTopic(Convert.ToInt32(reader[0]), Convert.ToInt32(reader[1]), reader[2].ToString(),
                        Convert.ToInt32(reader[3]), reader[4].ToString(), reader[5].ToString(), reader[6].ToString());
                base.Close();
                reader.Close();
                return topick;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения темы форума");
                base.Close();
                return null;
            }
        }
        public List<ForumSection> GetTopicksSections()
        {
            List<ForumSection> topicks = new List<ForumSection>();
            Open();
            if (status)
            {
                string sql1 = "select * from forum_sections;";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                while (reader.Read())
                    topicks.Add(new ForumSection(Convert.ToInt32(reader[0]), reader[1].ToString(),
                        Convert.ToInt32(reader[2])));
                base.Close();
                reader.Close();
                return topicks;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения тем форума");
                return null;
            }
        }
        public List<ForumAnswer> GetAnswers(int id_section)
        {
            List<ForumAnswer> answers = new List<ForumAnswer>();
            Open();
            if (status)
            {
                string sql1 = "SELECT forum_answers.id_author, users.nick, forum_answers.id_topic,"+
" forum_answers.date_create, forum_answers.context"+
" FROM forum_answers, users where forum_answers.id_author = users.id and forum_answers.id_topic = "+id_section.ToString()+";";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                while (reader.Read())
                    answers.Add(new ForumAnswer(Convert.ToInt32(reader[0]), reader[1].ToString(), Convert.ToInt32(reader[2]),
                        reader[3].ToString(), reader[4].ToString()));
                base.Close();
                reader.Close();
                return answers;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения темы форума");
                base.Close();
                return null;
            }
        }

        public List<CarAutopark> GetAutoParkCars()
        {
            List<CarAutopark> cars = new List<CarAutopark>();
            Open();
            if (status)
            {
                string sql1 = "SELECT * from cars;";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                while (reader.Read())
                    cars.Add(new CarAutopark(Convert.ToInt32(reader[0].ToString()), reader[1].ToString(), 
                        reader[2].ToString(), reader[3].ToString(),  Convert.ToInt32(reader[4].ToString()),
                       reader[5].ToString(), Convert.ToInt32(reader[6].ToString()), reader[7].ToString(), 
                       Convert.ToInt32(reader[8].ToString())));
                base.Close();
                reader.Close();
                return cars;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения темы форума");
                base.Close();
                return null;
            }
        }
        public List<CarAutopark> GetAutoParkCarsFromAuctions()
        {
            List<CarAutopark> cars = new List<CarAutopark>();
            Open();
            if (status)
            {
                string sql1 = "select * from cars where id IN(select car from auctions);";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                while (reader.Read())
                    cars.Add(new CarAutopark(Convert.ToInt32(reader[0].ToString()), reader[1].ToString(),
                        reader[2].ToString(), reader[3].ToString(), Convert.ToInt32(reader[4].ToString()),
                       reader[5].ToString(), Convert.ToInt32(reader[6].ToString()), reader[7].ToString(),
                       Convert.ToInt32(reader[8].ToString())));
                base.Close();
                reader.Close();
                return cars;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения темы форума");
                base.Close();
                return null;
            }
        }
        public CarAutopark GetAutoParkCarByID(int id)
        {
            CarAutopark car = null;
            Open();
            if (status)
            {
                string sql1 = "SELECT * from cars where id="+id.ToString()+";";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                if (reader.Read())
                    car = new CarAutopark(Convert.ToInt32(reader[0].ToString()), reader[1].ToString(),
                        reader[2].ToString(), reader[3].ToString(), Convert.ToInt32(reader[4].ToString()),
                       reader[5].ToString(), Convert.ToInt32(reader[6].ToString()), reader[7].ToString(),
                       Convert.ToInt32(reader[8].ToString()));
                base.Close();
                reader.Close();
                return car;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения автомобиля по ID");
                base.Close();
                return null;
            }
        }
        public int GetLastCarID()
        {
            int id = 0;
            Open();
            if (status)
            {
                string sql1 = "SELECT max(id) FROM cars;";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                if (reader.Read())
                    id = Convert.ToInt32(reader[0]);
                Close();    
                reader.Close();
                return id;
            }
            else
            {
                MessageBox.Show("Проверьте соединение с сервером: ошибка получения car id");
                base.Close();
                return id;
            }
        }

        // ------------------------------------

        public Auction GetAuctionByID(int id) // reflected
        {
            Auction auct = null;
            Open();
            if (status)
            {
                string sql1 = "SELECT id,dt_start + 0,dt_finish + 0,price,car, start_price, auction_owner, last_buyer FROM auctions where id = " + id.ToString()+";";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                if(reader.Read())
                   auct = new Auction(Convert.ToInt32(reader[0].ToString()), reader[1].ToString(),
                        reader[2].ToString(), Convert.ToInt32(reader[3].ToString()),
                       Convert.ToInt32(reader[4].ToString()), Convert.ToInt32(reader[5].ToString()),
                       Convert.ToInt32(reader[7].ToString()), Convert.ToInt32(reader[6].ToString()));
                base.Close();
                reader.Close();
                return auct;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения аукциона по идентификатору");
                base.Close();
                return null;
            }

        }


        //TEST functions without Open()

        public int TimeToAuctionStart(int _id)
        {
            int sec = 0;
            try
            {
                string sql1 = "select TimeToAuctionStart("+_id.ToString()+");";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                if (reader.Read())
                {
                    sec = Convert.ToInt32(reader[0].ToString());
                }
                reader.Close();
                return sec;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Get time to Start ERROR \n" + ex.Message);
                return sec;
            }
        }
        public int TimeToAuctionFinish(int _id)
        {
            int sec = 0;
            try
            {
                string sql1 = "select TimeToAuctionFinish(" + _id.ToString() + ");";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                if (reader.Read())
                {
                    sec = Convert.ToInt32(reader[0].ToString());
                }
                reader.Close();
                return sec;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Get time to Finish ERROR \n" + ex.Message);
                return sec;
            }
        }

        public int UpAuctionCarPrice(int _id, float persent, int buyer_id)
        {
            int price = 0;
            try
            {
                string sql1 = "select up_car_price("+_id.ToString()+","+
                    persent.ToString().Replace(",", ".")+", "+buyer_id.ToString()+");";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                if (reader.Read())
                {
                    price = Convert.ToInt32(reader[0].ToString());
                }
                reader.Close();
                return price;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Up car price ERROR \n" + ex.Message);
                return price;
            }
        }
        public int GetCarPriceFromAuction(int id)
        {
            int price = 0;
            try
            {
                string sql1 = "select price from now_auctions where id="+id.ToString()+"";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                if (reader.Read())
                {
                    price = Convert.ToInt32(reader[0].ToString());
                }
                reader.Close();
                return price;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Up car price ERROR \n" + ex.Message);
                return price;
            }
        }

        public int[] GetTimeToAucFinAndCarPriceAndBuyerID(int id)
        {
            int[] arr = new int[3];
            try
            {
                string sql1 = "select TimeToAuctionFinish(" + id.ToString() + "), get_price("+id.ToString()
                    + "), get_auct_last_buyer_id("+id.ToString()+");";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                if (reader.Read())
                {
                    arr[0] = Convert.ToInt32(reader[0].ToString());
                    arr[1] = Convert.ToInt32(reader[1].ToString());
                    arr[2] = Convert.ToInt32(reader[2].ToString());
                }
                reader.Close();
                return arr;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Get time to start ERROR \n" + ex.Message);
                return arr;
            }
        }
        public int[] GetTimeToAucFinAndTimeToAuctStart(int id)
        {
            int[] arr = new int[2];
            try
            {
                string sql1 = "select TimeToAuctionFinish(" + id.ToString() + "), TimeToAuctionStart("+id.ToString()+");";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                if (reader.Read())
                {
                    arr[0] = Convert.ToInt32(reader[0].ToString());
                    arr[1] = Convert.ToInt32(reader[1].ToString());
                }
                reader.Close();
                return arr;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Get time to finish and start ERROR \n" + ex.Message);
                return arr;
            }
        }

        public List<Auction> GetAllAuctions()
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
                    auctions.Add(new Auction(Convert.ToInt32(reader[0].ToString()),reader[1].ToString(),
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
        public List<CarAutopark> GetCarsByOwnerID(int id)
        {
            List<CarAutopark> cars = new List<CarAutopark>();
            Open();
            if (status)
            {
                string sql1 = "SELECT * from cars where id_user="+id.ToString()+";";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                while (reader.Read())
                    cars.Add(new CarAutopark(Convert.ToInt32(reader[0].ToString()), reader[1].ToString(),
                        reader[2].ToString(), reader[3].ToString(), Convert.ToInt32(reader[4].ToString()),
                       reader[5].ToString(), Convert.ToInt32(reader[6].ToString()), reader[7].ToString(),
                       Convert.ToInt32(reader[8].ToString())));
                base.Close();
                reader.Close();
                return cars;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения авто пользователя");
                base.Close();
                return null;
            }
        }

        public int GetDeltaTimeByNow(string date)
        {
            int price = 0;
            try
            {
                string sql1 = "select TIME_TO_SEC(TIMEDIFF('"+date+"', now()));";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                if (reader.Read())
                {
                    price = Convert.ToInt32(reader[0].ToString());
                }
                reader.Close();
                return price;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Get delta time error\n" + ex.Message);
                return price;
            }
        }
        public bool IsAdmin(int id)
        {
            bool IsAdmin = false;
            Open();
            if (status)
            {
                string sql1 = "select is_admin from users where id='" + id.ToString() + "';";

                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();

                if (reader.Read())
                if (Convert.ToInt32(reader[0].ToString()) == 1)
                {
                    IsAdmin = true;
                }

                base.Close();
                reader.Close();

                return IsAdmin;
            }
            else
            {
                MessageBox.Show("Проверьте соединение с сервером");
                base.Close();
                return IsAdmin;
            }
        }

        public List<Note> GetUserNotes(int id)
        {
            List<Note> notes = new List<Note>();
            Open();
            if (status)
            {
                string sql1 = "select * from noties where id_adress = "+id.ToString()+";";
                MySqlCommand myCommand = new MySqlCommand(sql1, conn);
                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();
                while (reader.Read())
                {
                    //notes.Add(new Note(Convert.ToInt32(reader[0].ToString()), Convert.ToInt32(reader[1].ToString()),
                    //    reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), Convert.ToInt32(reader[5].ToString())));
                    notes.Add(new Note(Convert.ToInt32(reader[0].ToString()), Convert.ToInt32(reader[1].ToString()),
                       reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), Convert.ToBoolean(reader[5])));
                }
                base.Close();
                reader.Close();
                return notes;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения уведомлений пользователя");
                base.Close();
                return null;
            }
        }
    }
}
