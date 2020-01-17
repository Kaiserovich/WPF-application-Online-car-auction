using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ver03.FileConstructions.Administartion
{
    public class UserInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string telephone { get; set; }
        public string email { get; set; }

        public UserInfo(int id, string name, string telephone, string email)
        {
            this.id = id;
            this.name = name;
            this.telephone = telephone;
            this.email = email;
        }
    }
}
