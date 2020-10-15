using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDB4O
{
    public class Pilot
    {
        public string Name { get; set; }
        public int Point { get; set; }

        public Pilot() { }
        public Pilot(string name, int point)
        {
            Name = name; Point = point;
        }

        public override string ToString()
        {
            return string.Format("{0} : {1}", Name, Point);
        }
    }
}
