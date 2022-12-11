using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Path = System.Windows.Shapes.Path;

namespace Prsi
{
    public enum Turn
    {
        YOUR,
        OPPONENT
    }
    
    public class GameManager
    {
        private MainWindow instance;

        public GameManager(MainWindow instance)
        {
            this.instance = instance;
            cardList = new List<Card>();
            givenAway = new List<Card>();
            remainingCardList = new List<Card>();
            lastGivenAway = null;
            rand = new Random();
            turn = Turn.OPPONENT;
            selecting = false;
        }

        // Create variables
        private List<Card> cardList;
        private List<Card> givenAway;
        private List<Card> remainingCardList;
        private Card lastGivenAway;
        private Random rand;
        private Turn turn;
        private bool selecting;

        // isSelecting getter
        public bool isSelecting()
        {
            return selecting;
        }
        
        // Init cards
        public void initCards()
        {
            string path = "C:\\Users\\dodis\\RiderProjects\\Prsi\\Prsi\\Cards\\";
            cardList.Add(new Card(Cards.BALLS_SEVEN, "Kulová sedma", path + "BALLS_SEVEN.png", ChangerType.NONE, new Cards[] { Cards.BALLS_SEVEN, Cards.BALLS_EIGHT, Cards.BALLS_NINE, Cards.BALLS_TEN, Cards.BALLS_LOWER, Cards.BALLS_CHANGER, Cards.BALLS_KING, Cards.BALLS_STOP, Cards.HEARTS_SEVEN, Cards.ACORN_SEVEN, Cards.LEAVES_SEVEN, Cards.HEARTS_CHANGER, Cards.ACORN_CHANGER, Cards.LEAVES_CHANGER }.ToList()));
            cardList.Add(new Card(Cards.BALLS_EIGHT, "Kulová osma", path + "BALLS_EIGHT.png", ChangerType.NONE, new Cards[] { Cards.BALLS_SEVEN, Cards.BALLS_EIGHT, Cards.BALLS_NINE, Cards.BALLS_TEN, Cards.BALLS_LOWER, Cards.BALLS_CHANGER, Cards.BALLS_KING, Cards.BALLS_STOP, Cards.HEARTS_EIGHT, Cards.ACORN_EIGHT, Cards.LEAVES_EIGHT, Cards.HEARTS_CHANGER, Cards.ACORN_CHANGER, Cards.LEAVES_CHANGER }.ToList()));
            cardList.Add(new Card(Cards.BALLS_NINE, "Kulová devítka", path + "BALLS_NINE.png", ChangerType.NONE, new Cards[] { Cards.BALLS_SEVEN, Cards.BALLS_EIGHT, Cards.BALLS_NINE, Cards.BALLS_TEN, Cards.BALLS_LOWER, Cards.BALLS_CHANGER, Cards.BALLS_KING, Cards.BALLS_STOP, Cards.HEARTS_NINE, Cards.ACORN_NINE, Cards.LEAVES_NINE, Cards.HEARTS_CHANGER, Cards.ACORN_CHANGER, Cards.LEAVES_CHANGER }.ToList()));
            cardList.Add(new Card(Cards.BALLS_TEN, "Kulová desítka", path + "BALLS_TEN.png", ChangerType.NONE, new Cards[] { Cards.BALLS_SEVEN, Cards.BALLS_EIGHT, Cards.BALLS_NINE, Cards.BALLS_TEN, Cards.BALLS_LOWER, Cards.BALLS_CHANGER, Cards.BALLS_KING, Cards.BALLS_STOP, Cards.HEARTS_TEN, Cards.ACORN_TEN, Cards.LEAVES_TEN, Cards.HEARTS_CHANGER, Cards.ACORN_CHANGER, Cards.LEAVES_CHANGER }.ToList()));
            cardList.Add(new Card(Cards.BALLS_LOWER, "Kulový spodek", path + "BALLS_LOWER.png", ChangerType.NONE, new Cards[] { Cards.BALLS_SEVEN, Cards.BALLS_EIGHT, Cards.BALLS_NINE, Cards.BALLS_TEN, Cards.BALLS_LOWER, Cards.BALLS_CHANGER, Cards.BALLS_KING, Cards.BALLS_STOP, Cards.HEARTS_LOWER, Cards.ACORN_LOWER, Cards.LEAVES_LOWER, Cards.HEARTS_CHANGER, Cards.ACORN_CHANGER, Cards.LEAVES_CHANGER }.ToList()));
            cardList.Add(new Card(Cards.BALLS_CHANGER, "Kulový měnič", path + "BALLS_CHANGER.png", ChangerType.NONE, new Cards[]
            {
                Cards.BALLS_SEVEN, Cards.BALLS_EIGHT, Cards.BALLS_NINE, Cards.BALLS_TEN, Cards.BALLS_LOWER,
                Cards.BALLS_CHANGER, Cards.BALLS_KING, Cards.BALLS_STOP, Cards.HEARTS_CHANGER,
                Cards.ACORN_CHANGER, Cards.LEAVES_CHANGER
            }.ToList()));
            cardList.Add(new Card(Cards.BALLS_KING, "Kulový král", path + "BALLS_KING.png", ChangerType.NONE, new Cards[] { Cards.BALLS_SEVEN, Cards.BALLS_EIGHT, Cards.BALLS_NINE, Cards.BALLS_TEN, Cards.BALLS_LOWER, Cards.BALLS_CHANGER, Cards.BALLS_KING, Cards.BALLS_STOP, Cards.HEARTS_KING, Cards.ACORN_KING, Cards.LEAVES_KING, Cards.HEARTS_CHANGER, Cards.ACORN_CHANGER, Cards.LEAVES_CHANGER }.ToList()));
            cardList.Add(new Card(Cards.BALLS_STOP, "Kulové eso", path + "BALLS_STOP.png", ChangerType.NONE, new Cards[] { Cards.BALLS_SEVEN, Cards.BALLS_EIGHT, Cards.BALLS_NINE, Cards.BALLS_TEN, Cards.BALLS_LOWER, Cards.BALLS_CHANGER, Cards.BALLS_KING, Cards.BALLS_STOP, Cards.HEARTS_STOP, Cards.ACORN_STOP, Cards.LEAVES_STOP, Cards.HEARTS_CHANGER, Cards.ACORN_CHANGER, Cards.LEAVES_CHANGER }.ToList()));
            
            cardList.Add(new Card(Cards.HEARTS_SEVEN, "Srdcová sedma", path + "HEARTS_SEVEN.png", ChangerType.NONE, new Cards[] { Cards.HEARTS_SEVEN, Cards.HEARTS_EIGHT, Cards.HEARTS_NINE, Cards.HEARTS_TEN, Cards.HEARTS_LOWER, Cards.HEARTS_CHANGER, Cards.HEARTS_KING, Cards.HEARTS_STOP, Cards.BALLS_SEVEN, Cards.ACORN_SEVEN, Cards.LEAVES_SEVEN, Cards.BALLS_CHANGER, Cards.ACORN_CHANGER, Cards.LEAVES_CHANGER }.ToList()));
            cardList.Add(new Card(Cards.HEARTS_EIGHT, "Srdcová osma", path + "HEARTS_EIGHT.png", ChangerType.NONE, new Cards[] { Cards.HEARTS_SEVEN, Cards.HEARTS_EIGHT, Cards.HEARTS_NINE, Cards.HEARTS_TEN, Cards.HEARTS_LOWER, Cards.HEARTS_CHANGER, Cards.HEARTS_KING, Cards.HEARTS_STOP, Cards.BALLS_EIGHT, Cards.ACORN_EIGHT, Cards.LEAVES_EIGHT, Cards.BALLS_CHANGER, Cards.ACORN_CHANGER, Cards.LEAVES_CHANGER }.ToList()));
            cardList.Add(new Card(Cards.HEARTS_NINE, "Srdcová devítka", path + "HEARTS_NINE.png", ChangerType.NONE, new Cards[] { Cards.HEARTS_SEVEN, Cards.HEARTS_EIGHT, Cards.HEARTS_NINE, Cards.HEARTS_TEN, Cards.HEARTS_LOWER, Cards.HEARTS_CHANGER, Cards.HEARTS_KING, Cards.HEARTS_STOP, Cards.BALLS_NINE, Cards.ACORN_NINE, Cards.LEAVES_NINE, Cards.BALLS_CHANGER, Cards.ACORN_CHANGER, Cards.LEAVES_CHANGER }.ToList()));
            cardList.Add(new Card(Cards.HEARTS_TEN, "Srdcová desítka", path + "HEARTS_TEN.png", ChangerType.NONE, new Cards[] { Cards.HEARTS_SEVEN, Cards.HEARTS_EIGHT, Cards.HEARTS_NINE, Cards.HEARTS_TEN, Cards.HEARTS_LOWER, Cards.HEARTS_CHANGER, Cards.HEARTS_KING, Cards.HEARTS_STOP, Cards.BALLS_TEN, Cards.ACORN_TEN, Cards.LEAVES_TEN, Cards.BALLS_CHANGER, Cards.ACORN_CHANGER, Cards.LEAVES_CHANGER }.ToList()));
            cardList.Add(new Card(Cards.HEARTS_LOWER, "Srdcový spodek", path + "HEARTS_LOWER.png", ChangerType.NONE, new Cards[] { Cards.HEARTS_SEVEN, Cards.HEARTS_EIGHT, Cards.HEARTS_NINE, Cards.HEARTS_TEN, Cards.HEARTS_LOWER, Cards.HEARTS_CHANGER, Cards.HEARTS_KING, Cards.HEARTS_STOP, Cards.BALLS_LOWER, Cards.ACORN_LOWER, Cards.LEAVES_LOWER, Cards.BALLS_CHANGER, Cards.ACORN_CHANGER, Cards.LEAVES_CHANGER }.ToList()));
            cardList.Add(new Card(Cards.HEARTS_CHANGER, "Srdcový měnič", path + "HEARTS_CHANGER.png", ChangerType.NONE, new Cards[]
            {
                Cards.HEARTS_SEVEN, Cards.HEARTS_EIGHT, Cards.HEARTS_NINE, Cards.HEARTS_TEN, Cards.HEARTS_LOWER,
                Cards.HEARTS_CHANGER, Cards.HEARTS_KING, Cards.HEARTS_STOP, Cards.BALLS_CHANGER,
                Cards.ACORN_CHANGER, Cards.LEAVES_CHANGER
            }.ToList()));
            cardList.Add(new Card(Cards.HEARTS_KING, "Srdcový král", path + "HEARTS_KING.png", ChangerType.NONE, new Cards[] { Cards.HEARTS_SEVEN, Cards.HEARTS_EIGHT, Cards.HEARTS_NINE, Cards.HEARTS_TEN, Cards.HEARTS_LOWER, Cards.HEARTS_CHANGER, Cards.HEARTS_KING, Cards.HEARTS_STOP, Cards.BALLS_KING, Cards.ACORN_KING, Cards.LEAVES_KING, Cards.BALLS_CHANGER, Cards.ACORN_CHANGER, Cards.LEAVES_CHANGER }.ToList()));
            cardList.Add(new Card(Cards.HEARTS_STOP, "Srdcové eso", path + "HEARTS_STOP.png", ChangerType.NONE, new Cards[] { Cards.HEARTS_SEVEN, Cards.HEARTS_EIGHT, Cards.HEARTS_NINE, Cards.HEARTS_TEN, Cards.HEARTS_LOWER, Cards.HEARTS_CHANGER, Cards.HEARTS_KING, Cards.HEARTS_STOP, Cards.BALLS_STOP, Cards.ACORN_STOP, Cards.LEAVES_STOP, Cards.BALLS_CHANGER, Cards.ACORN_CHANGER, Cards.LEAVES_CHANGER }.ToList()));
            
            cardList.Add(new Card(Cards.LEAVES_SEVEN, "Zelená sedma", path + "LEAVES_SEVEN.png", ChangerType.NONE, new Cards[] { Cards.LEAVES_SEVEN, Cards.LEAVES_EIGHT, Cards.LEAVES_NINE, Cards.LEAVES_TEN, Cards.LEAVES_LOWER, Cards.LEAVES_CHANGER, Cards.LEAVES_KING, Cards.LEAVES_STOP, Cards.BALLS_SEVEN, Cards.ACORN_SEVEN, Cards.HEARTS_SEVEN, Cards.BALLS_CHANGER, Cards.ACORN_CHANGER, Cards.HEARTS_CHANGER }.ToList()));
            cardList.Add(new Card(Cards.LEAVES_EIGHT, "Zelená osma", path + "LEAVES_EIGHT.png", ChangerType.NONE, new Cards[] { Cards.LEAVES_SEVEN, Cards.LEAVES_EIGHT, Cards.LEAVES_NINE, Cards.LEAVES_TEN, Cards.LEAVES_LOWER, Cards.LEAVES_CHANGER, Cards.LEAVES_KING, Cards.LEAVES_STOP, Cards.BALLS_EIGHT, Cards.ACORN_EIGHT, Cards.HEARTS_EIGHT, Cards.BALLS_CHANGER, Cards.ACORN_CHANGER, Cards.HEARTS_CHANGER }.ToList()));
            cardList.Add(new Card(Cards.LEAVES_NINE, "Zelená devítka", path + "LEAVES_NINE.png", ChangerType.NONE, new Cards[] { Cards.LEAVES_SEVEN, Cards.LEAVES_EIGHT, Cards.LEAVES_NINE, Cards.LEAVES_TEN, Cards.LEAVES_LOWER, Cards.LEAVES_CHANGER, Cards.LEAVES_KING, Cards.LEAVES_STOP, Cards.BALLS_NINE, Cards.ACORN_NINE, Cards.HEARTS_NINE, Cards.BALLS_CHANGER, Cards.ACORN_CHANGER, Cards.HEARTS_CHANGER }.ToList()));
            cardList.Add(new Card(Cards.LEAVES_TEN, "Zelená desítka", path + "LEAVES_TEN.png", ChangerType.NONE, new Cards[] { Cards.LEAVES_SEVEN, Cards.LEAVES_EIGHT, Cards.LEAVES_NINE, Cards.LEAVES_TEN, Cards.LEAVES_LOWER, Cards.LEAVES_CHANGER, Cards.LEAVES_KING, Cards.LEAVES_STOP, Cards.BALLS_TEN, Cards.ACORN_TEN, Cards.HEARTS_TEN, Cards.BALLS_CHANGER, Cards.ACORN_CHANGER, Cards.HEARTS_CHANGER }.ToList()));
            cardList.Add(new Card(Cards.LEAVES_LOWER, "Zelený spodek", path + "LEAVES_LOWER.png", ChangerType.NONE, new Cards[] { Cards.LEAVES_SEVEN, Cards.LEAVES_EIGHT, Cards.LEAVES_NINE, Cards.LEAVES_TEN, Cards.LEAVES_LOWER, Cards.LEAVES_CHANGER, Cards.LEAVES_KING, Cards.LEAVES_STOP, Cards.BALLS_LOWER, Cards.ACORN_LOWER, Cards.HEARTS_LOWER, Cards.BALLS_CHANGER, Cards.ACORN_CHANGER, Cards.HEARTS_CHANGER }.ToList()));
            cardList.Add(new Card(Cards.LEAVES_CHANGER, "Zelený měnič", path + "LEAVES_CHANGER.png", ChangerType.NONE, new Cards[]
            {
                Cards.LEAVES_SEVEN, Cards.LEAVES_EIGHT, Cards.LEAVES_NINE, Cards.LEAVES_TEN, Cards.LEAVES_LOWER,
                Cards.LEAVES_CHANGER, Cards.LEAVES_KING, Cards.LEAVES_STOP, Cards.BALLS_CHANGER,
                Cards.ACORN_CHANGER, Cards.HEARTS_CHANGER
            }.ToList()));
            cardList.Add(new Card(Cards.LEAVES_KING, "Zelený král", path + "LEAVES_KING.png", ChangerType.NONE, new Cards[] { Cards.LEAVES_SEVEN, Cards.LEAVES_EIGHT, Cards.LEAVES_NINE, Cards.LEAVES_TEN, Cards.LEAVES_LOWER, Cards.LEAVES_CHANGER, Cards.LEAVES_KING, Cards.LEAVES_STOP, Cards.BALLS_KING, Cards.ACORN_KING, Cards.HEARTS_KING, Cards.BALLS_CHANGER, Cards.ACORN_CHANGER, Cards.HEARTS_CHANGER }.ToList()));
            cardList.Add(new Card(Cards.LEAVES_STOP, "Zelené eso", path + "LEAVES_STOP.png", ChangerType.NONE, new Cards[] { Cards.LEAVES_SEVEN, Cards.LEAVES_EIGHT, Cards.LEAVES_NINE, Cards.LEAVES_TEN, Cards.LEAVES_LOWER, Cards.LEAVES_CHANGER, Cards.LEAVES_KING, Cards.LEAVES_STOP, Cards.BALLS_STOP, Cards.ACORN_STOP, Cards.HEARTS_STOP, Cards.BALLS_CHANGER, Cards.ACORN_CHANGER, Cards.HEARTS_CHANGER }.ToList()));
            
            cardList.Add(new Card(Cards.ACORN_SEVEN, "Žaludová sedma", path + "ACORN_SEVEN.png", ChangerType.NONE, new Cards[] { Cards.ACORN_SEVEN, Cards.ACORN_EIGHT, Cards.ACORN_NINE, Cards.ACORN_TEN, Cards.ACORN_LOWER, Cards.ACORN_CHANGER, Cards.ACORN_KING, Cards.ACORN_STOP, Cards.BALLS_SEVEN, Cards.LEAVES_SEVEN, Cards.HEARTS_SEVEN, Cards.BALLS_CHANGER, Cards.LEAVES_CHANGER, Cards.HEARTS_CHANGER }.ToList()));
            cardList.Add(new Card(Cards.ACORN_EIGHT, "Žaludová osma", path + "ACORN_EIGHT.png", ChangerType.NONE, new Cards[] { Cards.ACORN_SEVEN, Cards.ACORN_EIGHT, Cards.ACORN_NINE, Cards.ACORN_TEN, Cards.ACORN_LOWER, Cards.ACORN_CHANGER, Cards.ACORN_KING, Cards.ACORN_STOP, Cards.BALLS_EIGHT, Cards.LEAVES_EIGHT, Cards.HEARTS_EIGHT, Cards.BALLS_CHANGER, Cards.LEAVES_CHANGER, Cards.HEARTS_CHANGER }.ToList()));
            cardList.Add(new Card(Cards.ACORN_NINE, "Žaludová devítka", path + "ACORN_NINE.png", ChangerType.NONE, new Cards[] { Cards.ACORN_SEVEN, Cards.ACORN_EIGHT, Cards.ACORN_NINE, Cards.ACORN_TEN, Cards.ACORN_LOWER, Cards.ACORN_CHANGER, Cards.ACORN_KING, Cards.ACORN_STOP, Cards.BALLS_NINE, Cards.LEAVES_NINE, Cards.HEARTS_NINE, Cards.BALLS_CHANGER, Cards.LEAVES_CHANGER, Cards.HEARTS_CHANGER }.ToList()));
            cardList.Add(new Card(Cards.ACORN_TEN, "Žaludová desítka", path + "ACORN_TEN.png", ChangerType.NONE, new Cards[] { Cards.ACORN_SEVEN, Cards.ACORN_EIGHT, Cards.ACORN_NINE, Cards.ACORN_TEN, Cards.ACORN_LOWER, Cards.ACORN_CHANGER, Cards.ACORN_KING, Cards.ACORN_STOP, Cards.BALLS_TEN, Cards.LEAVES_TEN, Cards.HEARTS_TEN, Cards.BALLS_CHANGER, Cards.LEAVES_CHANGER, Cards.HEARTS_CHANGER }.ToList()));
            cardList.Add(new Card(Cards.ACORN_LOWER, "Žaludový spodek", path + "ACORN_LOWER.png", ChangerType.NONE, new Cards[] { Cards.ACORN_SEVEN, Cards.ACORN_EIGHT, Cards.ACORN_NINE, Cards.ACORN_TEN, Cards.ACORN_LOWER, Cards.ACORN_CHANGER, Cards.ACORN_KING, Cards.ACORN_STOP, Cards.BALLS_LOWER, Cards.LEAVES_LOWER, Cards.HEARTS_LOWER, Cards.BALLS_CHANGER, Cards.LEAVES_CHANGER, Cards.HEARTS_CHANGER }.ToList()));
            cardList.Add(new Card(Cards.ACORN_CHANGER, "Žaludový měnič", path + "ACORN_CHANGER.png", ChangerType.NONE, new Cards[]
            {
                Cards.ACORN_SEVEN, Cards.ACORN_EIGHT, Cards.ACORN_NINE, Cards.ACORN_TEN, Cards.ACORN_LOWER,
                Cards.ACORN_CHANGER, Cards.ACORN_KING, Cards.ACORN_STOP, Cards.BALLS_CHANGER,
                Cards.LEAVES_CHANGER, Cards.HEARTS_CHANGER
            }.ToList()));
            cardList.Add(new Card(Cards.ACORN_KING, "Žaludový král", path + "ACORN_KING.png", ChangerType.NONE, new Cards[] { Cards.ACORN_SEVEN, Cards.ACORN_EIGHT, Cards.ACORN_NINE, Cards.ACORN_TEN, Cards.ACORN_LOWER, Cards.ACORN_CHANGER, Cards.ACORN_KING, Cards.ACORN_STOP, Cards.BALLS_KING, Cards.LEAVES_KING, Cards.HEARTS_KING, Cards.BALLS_CHANGER, Cards.LEAVES_CHANGER, Cards.HEARTS_CHANGER }.ToList()));
            cardList.Add(new Card(Cards.ACORN_STOP, "Žaludové eso", path + "ACORN_STOP.png", ChangerType.NONE, new Cards[] { Cards.ACORN_SEVEN, Cards.ACORN_EIGHT, Cards.ACORN_NINE, Cards.ACORN_TEN, Cards.ACORN_LOWER, Cards.ACORN_CHANGER, Cards.ACORN_KING, Cards.ACORN_STOP, Cards.BALLS_STOP, Cards.LEAVES_STOP, Cards.HEARTS_STOP, Cards.BALLS_CHANGER, Cards.LEAVES_CHANGER, Cards.HEARTS_CHANGER }.ToList()));

        }

