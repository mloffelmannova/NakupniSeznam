using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.IO;

namespace NakupniSeznam
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> NakupniListek { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            NakupniListek = new ObservableCollection<string>();
            nakupniListek.ItemsSource = NakupniListek;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string polozka = polozky.Text;
            if (!string.IsNullOrWhiteSpace(polozka))
            {
                NakupniListek.Add(polozka);
                polozky.Clear();
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (nakupniListek.SelectedItem != null)
            {
                NakupniListek.Remove((string)nakupniListek.SelectedItem);
            }
        }

        private void RenameButton_Click(object sender, RoutedEventArgs e)
        {
            if (nakupniListek.SelectedItem != null)
            {
                string oldItem = (string)nakupniListek.SelectedItem;
                string newItem = Microsoft.VisualBasic.Interaction.InputBox("Zadej nový název položky:", "Přejmenování", oldItem);
                if (!string.IsNullOrWhiteSpace(newItem))
                {
                    int index = NakupniListek.IndexOf(oldItem);
                    NakupniListek[index] = newItem;
                }
            }
        }
        private void vytvorit_Click(object sender, RoutedEventArgs e)
        {
            string json = JsonConvert.SerializeObject(NakupniListek);
            File.WriteAllText($"{Environment.CurrentDirectory}/file.json", json);
        }

        private void nacist_Click(object sender, RoutedEventArgs e)
        {
            string cesta = $"{Environment.CurrentDirectory}/file.json";
            if (File.Exists(cesta))
            {
                string vypisdata = File.ReadAllText(cesta);
                string json = JsonConvert.DeserializeObject<Nakup>(vypisdata);
            }
        }
    }

}