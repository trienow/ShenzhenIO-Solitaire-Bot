using SHENZENSolitaire.Actor;
using System;

namespace SHENZENSolitaire.Utils
{
    public class GameStatePrinter
    {
        public static void Print(GameState state)
        {
            Console.Write($"Schritt {state.PathLength}: ");

            if (state.PreviousState != null)
            {
                if (state.ExecutedTurn.MergeDragons == default)
                {
                    Console.Write($"Move ");
                    PlayingFieldPrinter.PrintCard(state.MovedCard);
                    Console.Write(": ");
                }

                PlayingFieldPrinter.PrintTurn(state.ExecutedTurn);

            }

            Console.WriteLine();
            PlayingFieldPrinter.Print(state.FieldResult);
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