        public List<Cards> changerCanPlay(ChangerType type)
        {
            switch (type)
            {
                case ChangerType.ACORN:
                    return new Cards[]
                    {
                        Cards.ACORN_SEVEN, Cards.ACORN_EIGHT, Cards.ACORN_NINE, Cards.ACORN_TEN, Cards.ACORN_LOWER,
                        Cards.ACORN_CHANGER, Cards.ACORN_KING, Cards.ACORN_STOP, Cards.BALLS_CHANGER,
                        Cards.LEAVES_CHANGER, Cards.HEARTS_CHANGER
                    }.ToList();
                case ChangerType.LEAVES:
                    return new Cards[]
                    {
                        Cards.LEAVES_SEVEN, Cards.LEAVES_EIGHT, Cards.LEAVES_NINE, Cards.LEAVES_TEN, Cards.LEAVES_LOWER,
                        Cards.LEAVES_CHANGER, Cards.LEAVES_KING, Cards.LEAVES_STOP, Cards.BALLS_CHANGER,
                        Cards.ACORN_CHANGER, Cards.HEARTS_CHANGER
                    }.ToList();
                case ChangerType.HEARTS:
                    return new Cards[]
                    {
                        Cards.HEARTS_SEVEN, Cards.HEARTS_EIGHT, Cards.HEARTS_NINE, Cards.HEARTS_TEN, Cards.HEARTS_LOWER,
                        Cards.HEARTS_CHANGER, Cards.HEARTS_KING, Cards.HEARTS_STOP, Cards.BALLS_CHANGER,
                        Cards.ACORN_CHANGER, Cards.LEAVES_CHANGER
                    }.ToList();
                case ChangerType.BALLS:
                    return new Cards[]
                    {
                        Cards.BALLS_SEVEN, Cards.BALLS_EIGHT, Cards.BALLS_NINE, Cards.BALLS_TEN, Cards.BALLS_LOWER,
                        Cards.BALLS_CHANGER, Cards.BALLS_KING, Cards.BALLS_STOP, Cards.HEARTS_CHANGER,
                        Cards.ACORN_CHANGER, Cards.LEAVES_CHANGER
                    }.ToList();
            }

            return null;
        }

