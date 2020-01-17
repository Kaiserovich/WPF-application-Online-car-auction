using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ver03.DataBase;
using ver03.FileConstructions;

namespace ver03.Views
{
    /// <summary>
    /// Логика взаимодействия для AddAuction.xaml
    /// </summary>
    public partial class AddAuction : UserControl
    {
        DBFill dbf = new DBFill();
        DBLoad dbl = new DBLoad();
        ver03.FileConstructions.Auction auct;

        public AddAuction()
        {
            InitializeComponent();
            
            List<CarAutopark> userCars = dbl.GetCarsByOwnerID(ver03.Properties.Settings.Default.UserID);

            foreach (var car in userCars)
            {
                ComboBoxItem item = new ComboBoxItem();
                TextBlock tb = new TextBlock();
                tb.Text = car.brend + " " + car.model + " " + car.year.ToString();
                item.Content = tb;
                item.Tag = car.id;
                cb_car.Items.Add(item);
            }

            dp_date.SelectedDate = DateTime.Now.Date;
        }

        private void But_Add_Auction(object sender, RoutedEventArgs e)
        {
            string start;
            int price;
            try
            {
                start = dp_date.Text.Split('.')[2] + dp_date.Text.Split('.')[1] + dp_date.Text.Split('.')[0] +
                   cb_hours.SelectedValue.ToString() + cb_minutes.SelectedValue.ToString() + cb_seconds.SelectedValue.ToString();
            }
            catch
            {
                MessageBox.Show("Неправильный формат даты");
                return;
            }
            if (cb_car.Items.Count == 0)
            {
                MessageBox.Show("Для создания аукциона, нужно создать и выбрать объявление");
                return;
            }
            try
            {
                price = Convert.ToInt32(tb_price.Text.Replace(" ", ""));
            }
            catch
            {
                MessageBox.Show("Неправильный формат стартовой цены");
                return;
            }

            int car_id = Convert.ToInt32(((ComboBoxItem)(cb_car.SelectedItem)).Tag.ToString());
            int now_user_id = ver03.Properties.Settings.Default.UserID;
            if (CheckInfoValid(start, price))
            {
                ver03.Properties.Settings.Default.IsInfoValid = true;
                auct = new FileConstructions.Auction(0, start, start, price,
                    car_id, Convert.ToInt32(price), now_user_id, now_user_id);
                dbf.CreateAuction(auct);
                
            }
        }
        bool CheckInfoValid(string date, int price)
        {
            dbl.Open();
            int timetostart = dbl.GetDeltaTimeByNow(date);
            dbl.Close();

            if (timetostart < 0)
            {
                MessageBox.Show("Дата указана неверно");
                return false;
            }
            if (price < 100)
            {
                MessageBox.Show("Стартовая цена должна быть более 100$");
                return false;
            }
            return true;
        }
    }
}
