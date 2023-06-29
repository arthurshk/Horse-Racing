using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HorseRace
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Horse> horses = new List<Horse>();

        public MainWindow()
        {
            InitializeComponent();
            horses.Add(new Horse("German Holstein", Horse1Progress));
            horses.Add(new Horse("Los Angeles Debby", Horse2Progress));
            horses.Add(new Horse("Egyptian Caribou", Horse3Progress));
            horses.Add(new Horse("Spanish Wildhawk", Horse4Progress));
            horses.Add(new Horse("Israeli Comet", Horse5Progress));
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Horse horse in horses)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(horse.Run));
            }
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
            {
                UpdateProgressBars();
                DisplayResults();
            }));
        }

        private void UpdateProgressBars()
        {
            foreach (Horse horse in horses)
            {
                horse.ProgressBar.Dispatcher.Invoke(new Action(delegate
                {
                    horse.ProgressBar.Value = horse.Progress;
                }));
            }
        }

        private void DisplayResults()
        {
            horses.Sort(delegate (Horse x, Horse y)
            {
                return y.Progress.CompareTo(x.Progress);
            });
            string message = "Race Results:\n\n";
            for (int i = 0; i < horses.Count; i++)
            {
                message += $"{i + 1}. {horses[i].Name} - {horses[i].Progress:F1}%\n";
                if (horses[i].Progress == 100)
                {
                    MessageBox.Show($"{horses[i].Name} WON!!!!");
                }
            }
            MessageBox.Show(message, "Results");
        }
    }
}