        // Setting turn
        public void setTurn(Turn turn)
        {
            this.turn = turn;
        }

        // Changing turn
        public void changeTurn()
        {
            if (turn == Turn.YOUR) turn = Turn.OPPONENT;
            else turn = Turn.YOUR;
        }

        // Getting turn
        public Turn getTurn()
        {
            return turn;
        }

        // Init card pack
        public void initCardPack()
        {
            var img = instance.getCm().createImage("CARD_PACK", "C:\\Users\\dodis\\RiderProjects\\Prsi\\Prsi\\Cards\\CARD_PACK.png", 0, new Thickness(1560D, 0D,0D, 0D));
            instance.mainGrid.Children.Add(img);
        }

        // Give away a card (by player)
        public void giveAwayCard(Card card, FrameworkElement element)
        {
            // If last given is not set
            if (lastGivenAway == null)
            {
                Cards[] changers = new Cards[]
                    { Cards.ACORN_CHANGER, Cards.BALLS_CHANGER, Cards.HEARTS_CHANGER, Cards.LEAVES_CHANGER };
                
                ChangerType[] changersTypes = new ChangerType[]
                    { ChangerType.ACORN, ChangerType.BALLS, ChangerType.HEARTS, ChangerType.LEAVES };
                
                // Set last given card to card (from parameter)
                lastGivenAway = card;
                if (changers.Contains(card.getType()))
                {
                    ChangerType type = changersTypes[rand.Next(4)];
                    card.setChType(type);
                    showChanger(type);
                }
                givenAway.Add(card); // Add card to list
                var img = instance.getCm().createImage(card.getType().ToString(), card.getTexturePath(), rand.Next(37)*10, new Thickness(0D, 0D,0D, 0D));
                instance.mainGrid.Children.Add(img);
            }
            else // If last given is set
            {
                // If card is set
                if (card != null)
                {
                    // If the selected card is not changer
                    if (!new Cards[]
                                { Cards.ACORN_CHANGER, Cards.BALLS_CHANGER, Cards.HEARTS_CHANGER, Cards.LEAVES_CHANGER }
                            .Contains(lastGivenAway.getType()))
                    {
                        // If the selected card can be played on last given card && if the card is players && if the player's on turn
                        if (lastGivenAway.getCanPlay().Contains(card.getType()) &&
                            instance.getDm().getYour().Contains(card) && getTurn().Equals(Turn.YOUR))
                        {
                            hideChangerSelector();
                            instance.mainGrid.Children.Remove(element); // Remove card's image
                            lastGivenAway = card; // Set last given card
                            givenAway.Add(card); // Add card to list
                            
                            // Add selected card
                            var img = instance.getCm().createImage(card.getType().ToString(), card.getTexturePath(),
                                rand.Next(37) * 10, new Thickness(0D, 0D, 0D, 0D));
                            instance.mainGrid.Children.Add(img);
                            
                            instance.getDm().removeFromYour(card); // Remove card from player's deck
                            
                            if (new Cards[] { Cards.ACORN_SEVEN, Cards.BALLS_SEVEN, Cards.HEARTS_SEVEN, Cards.LEAVES_SEVEN }
                                .Contains(card.getType()))
                            {
                                botTakeCard();
                                botTakeCard();
                            }
                            
                            if (new Cards[] { Cards.ACORN_CHANGER, Cards.BALLS_CHANGER, Cards.HEARTS_CHANGER, Cards.LEAVES_CHANGER }
                            .Contains(card.getType()))
                            {
                                card.setChType(new ChangerType[]
                                    { ChangerType.HEARTS, ChangerType.ACORN, ChangerType.BALLS, ChangerType.LEAVES }[
                                    rand.Next(4)]);
                                switch (card.getType())
                                {
                                    case Cards.ACORN_CHANGER:
                                        showChanger(ChangerType.ACORN);
                                        card.setChType(ChangerType.ACORN);
                                        break;
                                    case Cards.BALLS_CHANGER:
                                        showChanger(ChangerType.BALLS);
                                        card.setChType(ChangerType.BALLS);
                                        break;
                                    case Cards.HEARTS_CHANGER:
                                        showChanger(ChangerType.HEARTS);
                                        card.setChType(ChangerType.HEARTS);
                                        break;
                                    case Cards.LEAVES_CHANGER:
                                        showChanger(ChangerType.LEAVES);
                                        card.setChType(ChangerType.LEAVES);
                                        break;
                                }

                            }
                            
                            // Change turn if card is not stopping card
                            if (!new Cards[] { Cards.ACORN_STOP, Cards.BALLS_STOP, Cards.HEARTS_STOP, Cards.LEAVES_STOP, Cards.ACORN_SEVEN, Cards.BALLS_SEVEN, Cards.HEARTS_SEVEN, Cards.LEAVES_SEVEN }
                                    .Contains(card.getType())) setTurn(Turn.OPPONENT);
                            else setTurn(Turn.YOUR);
                        }
                    }
                    else // If selected card is changer
                    {
                        // If the selected card can be played on last given card && if the card is players && if the player's on turn
                        if (changerCanPlay(lastGivenAway.getChType()).Contains(card.getType()) &&
                            instance.getDm().getYour().Contains(card) && getTurn().Equals(Turn.YOUR))
                        {
                            hideChangerSelector();
                            instance.mainGrid.Children.Remove(element); // Remove card's image
                            lastGivenAway = card; // Set last given card
                            givenAway.Add(card); // Add card to list
                            
                            // Add selected card
                            var img = instance.getCm().createImage(card.getType().ToString(), card.getTexturePath(),
                                rand.Next(37) * 10, new Thickness(0D, 0D, 0D, 0D));
                            instance.mainGrid.Children.Add(img);
                            
                            instance.getDm().removeFromYour(card); // Remove card from player's deck
                            
                            if (new Cards[] { Cards.ACORN_SEVEN, Cards.BALLS_SEVEN, Cards.HEARTS_SEVEN, Cards.LEAVES_SEVEN }
                                .Contains(card.getType()))
                            {
                                botTakeCard();
                                botTakeCard();
                            }
                            
                            if (new Cards[] { Cards.ACORN_CHANGER, Cards.BALLS_CHANGER, Cards.HEARTS_CHANGER, Cards.LEAVES_CHANGER }
                            .Contains(card.getType()))
                            {
                                
                                card.setChType(new ChangerType[]
                                    { ChangerType.HEARTS, ChangerType.ACORN, ChangerType.BALLS, ChangerType.LEAVES }[
                                    rand.Next(4)]);
                                switch (card.getType())
                                {
                                    case Cards.ACORN_CHANGER:
                                        showChanger(ChangerType.ACORN);
                                        card.setChType(ChangerType.ACORN);
                                        break;
                                    case Cards.BALLS_CHANGER:
                                        showChanger(ChangerType.BALLS);
                                        card.setChType(ChangerType.BALLS);
                                        break;
                                    case Cards.HEARTS_CHANGER:
                                        showChanger(ChangerType.HEARTS);
                                        card.setChType(ChangerType.HEARTS);
                                        break;
                                    case Cards.LEAVES_CHANGER:
                                        showChanger(ChangerType.LEAVES);
                                        card.setChType(ChangerType.LEAVES);
                                        break;
                                }

                            }
                            
                            // Change turn if card is not stopping card
                            if (!new Cards[] { Cards.ACORN_STOP, Cards.BALLS_STOP, Cards.HEARTS_STOP, Cards.LEAVES_STOP, Cards.ACORN_SEVEN, Cards.BALLS_SEVEN, Cards.HEARTS_SEVEN, Cards.LEAVES_SEVEN }
                                    .Contains(card.getType())) setTurn(Turn.OPPONENT);
                            else setTurn(Turn.YOUR);
                        }
                    }
                }
                else if (element.Name.Equals("CARD_PACK") && getTurn().Equals(Turn.YOUR)) // If player choose deck's image && if it's player's turn
                {
                    takeCard(); // Take card
                }
            }
            
            relocateDecks(); // Relocate decks
            
            if (instance.getDm().getYour().Count == 0) restartGame(); // If player does not have any cards -> player won
        }

