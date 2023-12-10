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
using System.Windows.Threading;
using System.Diagnostics;
using System.Configuration;
using System.Windows.Controls.Primitives;
using System.Threading;

namespace Halloween.Pages
{
    /// <summary>
    /// Logique d'interaction pour Game.xaml
    /// </summary>
    public partial class Game : UserControl
    {
        // Ouvre le fichier de configuration qui s’applique à tous les utilisateurs défini sur userLevel.None
        Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        // On créer 2 Timer pour notre loopGame et notre loopLife grâce au constructeur "DispatcherTimer"
        DispatcherTimer gameTimer = new DispatcherTimer();
        DispatcherTimer lifeTimer = new DispatcherTimer();
        // Variable de type bool qui verifie si notre player se déplace à gauche ou à droite
        bool moveLeft;
        bool moveRight;
        // On créer une liste de class Rectangle "itemsRemover" qui va nous servir à retirer des items
        List<Rectangle> itemRemover = new List<Rectangle>();
        // On créer une nouvelle variable "random" grâce au constructeur "Random" qui va nous servir à choisir un enemie au hasard
        Random random = new Random();
        // Variable de type char qui verifie et se souviens de la direction de notre player
        char direction;
        // Lit les paramètres de l’application pour obtenir la valeur de la clés "score" et l'attribuer à la varible "Mscore" de type string
        // Cette variable nous sert à retenir le meilleur score
        string Mscore = ConfigurationManager.AppSettings["score"];
        // Varible qui va nous servir à compter le nombre d'enemie différent
        int enemySpriteCounter = 0;
        // Variable qui compte le nombre d'enemie
        int enemyCounter = 100;
        // Variable qui défini la vitesse de notre player
        int playerSpeed = 10;
        // Variable qui défini la vitesse des enemies
        int enemySpeed = 5;
        // Variable qui défini le score du joueur
        int score = 0;
        // Variable qui défini la vie du joueur 
        int vie = 100;
        // Variable qui défini la limite d'enemie que l'on fera apparaitre 
        int limit = 50;
        // On créer une nouvelle variable de type rectangle qui va définir la hitbox de notre joueur
        Rect playerHitBox;

        public Game()
        {
            InitializeComponent();
            // On affiche le meilleur score dans le Textblock meilleur score
            TB_MScore.Text = "Best score: " + Mscore;
            // On initialise l'intervale de temps entre chaque "Tick", ici il est dde 20 milisecondes
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            // Grace à la méthode "EventHandler" on va appeler la voie "Gameloop" à chaque tick
            gameTimer.Tick += GameLoop;
            // On défini le focus logique et de notre clavier sur notre Canvas gâce à la méthode "Focus"
            SpielCanvas.Focus();
            // On lance notre timer "gameTimer" ce qui fait que notre jeu se lance quand on affiche la page game
            gameTimer.Start();

            // Même fonctionnement que pour notre "gametimer"
            // Ce timer nous servira à creer et faire spawn des items "vie" toute les 30 secondes
            lifeTimer.Interval = TimeSpan.FromSeconds(30);
            lifeTimer.Tick += Makelife;
            lifeTimer.Start();

            // On créer un nouvel imagebrush "playerImage" grâce au constructeur "ImageBrush" qui va nous servir à mettre une image sur notre rectanfle "Player"
            ImageBrush playerImage = new ImageBrush();
            // On va chercher la source de l'image en créant une nouvelle bitmap image lié à l'URI qui prend la valeur du chemin d'accès de notre image
            playerImage.ImageSource = new BitmapImage(new Uri("..\\..\\..\\Ressources\\Game\\SorciereLeft.png", UriKind.Relative));
            // On remplit notre rectangle "PLayer" avec cette image
            Player.Fill = playerImage;
        }

