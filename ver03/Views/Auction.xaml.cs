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
using System.Windows.Media.Animation;
using System.Threading;
using System.Windows.Threading;
using ver03.DataBase;
using ver03.FileConstructions;
using ver03.ViewConstructions;


namespace ver03.Views
{
    public partial class Auction : UserControl
    {
        Storyboard[] arr = new Storyboard[16];
        Path[] pathArr = new Path[16];
        DispatcherTimer anim_timer;
        SolidColorBrush[] colors = new SolidColorBrush[17];
        int sec;

        DBLoad dbl;
        DBFill dbf;
        ver03.FileConstructions.Auction auct;

        DispatcherTimer timer;
        int secToStart;
        int secToFinish;

        int now_car_buyer_id;
        int auctID = 2;
        CancellationTokenSource upProgKiller;
        Task upProg;
        int nowPrice;

        CarAutopark car;

        AuctCarPhotos photos;
        Image image_presenter;
        TextBlock image_index_prsenter;

        bool isPageSelect;

        public Auction()
        {
            InitializeComponent();
            MainLoad();
        }
     
        void But_Up_Price_By_Five(object sender, RoutedEventArgs e)
        {
            ButUpCarPrice(20);
        }
        void But_Up_Price_By_Teb(object sender, RoutedEventArgs e)
        {
            ButUpCarPrice(10);
        }
        void But_Up_Price_By_Twenty(object sender, RoutedEventArgs e)
        {
            ButUpCarPrice(5);
        }
        void But_Up_Price_By_Thirty(object sender, RoutedEventArgs e)
        {
            ButUpCarPrice(3.3333f);
        }       
        void But_Up_Price_By_Fourty(object sender, RoutedEventArgs e)
        {
            ButUpCarPrice(2.5f);
        }
        void ButUpCarPrice(float percent)
        {
            if (ver03.Properties.Settings.Default.Autoriz)
            {
                if (secToFinish > 0)
                {
                    Thread thread = new Thread(UpCarPrice);
                    thread.Start();

                    void UpCarPrice()
                    {
                        this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle,
                        (ThreadStart)delegate ()
                        {
                            dbf.AuctionUpCarPrice(auctID, percent, ver03.Properties.Settings.Default.UserID);
                        });
                    }
                }
                else
                    MessageBox.Show("Аукцион завершен");
            }
            else
                MessageBox.Show("Нужна авторизация");
        }

        private void But_Last_Photo(object sender, RoutedEventArgs e)
        {
            photos.SetLastPhoto();
        }
        private void But_Next_Photo(object sender, RoutedEventArgs e)
        {
            photos.SetNextPhoto();
        }