        // Find element by card
        public FrameworkElement findElementByCard(Card card)
        {
            FrameworkElement element = null; // Create variable
            
            // Foreach all elements
            foreach (FrameworkElement ele in instance.mainGrid.Children)
            {
                if (ele.Name.Equals(card.getType().ToString())) element = ele; // If element is the card's element -> set element's variable to found element
            }

            return element; // Return element
        }
        
        // Find element by name
        public FrameworkElement findElementByName(string name)
        {
            FrameworkElement element = null; // Create variable
            
            // Foreach all elements
            foreach (FrameworkElement ele in instance.mainGrid.Children)
            {
                if (ele.Name.Equals(name)) element = ele; // If element is the card's element -> set element's variable to found element
            }

            return element; // Return element
        }

        // Pack resetting
        public void resetPack()
        {
            foreach (Card card in givenAway) // Loop through all cards given away
            {
                if (card != lastGivenAway) // If card is not last given away
                {
                    FrameworkElement elem = findElementByCard(card); // Get element
                    instance.mainGrid.Children.Remove(elem); // Remove element
                    remainingCardList.Add(card); // Add card to remaining pack
                }
            }
        }

        // Give away a card (by bot)
        public bool botGiveAway(Card card)
        {
            bool played = false;
            if (lastGivenAway == null)
            {
                lastGivenAway = card;
                var img = instance.getCm().createImage(card.getType().ToString(), card.getTexturePath(), rand.Next(37)*10, new Thickness(0D, 0D,0D, 0D));
                instance.mainGrid.Children.Add(img);
                givenAway.Add(card);
            }
            else
            {
                if (card == null) return false;
                
                // If the selected card is not changer
                if (!new Cards[]
                            { Cards.ACORN_CHANGER, Cards.BALLS_CHANGER, Cards.HEARTS_CHANGER, Cards.LEAVES_CHANGER }
                        .Contains(lastGivenAway.getType()))
                {
                    if (lastGivenAway.getCanPlay().Contains(card.getType()) && instance.getDm().getOpponents().Contains(card) && getTurn().Equals(Turn.OPPONENT))
                    {
                        hideChangerSelector();
                        instance.mainGrid.Children.Remove(findElementByCard(card));
                        lastGivenAway = card;
                        givenAway.Add(card);
                        var img = instance.getCm().createImage(card.getType().ToString(), card.getTexturePath(),
                            rand.Next(37) * 10, new Thickness(0D, 0D, 0D, 0D));
                        instance.mainGrid.Children.Add(img);
                        instance.getDm().removeFromOpponent(card);
                        played = true;
                        if (new Cards[] { Cards.ACORN_SEVEN, Cards.BALLS_SEVEN, Cards.HEARTS_SEVEN, Cards.LEAVES_SEVEN }
                            .Contains(card.getType()))
                        {
                            takeCard();
                            takeCard();
                        }
                        if (new Cards[] { Cards.ACORN_CHANGER, Cards.BALLS_CHANGER, Cards.HEARTS_CHANGER, Cards.LEAVES_CHANGER }
                            .Contains(card.getType()))
                        {
                            ChangerType chtype = new ChangerType[]
                                { ChangerType.HEARTS, ChangerType.ACORN, ChangerType.BALLS, ChangerType.LEAVES }[
                                rand.Next(4)];
                            card.setChType(chtype);
                            showChanger(chtype);

                        }
                        if (!new Cards[] { Cards.ACORN_STOP , Cards.BALLS_STOP, Cards.HEARTS_STOP, Cards.LEAVES_STOP, Cards.ACORN_SEVEN, Cards.BALLS_SEVEN, Cards.HEARTS_SEVEN, Cards.LEAVES_SEVEN }.Contains(card.getType())) setTurn(Turn.YOUR);
                        else setTurn(Turn.OPPONENT);
                    }
                }
                else // If the selected card is changer
                {
                    if (changerCanPlay(lastGivenAway.getChType()).Contains(card.getType()) && instance.getDm().getOpponents().Contains(card) && getTurn().Equals(Turn.OPPONENT))
                    {
                        hideChangerSelector();
                        instance.mainGrid.Children.Remove(findElementByCard(card));
                        lastGivenAway = card;
                        givenAway.Add(card);
                        var img = instance.getCm().createImage(card.getType().ToString(), card.getTexturePath(),
                            rand.Next(37) * 10, new Thickness(0D, 0D, 0D, 0D));
                        instance.mainGrid.Children.Add(img);
                        instance.getDm().removeFromOpponent(card);
                        played = true;
                        if (new Cards[] { Cards.ACORN_SEVEN, Cards.BALLS_SEVEN, Cards.HEARTS_SEVEN, Cards.LEAVES_SEVEN }
                            .Contains(card.getType()))
                        {
                            takeCard();
                            takeCard();
                        }
                        if (new Cards[] { Cards.ACORN_CHANGER, Cards.BALLS_CHANGER, Cards.HEARTS_CHANGER, Cards.LEAVES_CHANGER }
                            .Contains(card.getType()))
                        {
                            ChangerType chtype = new ChangerType[]
                                { ChangerType.HEARTS, ChangerType.ACORN, ChangerType.BALLS, ChangerType.LEAVES }[
                                rand.Next(4)];
                            card.setChType(chtype);
                            showChanger(chtype);

                        }
                        if (!new Cards[] { Cards.ACORN_STOP , Cards.BALLS_STOP, Cards.HEARTS_STOP, Cards.LEAVES_STOP, Cards.ACORN_SEVEN, Cards.BALLS_SEVEN, Cards.HEARTS_SEVEN, Cards.LEAVES_SEVEN }.Contains(card.getType())) setTurn(Turn.YOUR);
                        else setTurn(Turn.OPPONENT);
                    }
                }
            }
            
            relocateDecks();
            
            if (instance.getDm().getOpponents().Count == 0) restartGame();

            return played;
        }

