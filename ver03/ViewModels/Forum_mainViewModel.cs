using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Threading;


namespace ver03.ViewModels
{
    class Forum_mainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private IMainWindowsCodeBehind _MainCodeBehind;

        public Forum_mainViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));
            _MainCodeBehind = codeBehind;
        }


        private RelayCommand _but_Login;
        public RelayCommand but_Login
        {
            get
            {
                return _but_Login = _but_Login ??
                  new RelayCommand(OnShowMessage, CanShowMessage);
            }
        }
        private bool CanShowMessage()
        {
            return true;
        }
        private void OnShowMessage()
        {
            //_MainCodeBehind.ShowMessage("I need you");
            _MainCodeBehind.LoadView(ViewType.Forum_topic);
        }
    }
}
