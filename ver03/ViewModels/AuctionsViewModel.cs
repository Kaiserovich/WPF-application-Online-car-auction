using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ver03.ViewModels
{
    class AuctionsViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private IMainWindowsCodeBehind _MainCodeBehind;

        public AuctionsViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));
            _MainCodeBehind = codeBehind;
        }


        private RelayCommand _but_show;
        public RelayCommand Load_Auction
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
             _MainCodeBehind.LoadView(ViewType.Auction);
        }

        private RelayCommand _add_auction;
        public RelayCommand Create_Auction
        {
            get
            {
                return _add_auction = _add_auction ??
                  new RelayCommand(OnShowMessage2, CanShowMessage2);
            }
        }
        private bool CanShowMessage2()
        {
            return true;
        }

        private void OnShowMessage2()
        {
            if (ver03.Properties.Settings.Default.Autoriz)
                _MainCodeBehind.LoadView(ViewType.AddAuction);
            else _MainCodeBehind.ShowMessage("Нужна авторизация");
        }
    }
}