        public void takeCard()
        {
            Card card = remainingCardList[rand.Next(remainingCardList.Count)];
            remainingCardList.Remove(card);
            var img = instance.getCm().createImage(card.getType().ToString(), card.getTexturePath(), 0, new Thickness(-1560D+findElementByCard(instance.getDm().getYour()[instance.getDm().getYour().Count-1]).Margin.Left*-1+150, 700D,0D, 0D));
            instance.mainGrid.Children.Add(img);
            instance.getDm().addToYour(card);
            setTurn(Turn.OPPONENT);

            if (remainingCardList.Count == 0) resetPack();
            
            relocateDecks();
        }

        public void showChangerSelector()
        {
            var img = instance.getCm().createSelectorImage("HEARTS", "C:\\Users\\dodis\\RiderProjects\\Prsi\\Prsi\\Cards\\HEARTS_CHANGER_SELECT.png", new Thickness(-1600D, -200D, 0D, 0D));
            instance.mainGrid.Children.Add(img);
            img = instance.getCm().createSelectorImage("LEAVES", "C:\\Users\\dodis\\RiderProjects\\Prsi\\Prsi\\Cards\\LEAVES_CHANGER_SELECT.png", new Thickness(-1600D, 200D,  0D, 0D));
            instance.mainGrid.Children.Add(img);
            img = instance.getCm().createSelectorImage("ACORN", "C:\\Users\\dodis\\RiderProjects\\Prsi\\Prsi\\Cards\\ACORN_CHANGER_SELECT.png", new Thickness(-1200D, -200D, 0D, 0D));
            instance.mainGrid.Children.Add(img);
            img = instance.getCm().createSelectorImage("BALLS", "C:\\Users\\dodis\\RiderProjects\\Prsi\\Prsi\\Cards\\BALLS_CHANGER_SELECT.png", new Thickness(-1200D, 200D, 0D, 0D));
            instance.mainGrid.Children.Add(img);
        }
        
