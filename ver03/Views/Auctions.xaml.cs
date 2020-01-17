using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using ver03.FileConstructions;
using ver03.DataBase;
using System.Threading;
using ver03.ViewConstructions;

namespace ver03.Views
{
    /// <summary>
    /// Логика взаимодействия для Auctions.xaml
    /// </summary>
    public partial class Auctions : UserControl
    {
        List<ver03.FileConstructions.Auction> auctions;
        DBLoad dbl = new DBLoad();
        List<CarAutopark> cars = new List<CarAutopark>();
      

        public Auctions()
        {
            InitializeComponent();
            ver03.Properties.Settings.Default.Loading = true;
            Thread thread = new Thread(LoadAuctionCollection);
            thread.Start();

            void LoadAuctionCollection()
            {               
                auctions = dbl.GetAllAuctions();
                cars = dbl.GetAutoParkCarsFromAuctions();

                this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle,
                   (ThreadStart)delegate ()
                   {
                       ShowAllAuctions();
                       ver03.Properties.Settings.Default.Loading = false;
                   });
            }
        }

        private void But_create_auction(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("123");
        }
        void ShowAllAuctions()
        {
            if (auctions == null)
            {
                MessageBox.Show("Коллекция аукционов пуста");
                return;
            }
            foreach (var auction in auctions)
            {          
                Button but = new Button();
                foreach (var car in cars)
                {
                    if (auction.car_id == car.id)
                    {
                        AuctionBlock carViewConverter = new AuctionBlock(car, FindResource("LastPhoto"), FindResource("NextPhoto"), auction);
                        but.Content = carViewConverter.GetBlock();
                        but.Style = (Style)FindResource("AuctionBlockStyle");
                        but.Click += But_Click_Auction;
                        but.Tag = auction.id; // важно
                        Binding binding2 = BindingOperations.GetBinding(but_test, Button.CommandProperty); //костыль не удалять!
                        but.SetBinding(Button.CommandProperty, binding2); //костыль не удалять!
                        sp_auctions.Children.Add(but);
                    }
                }

                

                
            }
        }
        void But_Click_Auction(object sender, RoutedEventArgs e)
        {
            ver03.Properties.Settings.Default.AuctionSelectId = Convert.ToInt32(((Button)sender).Tag.ToString());
        }


    }
}
