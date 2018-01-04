using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.IO;
using System.Data;
using System.Xml.Linq;
using Microsoft.Win32;

namespace LevelDbEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        XDocument doc;
        List<Line> keyValues;
        public MainWindow()
        {
            InitializeComponent();

            string data = File.ReadAllText(ConfigurationManager.AppSettings["filePath"]);

            doc = XDocument.Parse(data);
            keyValues = new List<Line>();

            foreach (XElement element in doc.Descendants().Where(p => p.HasElements == false))
            {
                string keyName = element.Name.LocalName;

                keyValues.Add(new Line() { key = keyName, value = element.Value});
            }
            dgUsers.ItemsSource = keyValues;
            dgUsers.CanUserAddRows = false;
        }

        private void btnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            XElement xml;
            if (keyValues.Count == 0)
            {
                xml = new XElement(doc.Root);
            }
            else
            {
                xml = new XElement(doc.Root.Name, keyValues.Select(i => new XElement(i.key, i.value)));
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, xml.ToString());
        }

        private void btnAddRow_Click(object sender, RoutedEventArgs e)
        {
            XElement xml;
            if (keyValues.Count == 0)
            {
                xml = new XElement(doc.Root);
            }
            else
            {
                xml = new XElement(doc.Root.Name, keyValues.Select(i => new XElement(i.key, i.value)));
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, xml.ToString());
        }

        public class Line
        {
            public string key { get; set; }

            public string value { get; set; }
        }
    }
}
