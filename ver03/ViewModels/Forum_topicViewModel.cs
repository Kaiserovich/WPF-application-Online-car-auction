using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Threading;


namespace ver03.ViewModels
{
    class Forum_topicViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        DispatcherTimer timer = new DispatcherTimer();

        private IMainWindowsCodeBehind _MainCodeBehind;

        public Forum_topicViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));
            _MainCodeBehind = codeBehind;
            Set_Timer();
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
            //timer.Start();
            //_MainCodeBehind.ShowMessage("Hellofrom forum topic");
        }
        void Set_Timer()
        {
            timer.Tick += new EventHandler(Try_Log_In);
            timer.Interval = new TimeSpan(0, 0, 1);
        }
        void Try_Log_In(object sender, EventArgs e)
        {
            _MainCodeBehind.Check_Autorization();
            if (ver03.Properties.Settings.Default.Autoriz)
                ((DispatcherTimer)sender).Stop();
        }
    }
}