        public void showChanger(ChangerType type)
        {
            Image img = null;
            switch (type)
            {
                case ChangerType.HEARTS:
                    img = instance.getCm().createSelectorImage("HEARTS", "C:\\Users\\dodis\\RiderProjects\\Prsi\\Prsi\\Cards\\HEARTS_CHANGER_SELECT.png", new Thickness(-1600D, -200D, 0D, 0D));
                    break;
                case ChangerType.LEAVES:
                    img = instance.getCm().createSelectorImage("LEAVES", "C:\\Users\\dodis\\RiderProjects\\Prsi\\Prsi\\Cards\\LEAVES_CHANGER_SELECT.png", new Thickness(-1600D, 200D,  0D, 0D));
                    break;
                case ChangerType.ACORN:
                    img = instance.getCm().createSelectorImage("ACORN", "C:\\Users\\dodis\\RiderProjects\\Prsi\\Prsi\\Cards\\ACORN_CHANGER_SELECT.png", new Thickness(-1200D, -200D, 0D, 0D));
                    break;
                case ChangerType.BALLS:
                    img = instance.getCm().createSelectorImage("BALLS", "C:\\Users\\dodis\\RiderProjects\\Prsi\\Prsi\\Cards\\BALLS_CHANGER_SELECT.png", new Thickness(-1200D, 200D, 0D, 0D));
                    break;
            }
            instance.mainGrid.Children.Add(img);
        }

