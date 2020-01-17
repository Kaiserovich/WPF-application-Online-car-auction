using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ver03.FileConstructions.Administartion
{
    public class ForumTopic
    {
        public int id { get; set; }
        public int id_author { get; set; }
        public int id_section { get; set; }
        public string name { get; set; }
        public string date_create { get; set; }
        public string context { get; set; }

        public ForumTopic(int id, int id_author, int id_section,
            string name, string date_create, string context)
        {
            this.id = id;
            this.id_author = id_author;
            this.id_section = id_section;
            this.name = name;
            this.date_create = date_create;
            this.context = context;
        }

        public override string ToString()
        {
            return id.ToString() + " " + date_create + " " + name;
        }
    }
}
