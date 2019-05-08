using System;
using System.Collections.Generic;

namespace SHENZENSolitaire
{
    public class Solitaire
    {
        Random rnd;
        Deck[] playingField = new Deck[45];

        public Solitaire()
        {
            rnd = new Random();
            BuildPlayingField();
        }

        /// <summary>
        /// Inits the playing field randomly (<see cref="playingField"/> 10 to 50)
        /// </summary>
        private void BuildPlayingField()
        {
            #region List<Deck> deckToDistribute = new List<Deck>();
            List<Deck> deckToDistribute = new List<Deck>()
            {
                Deck.Rose,
                Deck.Red,
                Deck.Red,
                Deck.Red,
                Deck.Red,
                Deck.Black,
                Deck.Black,
                Deck.Black,
                Deck.Black,
                Deck.Green,
                Deck.Green,
                Deck.Green,
                Deck.Green,
                Deck.Red1,
                Deck.Red2,
                Deck.Red3,
                Deck.Red4,
                Deck.Red5,
                Deck.Red6,
                Deck.Red7,
                Deck.Red8,
                Deck.Red9,
                Deck.Black1,
                Deck.Black2,
                Deck.Black3,
                Deck.Black4,
                Deck.Black5,
                Deck.Black6,
                Deck.Black7,
                Deck.Black8,
                Deck.Black9,
                Deck.Green1,
                Deck.Green2,
                Deck.Green3,
                Deck.Green4,
                Deck.Green5,
                Deck.Green6,
                Deck.Green7,
                Deck.Green8,
                Deck.Green9,
            };
            #endregion

            for (int i = 6; i < playingField.Length; i++)
            {
                int nextCardIndex = rnd.Next(deckToDistribute.Count);
                playingField[i] = deckToDistribute[nextCardIndex];
                deckToDistribute.RemoveAt(nextCardIndex);
            }
        }

        private void AutoActions()
        {
            for (int i = 6; i < playingField.Length; i++)
            {
                Deck card = playingField[i];
                if (card == Deck.Rose)
                {
                    playingField[3] = card;
                    card = Deck.Empty;
                }
                else
                {

                }
            }
        }
    }

    static class SolitaireExtensions
    {
        public static bool IsBlocker(this Deck card)
        {
            return card == Deck.Black || card == Deck.Green || card == Deck.Red;
        }
    }
}
