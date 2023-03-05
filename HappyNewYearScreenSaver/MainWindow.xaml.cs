using System.IO;
using System.Reflection;
using System.Windows;

namespace HappyNewYearScreenSaver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }
        public string Text { get => GetDayName(); }

        public string GetDayName()
        {
            string data = Properties.Resources._1;
            var text = Properties.Resources._1.Split('\n');
            var date = DateTime.Now.ToString("yyyyMMdd");
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i].Contains("VALUE=DATE:" + date))
                {
                    for (int y = i; y > 0; y--)
                    {
                        if (text[y].Contains("SUMMARY:"))
                            return text[y].Replace("SUMMARY:", "");
                    }
                }
            }
            return "Бывают, деточка, и просто дни";
        }
    }
}
