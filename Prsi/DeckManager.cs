using System.Collections.Generic;
using System.Windows;

namespace Prsi
{
    public class DeckManager
    {
        private MainWindow instance;

        public DeckManager(MainWindow instance)
        {
            this.instance = instance;
        }
        
        // Decks of players
        private List<Card> yourDeck = new List<Card>();
        private List<Card> opponentsDeck = new List<Card>();

        // Adding card to player method
        public void addToYour(Card card)
        {
            yourDeck.Add(card);
        }
        
        // Adding card to bot method
        public void addToOpponent(Card card)
        {
            opponentsDeck.Add(card);
        }
        
        // Removing card from player method
        public void removeFromYour(Card card)
        {
            yourDeck.Remove(card);
        }
        
        // Removing card from bot method
        public void removeFromOpponent(Card card)
        {
            opponentsDeck.Remove(card);
        }
        
        // Getting deck of player method
        public List<Card> getYour()
        {
            return yourDeck;
        }
        
        // Getting deck of bot method
        public List<Card> getOpponents()
        {
            return opponentsDeck;
        }
    }
}