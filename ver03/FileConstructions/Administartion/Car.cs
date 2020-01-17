using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ver03.FileConstructions.Administartion
{
    public class Car
    {
        public int id { get; set; }
        public string type { get; set; }
        public string brend { get; set; }
        public string model { get; set; }
        public int year { get; set; }
        public string fuel { get; set; }
        public int mileage { get; set; }
        public string context { get; set; }
        public int id_user { get; set; }

        public Car(int id, string type, string brend, string model,
            int year, string fuel, int mileage, string context, int id_user)
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

        public override string ToString()
        {
            return id.ToString() + " " + brend + " " + model;
        }
    }
}
