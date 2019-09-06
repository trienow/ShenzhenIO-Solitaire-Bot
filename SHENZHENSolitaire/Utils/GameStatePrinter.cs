using SHENZENSolitaire.Actor;
using System;

namespace SHENZENSolitaire.Utils
{
    public class GameStatePrinter
    {
        public static void Print(GameState state)
        {
            PlayingFieldPrinter.Print(state.FieldResult);

            if (state.PreviousState != null)
            {
                if (state.ExecutedTurn.MergeDragons == default)
                {
                    PlayingFieldPrinter.PrintCard(state.MovedCard);
                    Console.Write(": ");
                }

                PlayingFieldPrinter.PrintTurn(state.ExecutedTurn);

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
