using System.IO;
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
            var text = File.ReadAllLines(Environment.CurrentDirectory + "/Resourses/ical-favorite-out.ics");
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
