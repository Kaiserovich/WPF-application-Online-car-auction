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
using ver03.DataBase;
using System.Threading;
using System.ComponentModel;
using ver03.ViewModels;

namespace ver03.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginUC.xaml
    /// </summary>
    public partial class LoginUC : UserControl
    {
        public LoginUC()
        {
            InitializeComponent();
        }

        private void But_Login_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(Try_Log_in);
            thread.Start();
        }

        void Try_Log_in()
        {
            //Thread.Sleep(TimeSpan.FromSeconds(1));
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle,
                (ThreadStart)delegate ()
                {
                    Autorization log = new Autorization();
                    if (log.Check_Login(login_box.Text))
                    {
                        if (log.Check_Password(login_box.Text, password_box.Password))
                        {
                            int id = log.Get_user_id(login_box.Text);
                            if (log.Update_log(id))
                            {
                                ver03.Properties.Settings.Default.Autoriz = true;
                                ver03.Properties.Settings.Default.UserID = id;
                            }
                            else
                                MessageBox.Show("Error: ошибка обновления лога");
                        }
                        else
                            MessageBox.Show("Проверьте пароль");
                    }
                    else
                        MessageBox.Show("Проверьте логин");
                });
        }

    }   
}
