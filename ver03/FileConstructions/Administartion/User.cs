using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ver03.FileConstructions.Administartion
{
    public class User
    {
        public int id { get; set; }
        public string nick { get; set; }
        public string password { get; set; }
        public int isAdmin { get; set; }

        public User(int id, string nick, string password, int isAdmin)
        {
            this.id = id;
            this.nick = nick;
            this.password = password;
            this.isAdmin = isAdmin;
        }

        public override string ToString()
        {
            return id.ToString() + " " + nick;
        }
    }
}