        public void hideChangerSelector()
        {
            instance.mainGrid.Children.Remove(findElementByName("HEARTS"));
            instance.mainGrid.Children.Remove(findElementByName("ACORN"));
            instance.mainGrid.Children.Remove(findElementByName("LEAVES"));
            instance.mainGrid.Children.Remove(findElementByName("BALLS"));
        }
        
        public void botTakeCard()
        {
            Card card = remainingCardList[rand.Next(remainingCardList.Count)];
            remainingCardList.Remove(card);
            var img = instance.getCm().createImage(card.getType().ToString(), "C:\\Users\\dodis\\RiderProjects\\Prsi\\Prsi\\Cards\\CARD_BACK.png", 0, new Thickness(-1560D+findElementByCard(instance.getDm().getOpponents()[instance.getDm().getOpponents().Count-1]).Margin.Left*-1+150, -700D,0D, 0D));
            instance.mainGrid.Children.Add(img);
            instance.getDm().addToOpponent(card);
            setTurn(Turn.YOUR);
            
            if (remainingCardList.Count == 0) resetPack();
            
            relocateDecks();
        }

        public Card getCardByName(string name)
        {
            Card card = null;

            foreach (Card c in cardList)
            {
                if (c.getType().ToString().Equals(name))
                {
                    card = c;
                    break;
                }
            }
            return card;
        }

