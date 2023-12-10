using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
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

namespace Halloween.Pages
{
    /// <summary>
    /// Logique d'interaction pour P_Predictions.xaml
    /// </summary>
    public partial class P_Predictions : UserControl
    {
        Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        public P_Predictions()
        {
            InitializeComponent();
            // La variable Indice prends la valeur de la clés qui enregistre la variable "Indice"
            string Indice = ConfigurationManager.AppSettings["Indice"];
            // Variable "lignes" qui va lire chaque ligne du fichier text "Predictions.txt"
            var Lignes = System.IO.File.ReadAllLines(@"C:..\\..\\..\\Ressources\\Voyante\\Predictions.txt");
            // Variable qui va prendre la valeur d'un url d'une de nos images carte
            string url = "../Ressources/Cartes/";
            // La variable "url" prends la valeur de l'indice plus ".jpg" ce qui va nous permettre d'afficher une image en fonction  de l'indice
            url += (Indice.ToString() + ".jpg");
            carte.Source = new BitmapImage(new Uri(url, UriKind.Relative));
            // La TB_Speach va afficher le text d'une des lignes  du fichier text "Predictions.txt" en fonction  de l'indice
            TB_Speach.Text = (Lignes[int.Parse(Indice)]);
        }
    }
}
