using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ver03.ViewModels
{
    public class RegistViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        DispatcherTimer timer = new DispatcherTimer();

        private IMainWindowsCodeBehind _MainCodeBehind;

        public RegistViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));
            _MainCodeBehind = codeBehind;
            Initial_Timer();
        }


        private RelayCommand _but_regist;
        public RelayCommand but_regist
        {
            get
            {
                return _but_regist = _but_regist ??
                  new RelayCommand(OnShowMessage, CanShowMessage);
            }
        }
        private bool CanShowMessage()
        {
            return true;
        }
        private void OnShowMessage()
        {
            timer.Start();
        }
        void Initial_Timer()
        {
            timer.Tick += new EventHandler(Try_Log_in);
            timer.Interval = new TimeSpan(0, 0, 1);
        }
        void Try_Log_in(object sender, EventArgs e)
        {
            _MainCodeBehind.Check_Autorization();
            if (ver03.Properties.Settings.Default.Autoriz)
            {
                ((DispatcherTimer)sender).Stop();
            }
        }
    }
}
