using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ver03.ViewModels
{
    class User_RoomViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private IMainWindowsCodeBehind _MainCodeBehind;

        public User_RoomViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));
            _MainCodeBehind = codeBehind;
        }


        private RelayCommand _but_show;
        public RelayCommand but_show
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
            _MainCodeBehind.ShowMessage("Hello show me works");
        }

    }
}
