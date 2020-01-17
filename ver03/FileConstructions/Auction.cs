using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ver03.FileConstructions
{
    public class Auction
    {
        public int id;
        public string dt_start;
        public string dt_finish;
        public int price;
        public int start_price;
        public int car_id;

        public int last_buyer;
        public int owner;

        public Auction(int id, string dt_start, string dt_finish, int price, int car_id, int start_price)
        {
            this.id = id;
            this.dt_start = dt_start;
            this.dt_finish = dt_finish;
            this.price = price;
            this.car_id = car_id;
            this.start_price = start_price;
        }

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
    }
}
