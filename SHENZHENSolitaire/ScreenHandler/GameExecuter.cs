using SHENZENSolitaire.Actor;
using SHENZENSolitaire.Game;
using System;
using System.Threading;

namespace SHENZENSolitaire.ScreenHandler
{
    public class GameExecuter
    {
        public static void ExecuteState(GameState state)
        {
            Turn turn = state.ExecutedTurn;
            if (turn.MergeDragons == default)
            {
                if (!turn.ToTop || turn.ToColumn < 3 || state.FieldResult.CanStackAnythingOn(state.MovedCard))
                {
                    Vec2 from = ScreenExtractor.TranslateSlotToPos(turn.FromTop, turn.FromColumn, turn.FromRow, state.MovedCard);
                    Vec2 to = ScreenExtractor.TranslateSlotToPos(turn.ToTop, turn.ToColumn, 5, state.MovedCard);

                    Mouse.DragFromTo(from.XInt, from.YInt, to.XInt, to.YInt);
                    Thread.Sleep(500);
                }
                else
                {
                    Console.WriteLine("AUTO");
                    Thread.Sleep(300);
                }
            }
            else
            {
                switch (turn.MergeDragons)
                {
                    case SuitEnum.RED: Mouse.ClickTo(ScreenCoordinates.XDragon, ScreenCoordinates.YDragonRed); break;
                    case SuitEnum.GREEN: Mouse.ClickTo(ScreenCoordinates.XDragon, ScreenCoordinates.YDragonGreen); break;
                    case SuitEnum.BLACK: Mouse.ClickTo(ScreenCoordinates.XDragon, ScreenCoordinates.YDragonBlack); break;
                }
                Thread.Sleep(300);
            }
        }

        public static void NewGame()
        {
            Mouse.ClickTo(ScreenCoordinates.XNewGame, ScreenCoordinates.YNewGame);
            Thread.Sleep(7000);
        }
        public static void FocusWindow()
        {
            Mouse.ClickTo(ScreenCoordinates.XNothing, ScreenCoordinates.YNothing);
        }
    }
}