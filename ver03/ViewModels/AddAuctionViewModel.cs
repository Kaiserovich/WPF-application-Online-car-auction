using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ver03.ViewModels
{
    class AddAuctionViewModel
    {
        public string[] hours = new string[24];
        public string[] Hours
        {          
            get { return hours; }
        }

        public string[] minutes = new string[60];
        public string[] Minutes
        {
            get { return minutes; }
        }

        public string[] seconds = new string[60];
        public string[] Seconds
        {
            get { return seconds; }
        }



        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private IMainWindowsCodeBehind _MainCodeBehind;

        public AddAuctionViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));
            _MainCodeBehind = codeBehind;
            for (int i = 0; i < 24; i++)
            {
                if (i<10)
                hours[i] = "0" + i.ToString();
                else
                    hours[i] = i.ToString();
            }
            for (int i = 0; i < 60; i++)
            {
                if (i < 10)
                {
                    minutes[i] = "0" + i.ToString();
                    seconds[i] = "0" + i.ToString();
                }
                else
                {
                    minutes[i] = i.ToString();
                    seconds[i] = i.ToString();
                }
            }
        }


        private RelayCommand _but_show;
        public RelayCommand Create_Auction
        {
            get
            {
                return _but_show = _but_show ??
                  new RelayCommand(OnShowMessage, CanShowMessage);
            }
        }
        private bool CanShowMessage()
        {
            return true;
        }
        private void OnShowMessage()
        {
            if (ver03.Properties.Settings.Default.IsInfoValid == true)
            {
                _MainCodeBehind.LoadView(ViewType.Auctions);
                ver03.Properties.Settings.Default.IsInfoValid = false;
            }
        }
    }
}
