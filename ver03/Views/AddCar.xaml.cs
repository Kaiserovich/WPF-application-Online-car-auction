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
using Microsoft.Win32;
using ver03.FileConstructions;
using ver03.DataBase.FTP;
using ver03.DataBase;
using System.IO;
using System.Threading;

namespace ver03.Views
{
    /// <summary>
    /// Логика взаимодействия для AddCar.xaml
    /// </summary>
    public partial class AddCar : UserControl
    {
        List<CarPhoto> carPhotos = new List<CarPhoto>();
        DBFill dbf = new DBFill();
        DBLoad dbl = new DBLoad();
        FileManager menej = new FileManager();

        ControlTemplate butLeftPhotoTem;

        string brend = null;
        string model = null;
        string body_type = null;
        int year;
        string fuel = null;
        int milleage;
        string context = null;

        string engineAmount = null;
        string gearType = null;
        string steeringWheel = null;

        string dopContext = "";

        public AddCar()
        {
            butLeftPhotoTem = (ControlTemplate)FindResource("LastPhoto");

            menej.DeleteCarBufferFolder();
            InitializeComponent();
        }

        private void But_Add_Car_Photo(object sender, RoutedEventArgs e)
        {
            menej.CreateCarBuffeFolder();
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;...";
            if (dlg.ShowDialog() == true)
            {
                carPhotos.Add(new CarPhoto(dlg.SafeFileName, dlg.FileName));
                sp_CarPhotos.Children.Add(GetPhoto(menej.GetBufferCopyPath(dlg.FileName, dlg.SafeFileName)));
            }

            Image GetPhoto(string uri)
            {
                Image img = new Image();
                img.Margin = new Thickness(5, 0, 0, 0);
                BitmapImage logo = new BitmapImage();
                logo.BeginInit();
                logo.UriSource = new Uri(uri);
                logo.EndInit(); 
                img.Source = logo;
                return img;
            }

        }
        private void But_Add_Car(object sender, RoutedEventArgs e)
        {
            if (CheckInfoValid(brend, model, body_type, year, fuel, milleage, context))
            try
            {
                    TryAddContext();
                if (ver03.Properties.Settings.Default.Autoriz)
                {
                    sp_CarPhotos.Children.Clear();

                        Thread thread = new Thread(UpCarInfo);
                        thread.Start();

                        void UpCarInfo()
                        {
                            CarAutopark car = new CarAutopark(-1, body_type, brend, model,
                                year, fuel, milleage, context, ver03.Properties.Settings.Default.UserID);
                            dbf.CreateCarFromAutoPark(car);//+
                            int carId = dbl.GetLastCarID();//+
                            menej.FTPCreateCarPhotoFolder(carId); //+
                            foreach (CarPhoto uri in carPhotos)
                            {
                                menej.UploadCarPhoto(carId, uri.fullName, uri.name);
                            }
                        }
                }
                else
                {
                    MessageBox.Show("Нужна авторизация");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        string GetTextFromRTB(RichTextBox x)
        {
            TextRange z = new TextRange(x.Document.ContentStart, x.Document.ContentEnd);
            return z.Text.Replace(Environment.NewLine, " ");
        }
        bool CheckInfoValid(string brend, string model, string body_type, int year, string fuel, int milleage, string context)
        {
            if (brend == null || brend.Length == 0)
            {
                MessageBox.Show("Выберите марку");
                return false;
            }
            if (model == null || model.Length == 0)
            {
                MessageBox.Show("Введите модель авто");
                return false;
            }
            if (body_type == null || body_type.Length == 0)
            {
                MessageBox.Show("Выберите тип кузова");
                return false;
            }
            if (year <= 0)
            {
                MessageBox.Show("Введите год выпуска авто");
                return false;
            }
            if (fuel == null || fuel.Length == 0)
            {
                MessageBox.Show("Выберите тип топлива");
                return false;
            }
            if (milleage < 0)
            {
                MessageBox.Show("Введите пробег авто");
                return false;
            }
            if (context == null || context.Length == 0)
            {
                MessageBox.Show("Введите описание авто");
                return false;
            }

            return true;
        }
        void TryAddContext()
        {
            if (engineAmount != null)
            {
                dopContext += " Объём двигателя: " + engineAmount;
            }
            if (gearType != null)
            {
                dopContext += " КПП: " + gearType;
            }
            if (steeringWheel != null)
            {
                dopContext += " Руль: " + steeringWheel;
            }
            context += dopContext;
        }

        private void cb_Brend_Selected(object sender, RoutedEventArgs e)
        {
            brend = ((ComboBox)sender).SelectedValue.ToString();
        }
        private void tb_Model_TextChanged(object sender, TextChangedEventArgs e)
        {
            model = ((TextBox)sender).Text.ToString();
        }
        private void cb_CarType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            body_type = ((ComboBox)sender).SelectedValue.ToString();
        }
        private void tb_Year_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                year = Convert.ToInt32(((TextBox)sender).Text);
            }
            catch
            {
                MessageBox.Show("Неверный ввод данных");
                if (((TextBox)sender).Text.Length > 0)
                    ((TextBox)sender).Text = ((TextBox)sender).Text.Remove(((TextBox)sender).Text.Length-1,1);
            }
        }
        private void cb_Fuel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fuel = ((ComboBox)sender).SelectedValue.ToString();
        }
        private void tb_Mileage_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                milleage = Convert.ToInt32(((TextBox)sender).Text);
            }
            catch
            {
                MessageBox.Show("Неверный ввод данных");
                if (((TextBox)sender).Text.Length > 0)
                ((TextBox)sender).Text = ((TextBox)sender).Text.Remove(((TextBox)sender).Text.Length - 1, 1);
            }
        }
        private void rtb_Context_TextChanged(object sender, TextChangedEventArgs e)
        {
            context = GetTextFromRTB((RichTextBox)sender);
        }

        private void ComboBox_EngineAmount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            engineAmount = ((ComboBox)sender).SelectedValue.ToString();
        }
        private void ComboBox_GearType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gearType = ((ComboBox)sender).SelectedValue.ToString();
        }
        private void ComboBox_SteeringWheel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            steeringWheel = ((ComboBox)sender).SelectedValue.ToString();
        }
    }
}
