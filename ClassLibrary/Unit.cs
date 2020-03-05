using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Unit
    {
        public string Name { get; }
        public double DPS { get; }
        public double HDPS { get; }
        public double SingleDPS { get; }
        public double Life { get; set; }
        public double Reload { get; }

        /// <summary>
        /// Конструктор, который меняет значение только поля Life.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dps"></param>
        /// <param name="hdps"></param>
        /// <param name="singleDPS"></param>
        /// <param name="life"></param>
        /// <param name="reload"></param>
        public Unit(string name, double dps, double hdps, double singleDPS, double life, double reload)
        {
            Name = name;
            DPS = dps;
            HDPS = hdps;
            SingleDPS = singleDPS;
            Life = life;
            Reload = reload;
        }
    }
}
