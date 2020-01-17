using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ver03.FileConstructions
{
    public class Note
    {
        public int id_author { get; set; }
        public int id_adress { get; set; }
        public string send_dt { get; set; }
        public string tittle { get; set; }
        public string context { get; set; }
        public bool status { get; set; }

        public Note(int id_author, int id_adress, string send_dt, string tittle, string context, bool status)
        {
            this.id_author = id_author;
            this.id_adress = id_adress;
            this.send_dt = send_dt;
            this.tittle = tittle;
            this.context = context;
            this.status = status;
        }
    }
}
