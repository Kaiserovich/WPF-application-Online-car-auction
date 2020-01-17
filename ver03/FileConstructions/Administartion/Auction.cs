using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ver03.FileConstructions.Administartion
{
    public class Auction
    {
        public int id { get; set; }
        public string dt_start { get; set; }
        public string dt_finish { get; set; }
        public int price { get; set; }
        public int start_price { get; set; }
        public int car_id { get; set; }
        public int last_buyer { get; set; }
        public int owner { get; set; }

        public Auction(int id, string dt_start, string dt_finish, int price, int car_id, int start_price, int last_buyer, int owner)
        {
            this.id = id;
            this.dt_start = dt_start;
            this.dt_finish = dt_finish;
            this.price = price;
            this.car_id = car_id;
            this.start_price = start_price;
            this.last_buyer = last_buyer;
            this.owner = owner;
        }

        public override string ToString()
        {
            return id.ToString() + " " + dt_start + " " + price.ToString();
        }
    }
}