        string GetPriceToString(int price)
        {
            int len = price.ToString().Length;
            if (len <= 3)
                return price.ToString() + "$";
            else if (len <= 6)
                return price.ToString().Insert(len - 3, " ") + "$";
            else if (len <= 9)
                return price.ToString().Insert(len - 6, " ").Insert(len - 2, " ") + "$";
            else
                return price.ToString() + "$";
        }
        void SetTBCarPrice(int price)
        {
            tb_price.Text = GetPriceToString(price);          
        }
        void SetTBHeader(string str)
        {
            try
            {
                tb_main_timer.Text = str;
            }
            catch
            {
                this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle,
                    (ThreadStart)delegate () { tb_main_timer.Text = str; });
            }
        }
        void SetButtonPrices()
        {
            but_five.Content = ConvPrice(20);
            but_ten.Content = ConvPrice(10);
            but_twelve.Content = ConvPrice(5);
            but_thirty.Content = ConvPrice(3.3333f);
            but_fourthty.Content = ConvPrice(2.5f);

            string ConvPrice(float percent)
            {
                string price;
                price = ((int)(auct.start_price / percent)).ToString();
                
                int len = price.Length;
                if (len <= 3)
                    return price.ToString() + "$";
                else if (len <= 6)
                    return price.ToString().Insert(len - 3, " ") + "$";
                else if (len <= 9)
                    return price.ToString().Insert(len - 6, " ").Insert(len - 2, " ") + "$";
                return price;
            }
        }

        void Initial_Timer()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpTBTimer);
            timer.Interval = new TimeSpan(0, 0, 1);
        }
        void UpTBTimer(object sender, EventArgs e)
        {
            secToStart = dbl.TimeToAuctionStart(auctID);
            if (secToStart > 0)
            {
                this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle,
                     (ThreadStart)delegate ()
                     {
                         SetTBHeader("До начала: " + getTimeBySec(secToStart));
                     });                         
            }
            else if (secToStart <= 0)
            {
                AuctionStarted();
                timer.Stop();
            }

            string getTimeBySec(int sec)
            {
                int hours = 0;
                int minutes = 0;
                int seconds = 0;

                if (sec > 3600)
                    hours = sec / 3600;
                if (sec > 60)
                    minutes = (sec - hours * 3600) / 60;
                seconds = sec - hours * 3600 - minutes * 60;

                return hours.ToString() + ":" + minutes.ToString() + ":" + seconds.ToString();
            }
        }

        void AuctionWait()
        {
            timer.Start();
        }
        void AuctionStarted()
        {
            SetTBHeader("Аукцион начат");
            StartUploader();          
        }
        void AuctionFinished()
        {
            SetTBHeader("Аукцион окончен");
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle,
                    (ThreadStart)delegate () { ClearProg(); });
            dbf.Close();
            dbl.Close();
        }

        void CheckAuctionStatus()
        {
            if (secToStart > 0)
            {
                AuctionWait();
            }
            else if (secToFinish > 0)
            {
                AuctionStarted();
            }
            else
            {
                AuctionFinished();
            }           
        }

        #region Upload info
        private void KillUpProgThread() 
        {
            upProgKiller.Cancel();
            //MessageBox.Show("Kill");          
        }
        void StartUploader()
        {
            upProgKiller = new CancellationTokenSource();
            upProg = Task.Factory.StartNew(load, upProgKiller.Token);
            void load()
            {
                while (true)
                {                   
                    int[] timeAndPriceAndBuyerID = dbl.GetTimeToAucFinAndCarPriceAndBuyerID(auctID);
                    int time = timeAndPriceAndBuyerID[0];
                    nowPrice = timeAndPriceAndBuyerID[1];
                    now_car_buyer_id = timeAndPriceAndBuyerID[2];

                    this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle,
                    (ThreadStart)delegate ()
                        {                   
                            CheckIsMyCar();
                            UploadProgToAuctionEnd(time);
                            SetTBCarPrice(nowPrice);                           
                        });
                    if (time <= 0 || isPageSelect != true)
                    {
                        if (now_car_buyer_id == ver03.Properties.Settings.Default.UserID && time <= 0)
                            GiveCarToBuyer();
                        AuctionFinished();
                        KillUpProgThread();
                        break;
                    }
                    Thread.Sleep(1000);                  
                }
            }
            bool CheckIsMyCar()
            {
                if (now_car_buyer_id == ver03.Properties.Settings.Default.UserID)
                {
                    ell_central.Fill = (SolidColorBrush)FindResource("ell_CarIsMy");
                    return true;
                }
                else
                {
                    ell_central.Fill = (SolidColorBrush)FindResource("grey00");
                    return false;
                }
            }
        }
        void UploadProgToAuctionEnd(int timeToFin)
        {
            try
            {
                int x = 16 - timeToFin;
                if (x >= 0)
                {
                    StartFillingProgress(x);
                }
            }
            catch { ClearProg(); }
        }
        #endregion
        #region animation
        
        void Initial_anim_timer()
        {
            anim_timer = new DispatcherTimer();
            anim_timer.Tick += new EventHandler(SetNextSection);
            anim_timer.Interval = new TimeSpan(0, 0, 1);
        }
        public void StartFillingProgress(int section)
        {
            ClearProg();
            SetColor(section);
            if (anim_timer != null)
                if (anim_timer.IsEnabled)
                    anim_timer.Stop();
            sec = section;
            anim_timer.Start();
        }
        void SetNextSection(object sender, EventArgs e)
        {
            if (sec <= 15)
                arr[sec].Begin();
            else anim_timer.Stop();
            sec += 1;
        }
        void InitStoryboard()
        {
            arr[0] = (Storyboard)FindResource("Progress01");
            arr[1] = (Storyboard)FindResource("Progress02");
            arr[2] = (Storyboard)FindResource("Progress03");
            arr[3] = (Storyboard)FindResource("Progress04");

            arr[4] = (Storyboard)FindResource("Progress05");
            arr[5] = (Storyboard)FindResource("Progress06");
            arr[6] = (Storyboard)FindResource("Progress07");
            arr[7] = (Storyboard)FindResource("Progress08");

            arr[8] = (Storyboard)FindResource("Progress09");
            arr[9] = (Storyboard)FindResource("Progress10");
            arr[10] = (Storyboard)FindResource("Progress11");
            arr[11] = (Storyboard)FindResource("Progress12");

            arr[12] = (Storyboard)FindResource("Progress13");
            arr[13] = (Storyboard)FindResource("Progress14");
            arr[14] = (Storyboard)FindResource("Progress15");
            arr[15] = (Storyboard)FindResource("Progress16");
        }
        void pathArrInit()
        {
            pathArr[0] = (Path)FindName("path01");
            pathArr[1] = (Path)FindName("path02");
            pathArr[2] = (Path)FindName("path03");
            pathArr[3] = (Path)FindName("path04");

            pathArr[4] = (Path)FindName("path05");
            pathArr[5] = (Path)FindName("path06");
            pathArr[6] = (Path)FindName("path07");
            pathArr[7] = (Path)FindName("path08");

            pathArr[8] = (Path)FindName("path09");
            pathArr[9] = (Path)FindName("path10");
            pathArr[10] = (Path)FindName("path11");
            pathArr[11] = (Path)FindName("path12");

            pathArr[12] = (Path)FindName("path13");
            pathArr[13] = (Path)FindName("path14");
            pathArr[14] = (Path)FindName("path15");
            pathArr[15] = (Path)FindName("path16");
        }
        void InitColors()
        {
            for (int i = 0; i < 17; i++)
            {
                if (i <= 11)
                    colors[i] = (SolidColorBrush)FindResource("Prog12");
                else if (i == 12)
                    colors[i] = (SolidColorBrush)FindResource("Prog13");
                else if (i == 13)
                    colors[i] = (SolidColorBrush)FindResource("Prog14");
                else if (i == 14)
                    colors[i] = (SolidColorBrush)FindResource("Prog15");
                else if (i == 15)
                    colors[i] = (SolidColorBrush)FindResource("Prog16");
                else if (i == 16)
                    colors[i] = (SolidColorBrush)FindResource("Prog17");
            }
        }
        void SetColor(int n)
        {
            for (int i = 0; i < n; i++)
            {
                pathArr[i].Fill = colors[i];             
            }          
        }
        void ClearProg()
        {
            for (int i = 0; i < 16; i++)
            {
                pathArr[i].Fill = colors[16];
            }
        }
        #endregion
        #region TEST Buttons

        void MainLoad()
        {
            isPageSelect = true;
            ver03.Properties.Settings.Default.Loading = true;
            dbl = new DBLoad();
            dbf = new DBFill();
            InitColors();
            InitStoryboard();
            pathArrInit();
            Initial_Timer();
            Initial_anim_timer();
            int auctid = ver03.Properties.Settings.Default.AuctionSelectId;
            if (auctid != 0)
            {
                this.auctID = auctid;              
            }
            else
                MessageBox.Show(auctid.ToString());
            image_presenter = img_now;
            image_index_prsenter = tb_photoIndex;

            Thread thread = new Thread(LoadAndShowAll);
            thread.Start();
           
            void LoadAndShowAll()
            {               
                LoadAuction(); // have Open and close
                LoadCar(); // have Open and close
                dbl.Open();
                dbf.Open();
                LoadTimes();
                CheckAuctionStatus();
                if (car != null)
                {
                    this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle,
                    (ThreadStart)delegate ()
                    {
                        upCarInfo();
                        upAuctInfo();
                        SetButtonPrices();
                        photos = new AuctCarPhotos(car.id, image_presenter, image_index_prsenter);
                        ver03.Properties.Settings.Default.Loading = false;
                    });
                }
                          
            }
        }
        void LoadCar()
        {
            car = dbl.GetAutoParkCarByID(auct.car_id);
        }
        void LoadAuction()
        {
            auct = dbl.GetAuctionByID(auctID);
        }
        void LoadTimes()
        {
            int[] times = dbl.GetTimeToAucFinAndTimeToAuctStart(auctID);
            secToFinish = times[0];
            secToStart = times[1];
        }

        void upCarInfo()
        {
            tb_brend.Text = car.brend;
            tb_model.Text = car.model;
            tb_body.Text = car.type;
            tb_year.Text = car.year.ToString();
            tb_milleage.Text = car.mileage.ToString() + "км";
            tb_context.Text = car.context;
        }
        void upAuctInfo()
        {
            SetTBCarPrice(auct.price);
        }


        void Set_time_now(object sender, RoutedEventArgs e) // Test 
        {
            if (secToFinish > 0 && secToStart <= 0)
            {
                MessageBox.Show("Дождитесь окончания аукциона");
            }
            else
            {
                if (timer.IsEnabled)
                    timer.Stop();

                dbf.Open();
                dbf.Test_SetAuctTimerByNow(auctID);
                dbf.Close();
                MainLoad();
            }

        }
        #endregion

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            isPageSelect = false;
        }
        private void GiveCarToBuyer()
        {
            MessageBox.Show("Вы приобрели " + car.brend + " " + car.model + " " + car.year + " года\n" +
                "за " + GetPriceToString(nowPrice));
        }
    }
}
