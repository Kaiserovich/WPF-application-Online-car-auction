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

namespace ver03.Views
{
    public partial class Registration : UserControl
    {
        Registr_new_User reg = new Registr_new_User();
        Autorization aut = new Autorization();

        string nowLogin = null;
        string password1 = null;
        string password2 = null;
        string name = null;
        string telephone = null;
        string email;

        PasswordBox pasBox1;
        PasswordBox pasBox2;

        public Registration()
        {
            InitializeComponent();

            pasBox1 = box_password1;
            pasBox2 = box_password2;
        }

        private void But_Regist_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(Regist_new_User);
            thread.Start();

        }
        void Regist_new_User()
        {
            if (CheckValidInfo(nowLogin, password1, password2, email, telephone, name))
            {
                if (reg.Regist(nowLogin, password1, password2) && nowLogin.Length >= 4)
                {
                    int id = aut.Get_user_id(nowLogin);
                    if (reg.Fill_user_info(id, name, telephone, email))
                    {
                        if (reg.Create_log(id))
                        {
                            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle,
                                        (ThreadStart)delegate ()
                                        {
                                            ver03.Properties.Settings.Default.Autoriz = true;
                                            ver03.Properties.Settings.Default.UserID = id;
                                        });
                        }
                        else MessageBox.Show("Error ошибка создания лога");
                    }
                    else
                        MessageBox.Show("Error ошибка ввода учётных данных");
                }
            }
        }

        bool CheckValidInfo(string login, string pas1, string pas2, string mail, string tel, string nam)
        {
            if (nowLogin == null)
            {
                MessageBox.Show("Введите логин");
                return false;
            }
            if (nowLogin.Length < 4)
            {
                MessageBox.Show("Логин должен быть боллее 4-х символов");
                return false;
            }
            if (pas1 == null)
            {
                MessageBox.Show("Введите пароль");
                return false;
            }
            if (pas2 == null)
            {
                MessageBox.Show("Введите подтверждение пароля");
                return false;
            }
            if (pas1.Length < 4 || pas2.Length < 4)
            {
                MessageBox.Show("Пароль должен быть боллее 4-х символов");
                return false;
            }
            if (!pas1.Equals(pas2))
            {
                MessageBox.Show("Пароли не совпадают");
                return false;
            }
            if (mail == null)
            {
                MessageBox.Show("Введите email");
                return false;
            }
            if (mail.Length == 0)
            {
                MessageBox.Show("Введите email");
                return false;
            }
            if (!mail.Contains("@") || !mail.Contains("."))
            {
                MessageBox.Show("email введен некорректно");
                return false;
            }
            if (telephone == null || telephone.Length == 0)
            {
                MessageBox.Show("Введите телефон");
                return false;
            }
            if (!(telephone.Length > 13 || (!telephone.Contains(" ") && telephone.Length > 12)))
            {
                MessageBox.Show("Телефон введен некорректно");
                return false;
            }
            if (name == null || name.Length == 0)
            {
                MessageBox.Show("Пожалуйста, укажите, как к вам можно обращаться");
                return false;
            }

            return true;
        }

        void SetTBStyleValid(object sender)
        {
            ((TextBox)sender).Style = (Style)FindResource("TextBoxStyle00_ByRegistration_valid");
        }
        void SetTBStyleUnValid(object sender)
        {
            ((TextBox)sender).Style = (Style)FindResource("TextBoxStyle00_ByRegistration_unvalid");
        }
        void SetTBStyleDef(object sender)
        {
            ((TextBox)sender).Style = (Style)FindResource("TextBoxStyle00_ByRegistration_def");
        }

        void SetPBStyleDef(object sender)
        {
            ((PasswordBox)sender).Style = (Style)FindResource("PasswordBox_def");
        }
        void SetPBStyleValid(object sender)
        {
            ((PasswordBox)sender).Style = (Style)FindResource("PasswordBox_valid");
        }
        void SetPBStyleUnValid(object sender)
        {
            ((PasswordBox)sender).Style = (Style)FindResource("PasswordBox_unvalid");
        }

        

        private void box_nick_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (box_nick.Text.Length > 0)
                nowLogin = box_nick.Text;
        }
        private void box_password1_PasswordChanged(object sender, RoutedEventArgs e)
        {
            password1 = ((PasswordBox)sender).Password;
        }
        private void box_password2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            password2 = ((PasswordBox)sender).Password;
        }
        private void box_email_SelectionChanged(object sender, RoutedEventArgs e)
        {
            email = ((TextBox)sender).Text;           
        }
        private void box_telephone_TextChanged(object sender, RoutedEventArgs e)
        {
            telephone = ((TextBox)sender).Text;
        }
        private void box_name_SelectionChanged(object sender, RoutedEventArgs e)
        {
            name = ((TextBox)sender).Text;
            if (name.Length > 0)
                SetTBStyleValid(sender);
            else
                if (name.Length == 0)
                SetTBStyleDef(sender);
            else
                SetTBStyleUnValid(sender);
        }

        private void box_nick_LostFocus(object sender, RoutedEventArgs e)
        {
            if (nowLogin != null)
            {
                if (nowLogin.Length < 5)
                {
                    SetTBStyleDef(sender);
                }
                else
                {
                    bool result = false;
                    string nick;

                    Thread thread = new Thread(CheckNickValid);
                    thread.Start();

                    void CheckNickValid()
                    {
                        nick = nowLogin;
                        result = aut.Check_Login(nick);
                        this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle,
                    (ThreadStart)delegate ()
                    {
                        if (!result)
                            SetTBStyleValid(sender);
                        else
                            SetTBStyleUnValid(sender);
                    });
                    }


                }
            }
        }

        private void box_password1_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((password2 != null && password1 != null) && (password1 != null && password1.Length > 0) && (password2 != null && password2.Length > 0))
            {
                //MessageBox.Show("1");
                if (password2.Equals(password1))
                {
                    SetPBStyleValid(sender);
                    SetPBStyleValid(pasBox2);
                }
                else 
                {
                    SetPBStyleUnValid(sender);
                    SetPBStyleUnValid(pasBox2);
                }
            }
            else
            {
                //MessageBox.Show("2");
                SetPBStyleDef(sender);
                SetPBStyleDef(pasBox2);
            }


        }
        private void box_password2_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((password2 != null && password1 != null) && (password1 != null && password1.Length > 0) && (password2 != null && password2.Length > 0))
            {
                if (password1.Equals(password2))
                {
                    SetPBStyleValid(sender);
                    SetPBStyleValid(pasBox1);
                }
                else
                {
                    SetPBStyleUnValid(sender);
                    SetPBStyleUnValid(pasBox1);
                }

            }
            else
            {
                SetPBStyleDef(sender);
                SetPBStyleDef(pasBox1);
            }
          

        }

        private void box_email_LostFocus(object sender, RoutedEventArgs e)
        {
            if (email.Length > 0 && email.Contains("@") && email.Contains("."))
            {
                SetTBStyleValid(sender);
            }
            else
                if(email.Length > 0)
                SetTBStyleUnValid(sender);
            else
                SetTBStyleDef(sender);
        }
        private void box_telephone_LostFocus(object sender, RoutedEventArgs e)
        {
            if (telephone.Length > 13 || (!telephone.Contains(" ") && telephone.Length > 12))
                SetTBStyleValid(sender);
            else if (telephone.Length > 0)
                SetTBStyleUnValid(sender);
            else
                SetTBStyleDef(sender);
        }
    }
}
