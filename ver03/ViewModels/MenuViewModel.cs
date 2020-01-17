using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ver03.ViewModels
{
    class MenuViewModel
    {
        public MenuViewModel()
        {

        }

        public IMainWindowsCodeBehind CodeBehind { get; set; }



        private RelayCommand _LoadLoginUCCommand;
        public RelayCommand LoadLoginUCCommand
        {
            get
            {
                return _LoadLoginUCCommand = _LoadLoginUCCommand ??
                  new RelayCommand(OnLoadLoginUC, CanLoadLoginUC);
            }
        }
        private bool CanLoadLoginUC()
        {
            return true;
        }
        private void OnLoadLoginUC()
        {
            CodeBehind.LoadView(ViewType.Login);
        }


        private RelayCommand _LoadRegistrationCommand;
        public RelayCommand LoadRegistrationCommand
        {
            get
            {
                return _LoadRegistrationCommand = _LoadRegistrationCommand ??
                  new RelayCommand(OnLoadFirstUC, CanLoadFirstUC);
            }
        }
        private bool CanLoadFirstUC()
        {
            return true;
        }
        private void OnLoadFirstUC()
        {
            CodeBehind.LoadView(ViewType.Registration);
        }



        private RelayCommand _LoadForum;
        public RelayCommand LoadForum
        {
            get
            {
                return _LoadForum = _LoadForum ??
                  new RelayCommand(OnLoadForum_main, CanLoadForum_main);
            }
        }
        private bool CanLoadForum_main()
        {
            return true;
        }
        private void OnLoadForum_main()
        {
            CodeBehind.LoadView(ViewType.Forum);
            //System.Windows.MessageBox.Show("123");
        }

        private RelayCommand _LoadAuctions;
        public RelayCommand LoadAuctions
        {
            get
            {
                return _LoadAuctions = _LoadAuctions ??
                  new RelayCommand(OnLoadAuctions, CanLoadAuctions);
            }
        }
        private bool CanLoadAuctions()
        {
            return true;
        }
        private void OnLoadAuctions()
        {
            CodeBehind.LoadView(ViewType.Auctions);
        }



        private RelayCommand _ShowAuthorization;
        public RelayCommand Authorization
        {
            get
            {
                return _ShowAuthorization = _ShowAuthorization ??
                  new RelayCommand(OnLoadAuthorization, CanShowAuthorization);
            }
        }
        private bool CanShowAuthorization()
        {
            return true;
        }
        private void OnLoadAuthorization()
        {
            //CodeBehind.LoadView(ViewType.Forum);
            //System.Windows.MessageBox.Show("123");
            CodeBehind.HeadButAuthorization();
        }



        private RelayCommand _ExitAccount;
        public RelayCommand ExitAccount
        {
            get
            {
                return _ExitAccount = _ExitAccount ??
                  new RelayCommand(On_ExitAccount, Can__ExitAccount);
            }
        }
        private bool Can__ExitAccount()
        {
            return true;
        }
        private void On_ExitAccount()
        {
            //CodeBehind.LoadView(ViewType.Forum);
            //System.Windows.MessageBox.Show("123");
            CodeBehind.ButExitAccount();
        }


        private RelayCommand _LoadMainPage;
        public RelayCommand LoadMainPage
        {
            get
            {
                return _LoadMainPage = _LoadMainPage ??
                  new RelayCommand(OnLoadMainPage, CanLoadMainPage);
            }
        }
        private bool CanLoadMainPage()
        {
            return true;
        }
        private void OnLoadMainPage()
        {
            CodeBehind.LoadView(ViewType.MainPage);
        }


        private RelayCommand _LoadInfo;
        public RelayCommand LoadInfo
        {
            get
            {
                return _LoadInfo = _LoadInfo ??
                  new RelayCommand(OnLoadInfo, CanLoadInfo);
            }
        }
        private bool CanLoadInfo()
        {
            return true;
        }
        private void OnLoadInfo()
        {
            CodeBehind.LoadView(ViewType.Info);
        }

    }
}
