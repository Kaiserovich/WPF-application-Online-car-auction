using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ver03.FileConstructions
{
    public class ForumTopic
    {
        public int id { get; set; }
        public int id_author { get; set; }
        public string nick { get; set; }
        public int id_section { get; set; }
        public string name { get; set; }
        public string date { get; set; }
        public string context { get; set; }

        public ForumTopic(int id)
        {
            this.id = id;
        }
        public ForumTopic(int id, int id_author, string nick, int id_section, string name, string date, string context)
        {
            this.id = id;
            this.id_author = id_author;
            this.nick = nick;
            this.id_section = id_section;
            this.name = name;
            this.date = date;
            this.context = context;
        }
    }
}
