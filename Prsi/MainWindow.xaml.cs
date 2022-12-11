using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Prsi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow
    {

        // Variables of managers
        private ComponentManager cm;
        private DeckManager dm;
        private GameManager gm;

        // DeckManager getter
        public ComponentManager getCm()
        {
            return cm;
        }
        
        // DeckManager getter
        public DeckManager getDm()
        {
            return dm;
        }
        
        // GameManager getter
        public GameManager getGm()
        {
            return gm;
        }


        public MainWindow()
        {
            dm = new DeckManager(instance:this); // Init DeckManager
            cm = new ComponentManager(instance:this); // Init ComponentManager
            gm = new GameManager(instance:this); // Init GameManager
            
            InitializeComponent();
            
            gm.initCards(); // Init cards
            gm.initCardPack(); // Init decks of players
            gm.startGame(); // Start game

            // Debugging errors
            //gm.showChangerSelector();
            //gm.hideChangerSelector();
        }
        
        private void cardClick(object sender, MouseEventArgs e)
        {
            // Get clicked element
            FrameworkElement elem = (FrameworkElement)e.OriginalSource;
            
            // If element is image
            if (elem.GetType().ToString().Equals("System.Windows.Controls.Image"))
            {
                // Run give away method
                gm.giveAwayCard(gm.getCardByName(elem.Name), elem);
            }
            
        }

        private void cardHover(object sender, MouseEventArgs e)
        {
            // Get hover element
            FrameworkElement elem = (FrameworkElement)e.OriginalSource;
            
            // If element is image
            if (elem.GetType().ToString().Equals("System.Windows.Controls.Image"))
            {
                // Run give away method
                elem.Height *= 0.5;
                elem.Width *= 0.5;
            }
        }

        // On load of window
        private void Window_Loaded(Object sender, RoutedEventArgs e)
        {
            // Create new timer and start it (1 second interval)
            DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        // Let 3 variables
        private int timeElapsed = 0;
        private int toElapse = -1;
        Random rand = new Random();
        
        // Every tick
        private void timerTick(object sender, EventArgs e)
        {
            if (toElapse <= 0) toElapse = rand.Next(6); // If toElapse is not set -> set to random num (0-5)
            if (timeElapsed >= toElapse) // If specific time elapsed
            {
                toElapse = rand.Next(6); // Set to random num (0-5)
                timeElapsed = 0; // Reset time elapsed var

                if (gm.getTurn() == Turn.OPPONENT && !gm.isSelecting())
                {
                    int givenAway = 0; // Let variable
                    foreach (Card card in dm.getOpponents()) // Foreach all opponents cards
                    {
                        if (gm.botGiveAway(card)) // If bot has card to play
                        {
                            givenAway++; // Increase variable
                            gm.setTurn(Turn.YOUR);
                            break; // Stop loop (foreach)
                        }
                    }

                    if (givenAway == 0) gm.botTakeCard();
                    gm.setTurn(Turn.YOUR); // If bot has no card to play -> take card from pack
                }
            }
            timeElapsed++; // Increase time elapsed variable by 1
            //Debug.WriteLine("Turn: " + gm.getTurn());

            CommandManager.InvalidateRequerySuggested();
        }

    }
}