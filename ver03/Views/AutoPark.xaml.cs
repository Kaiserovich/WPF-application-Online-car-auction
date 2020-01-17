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
using ver03.DataBase.FTP;
using ver03.ViewConstructions;
using ver03.FileConstructions;
using ver03.DataBase;

namespace ver03.Views
{
    /// <summary>
    /// Логика взаимодействия для AutoPark.xaml
    /// </summary>
    public partial class AutoPark : UserControl
    {
        public AutoPark()
        {
            InitializeComponent();
            FileManager menej = new FileManager();
            List<CarAutopark> cars = new List<CarAutopark>();
            DBLoad dbl = new DBLoad();

            cars = dbl.GetAutoParkCars();

            if (cars == null)
            {
                MessageBox.Show("Ошибка получения коллекции авто для форума");
                return;
            }

            foreach (CarAutopark car in cars)
            {
                menej.CreateCarFolder(car.id);
                menej.LoadCarPhotos(car.id);
                CreateCarBlock(car);
            }
            AddEmptyGrid();
        }
    
        private void CreateCarBlock(CarAutopark car)
        {
            CarBlock cb = new CarBlock(car, FindResource("LastPhoto"), FindResource("NextPhoto"));
            spCarList.Children.Add(cb.GetBlock());          
        }

        private void AddEmptyGrid()
        {
            Grid x = new Grid();
            //x.Background = Brushes.White;
            x.Height = 70;
            spCarList.Children.Add(x);
        }
    }
}
