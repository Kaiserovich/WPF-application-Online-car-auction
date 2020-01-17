using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ver03.FileConstructions
{
    public class CarAutopark
    {
        public int id;
        public string type;
        public string brend;
        public string model;
        public int year;
        public string fuel;
        public int mileage;
        public string context;
        public int id_user;

        public CarAutopark(int id, string type, string brend, string model, 
            int year, string fuel, int mileage , string context, int id_user)
        {
            this.id = id;
            this.type = type;
            this.brend = brend;
            this.model = model;
            this.year = year;
            this.fuel = fuel;
            this.mileage = mileage;
            this.context = context;
            this.id_user = id_user;
        }
    }
}
