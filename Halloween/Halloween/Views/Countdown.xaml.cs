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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Diagnostics;

namespace Halloween.Pages
{
    /// <summary>
    /// Logique d'interaction pour Countdown.xaml
    /// </summary>
    public partial class Countdown : UserControl
    {
        Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        public Countdown()
        {
            InitializeComponent();
            // Clés qui enregistre la variable "prénom"
            string prenom = ConfigurationManager.AppSettings["Prénom"];
            // Clés qui enregistre la variable "nom"
            string nom = ConfigurationManager.AppSettings["Nom"];
            // Clés qui enregistre la variable "JAfter"
            string Jafter = ConfigurationManager.AppSettings["JAfter"];
            // Clés qui enregistre la variable "Color"
            string Color = ConfigurationManager.AppSettings["Color"];

            // Code qui nous permet de calculer les jours restants avant halloween en fonction de la date du jour d'halloween et de la date actuelle
            DateTime DateHallo = new DateTime(2024, 10, 31);
            DateTime thisDay = DateTime.Now;
            // "Jrest" est donc la variable qui prends la valeur des jours restants
            int JRest = (DateHallo - thisDay).Days;

            // Si la couleur prédéfini par l'utilisateur est verte
            if (Color == "Green")
            {
                // On change le background de la page countdown
                Uri Green = new Uri("..\\..\\..\\Ressources\\Background\\Background_Countdown2.jpg", UriKind.Relative);
                BK_Countdown.ImageSource = new BitmapImage(Green);

                // On change le text affiché dans la page countdown
                TBL_NbrJGreen.Text = JRest.ToString();
                TBL_BHGreen.Text = "Before Halloween";
                TBL_DGreen.Text = "Days";
                
                // Si l'utilisateur à rentré son nom et son prénom on l'affiche dans le text block prévu à cet effet
                if (string.IsNullOrEmpty(prenom) || string.IsNullOrEmpty(nom))
                {
                    UserGreen.Text = "";
                }
                else
                {
                    UserGreen.Text = $"{prenom} {nom}";

                }

            }
            // Si la couleur prédéfini par l'utilisateur est rouge
            else if (Color == "Red")
            {
                // On change le background de la page countdown
                Uri Red = new Uri("..\\..\\..\\Ressources\\Background\\Background_Skull.jpg", UriKind.Relative);
                BK_Countdown.ImageSource = new BitmapImage(Red);

                // On change le text affiché dans la page countdown
                TBL_NbrJRed.Text = JRest.ToString();
                TBL_BHRed.Text = "Before Halloween";
                TBL_DRed.Text = "Days";

                // Si l'utilisateur à rentré son nom et son prénom on l'affiche dans le text block prévu à cet effet
                if (string.IsNullOrEmpty(prenom) || string.IsNullOrEmpty(nom))
                {
                    TBL_UserRed.Text = "";
                }
                else
                {
                    TBL_UserRed.Text = $"Content de vous revoir {prenom} {nom} !";
                }

            }

            // Code qui permet de créer un nouvel indice chaque jour
            // Si la varible "JRest" est égal à la variable "Jafter" qui prends la valeur du jour d'après
            if (JRest == int.Parse(Jafter))
            {
                // On enlève 1 à la varible "Jafter"
                Jafter = (int.Parse(Jafter) - 1).ToString();
                // On enregistre cette variable
                configuration.AppSettings.Settings.Remove("JAfter");
                configuration.AppSettings.Settings.Add("JAfter", Jafter);

                // On créé un nouveau random
                Random rand = new Random();
                // La variable "indice" prends un valeur aléatoire entre 1 et 10 (nombres de cartes)
                int indice = rand.Next(1, 10);
                // On sauvergarde cet indice pour garder le même chaque jour
                configuration.AppSettings.Settings.Remove("Indice");
                configuration.AppSettings.Settings.Add("Indice", indice.ToString());
                string Indice = ConfigurationManager.AppSettings["Indice"]; // Vu que le nombre de jour avant halloween est élevé il est possible de tomber plusieurs fois sur la même carte

                configuration.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }
      
    }
}
