using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ver03.FileConstructions.Administartion
{
    public class ForumAnswer
    {
        public int id { get; set; }
        public int id_author { get; set; }
        public int id_topic { get; set; }
        public string date_create { get; set; }
        public string context { get; set; }

        public ForumAnswer(int id_author, int id_topic,
            string date_create, string context, int id)
        {
            this.id_author = id_author;
            this.id_topic = id_topic;
            this.date_create = date_create;
            this.context = context;
            this.id = id;
        }

        public override string ToString()
        {
            return id.ToString() + " " +id_author.ToString() + " "  + id_topic.ToString() + " " + date_create + " " + context;
        }
    }
}
