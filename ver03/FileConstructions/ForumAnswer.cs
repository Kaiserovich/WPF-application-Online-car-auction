using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ver03.FileConstructions
{
    public class ForumAnswer
    {
        public int id_author;
        public string nick_author;
        public int id_topic;
        public string date_create;
        public string context;

        public ForumAnswer(int id_author, string nick_author, int id_topic, string date_create, string context)
        {
            this.id_author = id_author;
            this.nick_author = nick_author;
            this.id_topic = id_topic;
            this.date_create = date_create;
            this.context = context;
        }
    }
}
