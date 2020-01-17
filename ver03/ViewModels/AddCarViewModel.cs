using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ver03.ViewModels
{
    class AddCarViewModel
    {
        public string[] carBrends = new string[] { "Самодельный", "Acura", "Alfa Romeo", "Aston Martin," ,
            "Audi", "Bentley", "BMW", "Bugatti", "Buick", "Cadillac", "Chery", "Chevrolet", "Phrysler",
        "Citroen", "Dacia", "Daewoo", "Dodge", "Ferrary", "Fiat", "Ford", "Geely", "GMC", "Honda", "Hummer", "Hundai",
        "Volkswagen", "Nissan", "Mercedes-Benz", "Toyota"};
        public string[] CarBrends
        {
            get { return carBrends; }
        }

        public string[] carBodyes = new string[] { "седан", "универсал", "хэтчбэк", "купе", "лимузин", "микроавтобус",
        "минивен", "хардтоп", "кабриолет", "пикап", "фургон", "другой"};
        public string[] CarBodyes
        {
            get { return carBodyes; }
        }

        public string[] carFuel = new string[] { "бензин", "дизель", "газ", "элетро", "гибрид", "другой"};
        public string[] CarFuel
        {
            get { return carFuel; }
        }

        public string[] gearType = new string[] { "механическая", "автоматическая", "вариатор", "роботизированная", "отсутствует" };
        public string[] GearType
        {
            get { return gearType; }
        }

        public string[] steerWheel = new string[] { "левый", "правый"};
        public string[] SteerWheel
        {
            get { return steerWheel; }
        }

        public string[] engineAmount = new string[] { "0.8", "1.0", "1.2", "1.4", "1.6", "1.8", "2.0", "2.2", "2.4", "2.6", "2.8", "3.0"
        , "3.2", "3.4", "3.6", "3.8", "4.0", "4.2", "4.4", "4.6", "4.8", "5.0", "5.2", "5.3", "5.5", "5.6", "5.7", "6.0", "Другой..."};
        public string[] EngineAmount
        {
            get { return engineAmount; }
        }



        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private IMainWindowsCodeBehind _MainCodeBehind;

        public AddCarViewModel(IMainWindowsCodeBehind codeBehind)
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
            _MainCodeBehind.LoadView(ViewType.AutoPark);
        }

    }
}
