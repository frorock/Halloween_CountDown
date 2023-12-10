using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

namespace Halloween.Pages
{
    /// <summary>
    /// Logique d'interaction pour Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {

        Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
  
        public Settings()
        {
            InitializeComponent();
            // Clés qui enregistre la variable "prénom"
            string prenom = ConfigurationManager.AppSettings["Prénom"];
            // Clés qui enregistre la variable "nom"
            string nom = ConfigurationManager.AppSettings["Nom"];
            // Clés qui enregistre la variable "Color"
            string Color = ConfigurationManager.AppSettings["Color"];
            // Clés qui enregistre la variable "Sexe"
            string Sexe = ConfigurationManager.AppSettings["Sexe"];


            // Si la couleur prédéfini par l'utilisateur est verte
            if (Color == "Green")
            {
                // On change le background de la page settings
                Uri Green = new Uri("..\\..\\..\\Ressources\\Background\\Background_Settings2.jpg", UriKind.Relative);
                // On change la couleur et le type de police du texte
                BK_Settings.ImageSource = new BitmapImage(Green);
                TB_Prenom.Foreground = new SolidColorBrush(Colors.Green);
                TB_Prenom.FontFamily = new FontFamily("Chiller");
                TB_Nom.Foreground = new SolidColorBrush(Colors.Green);
                TB_Nom.FontFamily = new FontFamily("Chiller");
                TB_Sexe.Foreground = new SolidColorBrush(Colors.Green);
                TB_Sexe.FontFamily = new FontFamily("Chiller");
                TB_Couleur.Foreground = new SolidColorBrush(Colors.Green);
                TB_Couleur.FontFamily = new FontFamily("Chiller");
                BTN_Save.Background = new SolidColorBrush(Colors.Green);
                BTN_Save.BorderBrush = new SolidColorBrush(Colors.DarkGreen);
                BTN_Save.FontFamily = new FontFamily("Chiller");
                // On désactive le bouton vert et on active le bouton rouge
                BTN_Green.IsEnabled = false;
                BTN_Red.IsEnabled = true;

            }
            // Si la couleur prédéfini par l'utilisateur est verte
            else if (Color == "Red")
            {
                // On désactive le bouton rouge et on active le bouton vert
                BTN_Green.IsEnabled = true;
                BTN_Red.IsEnabled = false;
                // On ne change pas le code car le thème rouge est celui utilisé de base
            }
        }

        // Voie privée lorsque que l'on appuie sur le bouton sauvegardé
        private void BTN_Save_Click(object sender, RoutedEventArgs e)
        {
            // Si l'utilisateur n'a pasrentré son nom et son prénom on ne change pas les clés prénom et nom
            if (string.IsNullOrEmpty(TB_Pr.Text) || string.IsNullOrEmpty(TB_Nm.Text))
            {
            }
            else
            {  // Sinon On enregistre la valeur de la variable prénom qui sera le text tapé dans la "TB_Pr.Text"
                configuration.AppSettings.Settings.Remove("Prénom");
                configuration.AppSettings.Settings.Add("Prénom", TB_Pr.Text);
                string prenom = ConfigurationManager.AppSettings["Prénom"];
                // On enregistre la valeur de la variable nom qui sera le text tapé dans la "TB_Nm.Text"
                configuration.AppSettings.Settings.Remove("Nom");
                configuration.AppSettings.Settings.Add("Nom", TB_Nm.Text);
                string nom = ConfigurationManager.AppSettings["Nom"];
                // On sauvegarde les modifications
                configuration.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }

      

            // On créé un messagebox pour prévenir l'utilisateur qu'il faut redamarer l'application afin de faire apparaitre les changement de design
            string messageBoxText = "Redemarrage de l'application nécéssaire";
            string caption = "";
            MessageBoxButton button = MessageBoxButton.OKCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;
            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

            switch (result)
            {
                case MessageBoxResult.OK:
                    var currentExecutablePath = Process.GetCurrentProcess().MainModule.FileName;
                    Process.Start(currentExecutablePath);
                    System.Windows.Application.Current.Shutdown(); break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        private void CB_Femme_Checked(object sender, RoutedEventArgs e)
        {
            configuration.AppSettings.Settings.Remove("Sexe");
            configuration.AppSettings.Settings.Add("Sexe", "Femme");
            string prenom = ConfigurationManager.AppSettings["Sexe"];

            CB_Homme.IsChecked = false;
        }
        private void CB_Homme_Checked(object sender, RoutedEventArgs e)
        {
            configuration.AppSettings.Settings.Remove("Sexe");
            configuration.AppSettings.Settings.Add("Sexe", "Homme");
            string prenom = ConfigurationManager.AppSettings["Sexe"];

            CB_Femme.IsChecked = false;

        }
        // Voie privée lorsque que l'on appuie sur le bouton vert
        private void BTN_Green_Click(object sender, RoutedEventArgs e)
        {
            // La variable "Thème" prends la valeur "Green"
            string Thème = "Green";
            // On l'enregistre dans la clés "Color" 
            configuration.AppSettings.Settings.Remove("Color");
            configuration.AppSettings.Settings.Add("Color", Thème);
            string Color = ConfigurationManager.AppSettings["Color"];

            // On sauvegarde les modifications
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            // On désactive le bouton vert et on active le bouton rouge
            BTN_Green.IsEnabled = false;
            BTN_Red.IsEnabled = true;
        }
        // Voie privée lorsque que l'on appuie sur le bouton rouge
        private void BTN_Red_Click(object sender, RoutedEventArgs e)
        {
            // La variable "Thème" prends la valeur "Red"
            string Thème = "Red";
            // On l'enregistre dans la clés "Color" 
            configuration.AppSettings.Settings.Remove("Color");
            configuration.AppSettings.Settings.Add("Color", Thème);
            string Color = ConfigurationManager.AppSettings["Color"];
            // On sauvegarde les modifications
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            // On désactive le bouton rouge et on active le bouton vert
            BTN_Green.IsEnabled = true;
            BTN_Red.IsEnabled = false;
        }

      
    }
}
