using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ver03.ViewModels
{
    class AuctionViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private IMainWindowsCodeBehind _MainCodeBehind;

        public AuctionViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));
            _MainCodeBehind = codeBehind;
        }


        private RelayCommand _but_show;
        public RelayCommand Add_car
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
            // _MainCodeBehind.LoadView(ViewType.AddCar);
            //_MainCodeBehind.ShowMessage("I'll be back");
        }
    }
}
