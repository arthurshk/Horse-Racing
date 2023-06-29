using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HorseRace
{
    public class Horse
    {
        private static readonly Random random = new Random();
        public string Name { get; }
        public ProgressBar ProgressBar { get; }
        public int Progress { get; private set; }

        public Horse(string name, ProgressBar progressBar)
        {
            Name = name;
            ProgressBar = progressBar;
        }

        public void Run(object state)
        {
                if(Progress < 100)
            {
                int speed = random.Next(5, 15);
                Progress = Math.Min(Progress + speed, 100);
                Thread.Sleep(1000);
            }
        }
    }
}
