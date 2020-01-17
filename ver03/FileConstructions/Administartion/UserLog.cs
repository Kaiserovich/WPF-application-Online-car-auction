using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ver03.FileConstructions.Administartion
{
    public class UserLog
    {
        public int id { get; set; }
        public string log { get; set; }

        public UserLog(int id, string log)
        {
            this.id = id;
            this.log = log;
        }
    }
}