        // Voie privée qui représente la loop de notre jeu
        private void GameLoop(object sender, EventArgs e)
        {
            // On définie les valeurs de notre hitboxplayer (ou elle apparait et son hauteur et largeur) 
            // Ici elle prends les valeurs de notre rectangle player
            playerHitBox = new Rect(Canvas.GetLeft(Player), Canvas.GetTop(Player), Player.Width, Player.Height);
            // On enlève 1 au compteur d'enemie car on en créé un nouveau
            enemyCounter -= 1;
            // On affiche le score et la vie dans nos labels en fonction des valeurs des variables "score" et "vie"
            LB_Score.Content = "Score: " + score;
            LB_Life.Content = "Vie: " + vie;
            // Si le compteur d'enemie n'est pas à 0
            if (enemyCounter < 0)
            {
                // On va chercher la class "MakeEnemie" qui va creer de nouveau enemies
                MakeEnemies();
                // Le compteur d'enemie prends la valeur de la limit d'enemie
                // Cela nous permet de controler le nombre d'enemie que l'on shouaite faire apparaitre
                enemyCounter = limit;
            }
            // Si la variable "moveleft" renvoie la valeur true et que le player n'est pas au bout gauche de notre canvas
            if (moveLeft == true && Canvas.GetLeft(Player) > 0)
            {
                // Grâce à la méthode "Canvas.Setleft" on définit la valeur de la propriété attachée à Left pour un l'objet "player"
                // Ici on bouge notre "Player" à sa gauche et on soustrait sa vitesse pour le bouger à la vitesse désiré
                Canvas.SetLeft(Player, Canvas.GetLeft(Player) - playerSpeed);
                // On change l'image brush de notre player par l'image de la socière allant à gauche
                ImageBrush playerImage = new ImageBrush();
                playerImage.ImageSource = new BitmapImage(new Uri("..\\..\\..\\Ressources\\Game\\SorciereLeft.png", UriKind.Relative));
                Player.Fill = playerImage;
                // La varible direction prends la valeur 'L' pour left
                direction = 'L';
            }
            // Même principe mais pour aller à gauche
            if (moveRight == true && Canvas.GetLeft(Player) + 90 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(Player, Canvas.GetLeft(Player) + playerSpeed);

                ImageBrush playerImage = new ImageBrush();
                playerImage.ImageSource = new BitmapImage(new Uri("..\\..\\..\\Ressources\\Game\\SorciereRight.png", UriKind.Relative));
                Player.Fill = playerImage;

                direction = 'R';

            }
            // Pour chaque variable "x" de type  rectangle de notre canvas
            foreach (var x in SpielCanvas.Children.OfType<Rectangle>())
            {
                // Si "x" est un rectangle et que son tag est "bullet" 
                if (x is Rectangle && (string)x.Tag == "bullet")
                {
                    // On fait apparaitre notre bullet à la position du rectangle bullet moins 15 pixels
                    Canvas.SetTop(x, Canvas.GetTop(x) - 15);
                    // On créer un nouveau rectangle "bullethitbox" qui définira la hitbox de notre balle
                    // Cette hitbox apparait à lendroit de notre variable x (donc notre balle) et prends sa largeur et sa longueur
                    Rect bulletHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    // Si la position top de notre balle est inferieur à 10 pixels
                    if (Canvas.GetTop(x) < 10)
                    {
                        // On ajoute cette balle à notre liste "itemsRemover"
                        itemRemover.Add(x);
                    }
                    // Pour chaque variable "y" de type liste rectangle de notre canvas ("SpielCanvas" est le nom de notre canvas)
                    foreach (var y in SpielCanvas.Children.OfType<Rectangle>())
                    {
                        // Même fonctionnement que pour les balles
                        if (y is Rectangle && (string)y.Tag == "enemy")
                        {
                            Rect enemyHit = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);
                            // Si la hitbox "bulletHitbox" entre en contact avec la hitbox "enemyHit"
                            if (bulletHitbox.IntersectsWith(enemyHit))
                            {
                                // On les ajoutent à notre liste "itemsRemover"
                                itemRemover.Add(x);
                                itemRemover.Add(y);
                                // On ajoute +10 au score
                                score = score + 10;
                            }
                        }
                    }
                }
                // Si "x" est un rectangle et que son tag est "enemy" 
                if (x is Rectangle && (string)x.Tag == "enemy")
                {
                    {
                        // Grâce à la méthode "Canvas.SetTop" on définit la valeur de la propriété attachée à Top pour un l'objet "enemy"
                        // Ici on bouge notre "enemy" en bas et on ajoute sa vitesse pour le bouger à la vitesse désirér
                        Canvas.SetTop(x, Canvas.GetTop(x) + enemySpeed);
                    }
                    // Si l'enemy arrive en bas de notre canvas
                    if (Canvas.GetTop(x) > 620)
                    {
                        // On l'ajoute à notre liste "itemsRemover"
                        itemRemover.Add(x);
                        // On enleve 10 à la vie
                        vie -= 10;
                    }
                    // On créer la hitbox des enemies
                    Rect enemyHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    // Si la hitbox "playerHitBox" entre en contact avec la hitbox "enemyHitbox"
                    if (playerHitBox.IntersectsWith(enemyHitbox))
                    {
                        // On l'ajoute à notre liste "itemsRemover"
                        itemRemover.Add(x);
                        // On enleve 5 à la vie
                        vie -= 5;
                    }

                }
                // Même principe pour les vie sauf que l'on ajoute 10 à la varible "vie" si elle rentre en contact avec la hitboxplayer
                if (x is Rectangle && (string)x.Tag == "life")
                {
                    {
                        Canvas.SetTop(x, Canvas.GetTop(x) + enemySpeed);
                    }

                    if (Canvas.GetTop(x) > 620)

                    {
                        itemRemover.Add(x);
                    }

                    Rect lifeHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox.IntersectsWith(lifeHitbox))
                    {
                        itemRemover.Add(x);
                        vie += 10;
                    }

                }
                // Si le score est plus grand que la variable Mscore convertit en int
                if (score > int.Parse(Mscore))
                {
                    // On supprime l'ancienne valeur de l'appsettings "score" pour la remplacer par le score du joueur
                    configuration.AppSettings.Settings.Remove("score");
                    configuration.AppSettings.Settings.Add("score", score.ToString());
                    // La varible "Mscore" prends cette valeur
                    string Mscore = ConfigurationManager.AppSettings["score"];
                    // On sauvegarde les modifications
                    configuration.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                    // On affiche le meilleur score dans le Textblock
                    TB_MScore.Text = "Best score: " + Mscore;
                }
            }
            // Pour châque rectangle "i" dans la liste "itemRemover"
            foreach (Rectangle i in itemRemover)
            {
                // On supprime ces rectangle du canvas grâce à la méthode "Remove"
                SpielCanvas.Children.Remove(i);
            }
            // instruction switch en fonction du score
            switch (score)
            {
                // Si le score vaut 100 on passe au level2
                case 100:
                    LB_Lvl.Content = "Level 2";
                    // On augmente la vitesse des enemies
                    enemySpeed = 7;
                    // On augmente la vitesse du player
                    playerSpeed = 15;
                    // On réduit la limite ce qui fait que plus d'enemies von apparaitre
                    limit = 30;
                    break;

                case 300:
                    LB_Lvl.Content = "Level 3";
                    limit = 20;
                    playerSpeed = 15;
                    break;

                case 600:
                    LB_Lvl.Content = "Level 4";
                    enemySpeed = 10;
                    limit = 15;
                    playerSpeed = 20;
                    break;

                case 800:
                    LB_Lvl.Content = "Level 5";
                    limit = 20;
                    enemySpeed = 12;
                    playerSpeed = 20;
                    break;

                case 1000:
                    LB_Lvl.Content = "Level 6";
                    limit = 18;
                    enemySpeed = 13;
                    playerSpeed = 25;
                    break;

                case 1300:
                    LB_Lvl.Content = "Level 7";
                    limit = 15;
                    enemySpeed = 14;
                    playerSpeed = 25;
                    break;

                case 1600:
                    LB_Lvl.Content = "Level 8";
                    limit = 12;
                    enemySpeed = 15;
                    playerSpeed = 30;
                    break;

                case 1800:
                    LB_Lvl.Content = "Level 8";
                    limit = 10;
                    enemySpeed = 16;
                    playerSpeed = 30;
                    break;
            }
            // Si la varible "vie" tombe à 0
            if (vie <= 0)
            {
                // On arrète les timer
                 gameTimer.Stop();
                lifeTimer.Stop();
                // Le label Life devient rouge
                LB_Life.Foreground = Brushes.Red;
                // On affiche un messagebox qui nous demande si on veut recommencer le jeu
                string messageBoxText = "Recommencer?";
                // On affiche GAME OVER sur la barre de notre message box
                string caption = "GAME OVER";
                // On affiche 2 boutons "Yes" et "No"
                MessageBoxButton button = MessageBoxButton.YesNo;
                // ON n'affiche aucune icone
                MessageBoxImage icon = MessageBoxImage.None;
                // La varible "result" affiche notre messagebox avec tous ces paramètres
                MessageBoxResult result;
                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
                // Instruction switch selon le result
                switch (result)
                {
                    // Si le joueur appuie sur "Yes"
                    case MessageBoxResult.Yes:
                        // Appel de la fonction GameReset
                        GameReset();
                        break;
                    // Si le joueur appuie sur "No"
                    case MessageBoxResult.No:
                        // On relance l'application
                        var currentExecutablePath = Process.GetCurrentProcess().MainModule.FileName;
                        Process.Start(currentExecutablePath);
                        System.Windows.Application.Current.Shutdown(); break;
                        break;
                }

            }
          
        }
        // Voie privée GameReset qui nous sert à relancer le jeu
        private void GameReset()
        {
            // On remet le texte du label Vie en blanc 
            LB_Life.Foreground = Brushes.White;
            // On réafiche le level 1
            LB_Lvl.Content = "Level 1";
            // On relance les timers
            gameTimer.Start();
            lifeTimer.Start();
            // On réinitialise les variables
            enemySpriteCounter = 0;
            enemyCounter = 100;
            playerSpeed = 10;
            enemySpeed = 5;
            score = 0;
            vie = 100;
            limit = 50;
            // Pour chaque variable y du canvas de type rectangle
            foreach (var y in SpielCanvas.Children.OfType<Rectangle>())
            {
                // Si y est un rectangle et que son tag est "enemy" ou "life"
                if (y is Rectangle && (string)y.Tag == "enemy" || (string)y.Tag == "life")
                {
                    // On l'ajoute à la liste "itemRemover"
                     itemRemover.Add(y);             
                }
            }

        }
        // Voie privée lorsque l'on appuie sur une touche du clavier
        // "KeyEventArgs" est un constructeur qui initialise une nouvelle instance de la classe KeyEventArgs
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            // On supprime le texte qui affiche les regles du jeu
            TB_Rule.Text = "";
            // Si la touche du clavier est la touche gauche 
            if (e.Key == Key.Left)
            {
                // La variable "moveLeft" prends la valeur "true"
                moveLeft = true;
            }
            // Si la touche du clavier est la touche droite 
            if (e.Key == Key.Right)
            {
                // La variable "moveRight" prends la valeur "true"
                moveRight = true;
            }
        }
        // Voie privée lorsque l'on relache une touche du clavier
        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            // Même principe sauf que les variables prennent la valeur "false"
            if (e.Key == Key.Left)
            {
                moveLeft = false;
            }
            if (e.Key == Key.Right)
            {
                moveRight = false;
            }
            // Si la touche du clavier est la touche espace
            if (e.Key == Key.Space)
            {
                // On creer un nouveau rectangle "nexBullet" 
                Rectangle newBullet = new Rectangle
                {
                    // Son tag sera donc "bullet"
                    Tag = "bullet",
                    // Sa hauteur sera de 2O pixels
                    Height = 20,
                    // Sa largeur est de 7 pixels
                    Width = 7,
                    // On remplis ce retangle avec la couleur blanche 
                    Fill = Brushes.White,
                    // Le contour de ce rectangle sera rouge
                    Stroke = Brushes.Red

                };
                // Si la variable direction prends la valeur 'R'
                if (direction == 'R')
                {
                    // On creer la balle à la droite du rectangle player (meême principe que pour les autres rectangles)
                    Canvas.SetLeft(newBullet, Canvas.GetLeft(Player) + Player.ActualWidth);
                    Canvas.SetTop(newBullet, Canvas.GetTop(Player));
                    SpielCanvas.Children.Add(newBullet);
                }
                // Sinon on creer la balle à la gauche du rectangle player
                else
                {
                    Canvas.SetLeft(newBullet, Canvas.GetLeft(Player));
                    Canvas.SetTop(newBullet, Canvas.GetTop(Player));
                    SpielCanvas.Children.Add(newBullet);
                }
            }
        }
        // Voie privée "MakeEnemies"
        private void MakeEnemies()
        {
            // On créer un nouvel imagebrush "enemySprite" grâce au constructeur "ImageBrush"
            ImageBrush enemySprite = new ImageBrush();
            // La variable "enemySpriteCounter" prends une valeur au hasard entre 1 et 5
            enemySpriteCounter = random.Next(1, 5);
            // Instruction switch en fonction du "enemySpriteCounter"
            switch (enemySpriteCounter)
            {
                // Selon sa valeur on va afficher différente image pour les enemies en allant chercher différent chemin d'accès
                case 1:
                    enemySprite.ImageSource = new BitmapImage(new Uri("..\\..\\..\\Ressources\\Game\\Loup_garou.png", UriKind.Relative));
                    break;

                case 2:
                    enemySprite.ImageSource = new BitmapImage(new Uri("..\\..\\..\\Ressources\\Game\\Demon.png", UriKind.Relative));
                    break;

                case 3:
                    enemySprite.ImageSource = new BitmapImage(new Uri("..\\..\\..\\Ressources\\Game\\Momie.png", UriKind.Relative));
                    break;

                case 4:
                    enemySprite.ImageSource = new BitmapImage(new Uri("..\\..\\..\\Ressources\\Game\\Frank.png", UriKind.Relative));
                    break;

                case 5:
                    enemySprite.ImageSource = new BitmapImage(new Uri("..\\..\\..\\Ressources\\Game\\Zombie.png", UriKind.Relative));
                    break;
            }
            // On créé un nouveau rectangle "newEnemy" et on remplit notre rectangle avec cette image
            Rectangle newEnemy = new Rectangle
            {
                Tag = "enemy",
                Height = 80,
                Width = 85,
                Fill = enemySprite,
            };
            // Les rectangle "newEnemy" apparaitront en haut de notre canvas et aléatoirement de 30 à 450 pixels de gauche à droite
            Canvas.SetTop(newEnemy, 0);
            Canvas.SetLeft(newEnemy, random.Next(30, 450));
            SpielCanvas.Children.Add(newEnemy);
        }
        // Voie privée "Makelife" qui nous servira à creer des vies
        private void Makelife(object sender, EventArgs e)
        {
            // Mâme principe que pour nos enemies sauf qu'ici nous n'avons qu'une seule image
            ImageBrush LifeSprite = new ImageBrush();
        
            LifeSprite.ImageSource = new BitmapImage(new Uri("..\\..\\..\\Ressources\\Game\\Life.png", UriKind.Relative));
               
            Rectangle newLife = new Rectangle
            {
                Tag = "life",
                Height = 50,
                Width = 50,
                Fill = LifeSprite,
            };

            Canvas.SetTop(newLife, 0);
            Canvas.SetLeft(newLife, random.Next(30, 450));
            SpielCanvas.Children.Add(newLife);
        }
      
    }
}
