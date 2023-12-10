using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static Halloween.MainWindow;
using static System.Net.Mime.MediaTypeNames;
using Halloween.Pages;
using System.Configuration;

namespace Halloween

{ 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Clés qui enregistre la variable "prénom"
            string prenom = ConfigurationManager.AppSettings["Prénom"];
            // Clés qui enregistre la variable "nom"
            string nom = ConfigurationManager.AppSettings["Nom"];
            // Clés qui enregistre la variable "Color"
            string Color = ConfigurationManager.AppSettings["Color"];

            // Si l'utilisateur n'a pas rentré son nom et son prénom
            if (string.IsNullOrEmpty(prenom)  || string.IsNullOrEmpty(nom))
            {
                // On l'enmène sur la page settings 
                Settings PS = new Settings();
                Conteneur.Children.Add(PS); 
            }
            else
            {
                // Sinon on l'enmène sur la page countdown
                Countdown PC = new Countdown();
                Conteneur.Children.Add(PC);
            }
            // Si la couleur prédéfini par l'utilisateur est verte
            if (Color == "Green")
            {
                // On change la couleur des boutons en vert et le type de police des boutons
                BTN_Prediction.Background = new SolidColorBrush(Colors.Green);
                BTN_Prediction.BorderBrush = new SolidColorBrush(Colors.DarkGreen);
                BTN_Prediction.FontFamily = new FontFamily("Chiller");
                BTN_Home.Background = new SolidColorBrush(Colors.Green);
                BTN_Home.BorderBrush = new SolidColorBrush(Colors.DarkGreen);
                BTN_Home.FontFamily = new FontFamily("Chiller");
                BTN_Settings.Background = new SolidColorBrush(Colors.Green);
                BTN_Settings.BorderBrush = new SolidColorBrush(Colors.DarkGreen);
                BTN_Settings.FontFamily = new FontFamily("Chiller");
                BTN_Game.Background = new SolidColorBrush(Colors.Green);
                BTN_Game.BorderBrush = new SolidColorBrush(Colors.DarkGreen);
                BTN_Game.FontFamily = new FontFamily("Chiller");

            }

        }
        // Voie privée qui enmene sur la page Home lorque l'on clique sur le BTN_Home 
        private void BTN_Home_Click(object sender, RoutedEventArgs e)
        {
            Conteneur.Children.Clear();
            Countdown PC = new Countdown();
            Conteneur.Children.Add(PC);
        }
        // Voie privée qui enmene sur la page settings lorque l'on clique sur le BTN_Sett 
        private void BTN_Sett_Click(object sender, RoutedEventArgs e)
        {
            Conteneur.Children.Clear();
            Settings PS = new Settings();
            Conteneur.Children.Add(PS);
           
        }
        // Voie privée qui enmene sur la page Predictions lorque l'on clique sur le BTN_Pred 
        private void BTN_Pred_Click(object sender, RoutedEventArgs e)

        {
            Conteneur.Children.Clear();
            P_Predictions PP = new P_Predictions();
            Conteneur.Children.Add(PP);
        }
        // Voie privée qui enmene sur la page Game lorque l'on clique sur le BTN_PGame
        private void BTN_Game_Click(object sender, RoutedEventArgs e)

        {
            Conteneur.Children.Clear();
            Game PP = new Game();
            Conteneur.Children.Add(PP);
        }


    }

}
