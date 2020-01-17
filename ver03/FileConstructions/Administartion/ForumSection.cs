using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ver03.FileConstructions.Administartion
{
    public class ForumSection
    {
        public int id { get; set; }
        public string name { get; set; }
        public int color { get; set; }


        public ForumSection(int id, string name, int color)
        {
            this.id = id;
            this.name = name;
            this.color = color;
        }

        public override string ToString()
        {
            return id.ToString() + " " + name + " " + color.ToString();
        }
    }
}