        public void relocateDecks()
        {
            int i = 0;
            foreach (Card card in instance.getDm().getYour())
            {
                FrameworkElement elem = findElementByCard(card);
                instance.mainGrid.Children.Remove(elem);
                var img = instance.getCm().createImage(card.getType().ToString(), card.getTexturePath(), 0, new Thickness(-1560D+i*300, 700D,0D, 0D));
                instance.mainGrid.Children.Add(img);
                i++;
            }
            
            i = 0;
            foreach (Card card in instance.getDm().getOpponents())
            {
                FrameworkElement elem = findElementByCard(card);
                instance.mainGrid.Children.Remove(elem);
                var img = instance.getCm().createImage(card.getType().ToString(), "C:\\Users\\dodis\\RiderProjects\\Prsi\\Prsi\\Cards\\CARD_BACK.png", 0, new Thickness(-1560D+i*150, -700D,0D, 0D));
                instance.mainGrid.Children.Add(img);
                i++;
            }
        }

        public void startGame()
        {
            List<Card> toGiveAway = new List<Card>();
            foreach (Card c in cardList)
            {
                toGiveAway.Add(c);
            }

            Card card;
            
            for (int i = 0; i < 4; i++)
            {
                card = toGiveAway[rand.Next(toGiveAway.Count)];
                instance.getDm().addToOpponent(card);
                toGiveAway.Remove(card);
                var img = instance.getCm().createImage(card.getType().ToString(), "C:\\Users\\dodis\\RiderProjects\\Prsi\\Prsi\\Cards\\CARD_BACK.png", 0, new Thickness(-1560D+i*150, -700D,0D, 0D));
                instance.mainGrid.Children.Add(img);
                
                card = toGiveAway[rand.Next(toGiveAway.Count)];
                instance.getDm().addToYour(card);
                toGiveAway.Remove(card);
                img = instance.getCm().createImage(card.getType().ToString(), card.getTexturePath(), 0, new Thickness(-1560D+i*300, 700D,0D, 0D));
                instance.mainGrid.Children.Add(img);
            }

            card = toGiveAway[rand.Next(toGiveAway.Count)];
            giveAwayCard(card, null);
            toGiveAway.Remove(card);
            
            remainingCardList = toGiveAway;


        }

        public void restartGame()
        {
            Window oldWin = instance;
            Window newWin = new MainWindow();
            newWin.Show();
            oldWin.Close();
        }
    }
}