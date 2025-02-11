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
        public MainWindow()
        {
            InitializeComponent();
            JSON_data nakup = new JSON_data();
            nakup.NakupniListek = new List<string>();
            nakupniListek.ItemsSource = nakup.NakupniListek;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            JSON_data nakup = new JSON_data();
            string polozka = polozky.Text;
            if (!string.IsNullOrWhiteSpace(polozka))
            {
                nakup.NakupniListek.Add(polozka);
                polozky.Clear();
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            JSON_data nakup = new JSON_data();
            if (nakupniListek.SelectedItem != null)
            {
                nakup.NakupniListek.Remove((string)nakupniListek.SelectedItem);
            }
        }

        private void RenameButton_Click(object sender, RoutedEventArgs e)
        {
            JSON_data nakup = new JSON_data();
            if (nakupniListek.SelectedItem != null)
            {
                string oldItem = (string)nakupniListek.SelectedItem;
                string newItem = Microsoft.VisualBasic.Interaction.InputBox("Zadej nový název položky:", "Přejmenování", oldItem);
                if (!string.IsNullOrWhiteSpace(newItem))
                {
                    int index = nakup.NakupniListek.IndexOf(oldItem);
                    nakup.NakupniListek[index] = newItem;
                }
            }
        }
        private void vytvorit_Click(object sender, RoutedEventArgs e)
        {
            JSON_data nakup = new JSON_data();
            string json = JsonConvert.SerializeObject(nakup.NakupniListek);
            File.WriteAllText($"{Environment.CurrentDirectory}/file.json", json);
        }

        private void nacist_Click(object sender, RoutedEventArgs e)
        {
            JSON_data nakup = new JSON_data();
            string cesta = $"{Environment.CurrentDirectory}/file.json";
            if (File.Exists(cesta))
            {
                string vypisdata = File.ReadAllText(cesta);
                JSON_data json = JsonConvert.DeserializeObject<JSON_data>(vypisdata);
                foreach (var item in nakup.NakupniListek)
                {
                    nakupniListek.Items.Add(json);
                }
            }
        }

        private void polozky_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter) { AddButton_Click(sender, e); }
        }
    }

}


//private void Loadbtn_Click(object sender, RoutedEventArgs e)
//{
//    string soubor_cesta = ($"{Environment.CurrentDirectory}/file.json");
//    if (File.Exists(soubor_cesta))
//    {
//        string data = File.ReadAllText(soubor_cesta);
//        JSON_data json = JsonConvert.DeserializeObject<JSON_data>(data);
//        Seznam.Text = json.seznam_data;
//    }
//    else
//    { MessageBox.Show("Ještě jsi nevytořil list xd"); }
//}