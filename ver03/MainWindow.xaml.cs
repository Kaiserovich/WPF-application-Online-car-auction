using ver03.ViewModels;
using ver03.Views;
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
using System.Windows.Threading;
using ver03.DataBase;
using System.Threading;
using ver03.FileConstructions;

namespace ver03
{
    public interface IMainWindowsCodeBehind
    {
        void ShowMessage(string message);
        void LoadView(ViewType typeView);
//        void UpUserId();
        void Check_Autorization();
        void ShowLoadIndicator();
        void CollapseLoadIndicator();
        void HeadButAuthorization();
        void ButExitAccount();
    }

    public enum ViewType
    {
        Login,
        Registration,
        Forum,
        Forum_topic,
        User_room,
        AutoPark,
        AddCar,
        Auctions,
        Auction,
        AddAuction,
        MainPage,
        Info
    }


    public partial class MainWindow : Window, IMainWindowsCodeBehind
    {
        public int id;
        DBLoad db;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MenuViewModel vm = new MenuViewModel();
            vm.CodeBehind = this;
            this.DataContext = vm;

            //Check_Autorization();
            LoadView(ViewType.MainPage);
            ver03.Properties.Settings.Default.PropertyChanged += Default_PropertyChanged;
        }

        private void Default_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) //NEW
        {
            if (ver03.Properties.Settings.Default.Loading == true)
            {
                ShowLoadIndicator();
            }
            else
                CollapseLoadIndicator();
        }

        public void LoadView(ViewType typeView)
        {
            switch (typeView)
            {
                case ViewType.Login:
                    {
                        LoginUC view = new LoginUC();
                        LoginViewModel vm = new LoginViewModel(this);
                        view.DataContext = vm;
                        this.OutputView.Content = view;
                        break;
                    }
                case ViewType.Registration:
                    {
                        Registration viewF = new Registration();
                        RegistViewModel vmF = new RegistViewModel(this);
                        viewF.DataContext = vmF;
                        this.OutputView.Content = viewF;
                        break;
                    }
                case ViewType.Forum:
                    {
                            Forum_main viewF = new Forum_main();
                            Forum_mainViewModel vmF = new Forum_mainViewModel(this);
                            viewF.DataContext = vmF;
                            this.OutputView.Content = viewF;
                            break;
                                     
                    }
                case ViewType.Forum_topic:
                    {
                        Forum_topic viewF = new Forum_topic();
                        Forum_topicViewModel vmF = new Forum_topicViewModel(this);
                        viewF.DataContext = vmF;
                        this.OutputView.Content = viewF;
                        break;
                    }
                case ViewType.User_room:
                    {
                        User_Room viewF = new User_Room();
                        User_RoomViewModel vmF = new User_RoomViewModel(this);
                        viewF.DataContext = vmF;
                        this.OutputView.Content = viewF;
                        break;
                    }
                case ViewType.AutoPark:
                    {
                        AutoPark viewF = new AutoPark();
                        AutoParkViewModel vmF = new AutoParkViewModel(this);
                        viewF.DataContext = vmF;
                        this.OutputView.Content = viewF;
                        break;
                    }
                case ViewType.AddCar:
                    {
                        AddCar viewF = new AddCar();
                        AddCarViewModel vmF = new AddCarViewModel(this);
                        viewF.DataContext = vmF;
                        this.OutputView.Content = viewF;
                        break;
                    }
                case ViewType.Auctions:
                    {
                        Auctions viewF = new Auctions();
                        AuctionsViewModel vmF = new AuctionsViewModel(this);
                        viewF.DataContext = vmF;
                        this.OutputView.Content = viewF;
                        break;
                    }
                case ViewType.Auction:
                    {
                        ver03.Views.Auction viewF = new ver03.Views.Auction();
                        AuctionViewModel vmF = new AuctionViewModel(this);
                        viewF.DataContext = vmF;
                        this.OutputView.Content = viewF;
                        break;
                    }
                case ViewType.AddAuction:
                    {
                        AddAuction viewF = new ver03.Views.AddAuction();
                        AddAuctionViewModel vmF = new AddAuctionViewModel(this);
                        viewF.DataContext = vmF;
                        this.OutputView.Content = viewF;
                        break;
                    }
                case ViewType.MainPage:
                    {
                        MainPage viewF = new ver03.Views.MainPage();
                        MainPageViewModel vmF = new MainPageViewModel(this);
                        viewF.DataContext = vmF;
                        this.OutputView.Content = viewF;
                        break;
                    }
                case ViewType.Info:
                    {
                        Info viewF = new ver03.Views.Info();
                        InfoViewModel vmF = new InfoViewModel(this);
                        viewF.DataContext = vmF;
                        this.OutputView.Content = viewF;
                        break;
                    }

            }

            //void LoadPageAndViewModel<T,V>(T page, V viewModel)
            //    where T: new()
            //    where V: class, new()
            //{
            //    T viewF = new T();
            //    V vmF = new V(this);
            //    viewF.DataContext = vmF;
            //    this.OutputView.Content = viewF;
            //}

        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public void Check_Autorization()
        {
            if (ver03.Properties.Settings.Default.Autoriz)
            {
                this.id = ver03.Properties.Settings.Default.UserID;
                
                Collapse_AuthorizationButtons();
                OutputView.Content = null;

                Thread thread = new Thread(Load_nickname);
                thread.Start();

              

            }
        }
        public void Check_IsAdmin()
        {
            Thread thread = new Thread(LoadAdminInfo);
            
            thread.Start();
            void LoadAdminInfo()
            {
                this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle,
                (ThreadStart)delegate ()
                {
                    ver03.Properties.Settings.Default.IsAdmin = db.IsAdmin(id);
                    if (ver03.Properties.Settings.Default.IsAdmin)
                    {
                        //MessageBox.Show("You are admin");
                        AdminWIndow adm = new AdminWIndow();
                        adm.Show();
                    }
                    //else
                        //MessageBox.Show("You are not admin");
                });
               
            }
        }
        public void Show_AuthorizationButtons()
        {
            but_regist.Visibility = Visibility.Visible;

            if (ver03.Properties.Settings.Default.Autoriz)
            {
                but_exit.Visibility = Visibility.Visible;
            }
            else
            {
                but_login.Visibility = Visibility.Visible;
            }
        }

        public void Collapse_AuthorizationButtons()
        {
            but_exit.Visibility = Visibility.Collapsed;
            but_login.Visibility = Visibility.Collapsed;
            but_regist.Visibility = Visibility.Collapsed;
        }

        public void HeadButAuthorization()
        {
            Show_AuthorizationButtons();
        }

        public void ButExitAccount()
        {
            ver03.Properties.Settings.Default.UserID = 0;
            ver03.Properties.Settings.Default.Autoriz = false;
            tblock_nick.Text = "Гость";

            Collapse_AuthorizationButtons();
            Show_AuthorizationButtons();
        }

        void Load_nickname()
        {
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle,
                (ThreadStart)delegate ()
                {
                    ShowLoadIndicator();
                });
            Thread.Sleep(TimeSpan.FromSeconds(1));
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle,
                (ThreadStart)delegate ()
                {
                    db = new DBLoad(id);
                    ver03.Properties.Settings.Default.NickName = db.nick;
                    Set_NickName(db.nick);

                    Check_IsAdmin();// Administaration part
                    CollapseLoadIndicator();
                });
        }

        private void Set_NickName(string str)
        {
            tblock_nick.Text = str;
        }
        private void Sp_User_Room(object sender, MouseButtonEventArgs e)
        {
            if (ver03.Properties.Settings.Default.Autoriz)
            LoadView(ViewType.User_room);
        }
        private void But_Autopark_Click(object sender, RoutedEventArgs e)
        {
            ver03.Properties.Settings.Default.Loading = true;
            Thread t = new Thread(load);
            void load()
            {
                this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle,
                (ThreadStart)delegate ()
                {
                    LoadView(ViewType.AutoPark);
                    ver03.Properties.Settings.Default.Loading = false;
                });
            }
            t.Start();
        }

        public void ShowLoadIndicator()
        {
            LoadIndic.Visibility = Visibility.Visible;
        }
        public void CollapseLoadIndicator()
        {
            LoadIndic.Visibility = Visibility.Collapsed;
        }
    }
}
