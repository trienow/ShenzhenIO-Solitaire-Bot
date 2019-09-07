using SHENZENSolitaire.Actor;
using SHENZENSolitaire.Game;
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
                if (turn.ToTop && turn.ToColumn > 2)
                    Thread.Sleep(500);

                Vec2 from = ScreenExtractor.TranslateSlotToPos(turn.FromTop, turn.FromColumn, turn.FromRow, state.MovedCard);
                Vec2 to = ScreenExtractor.TranslateSlotToPos(turn.ToTop, turn.ToColumn, 5, state.MovedCard);

                Mouse.DragFromTo(from.XInt, from.YInt, to.XInt, to.YInt);
            }
            else
            {
                switch (turn.MergeDragons)
                {
                    case SuitEnum.RED: Mouse.ClickTo(1208, 337); break;
                    case SuitEnum.GREEN: Mouse.ClickTo(1208, 420); break;
                    case SuitEnum.BLACK: Mouse.ClickTo(1208, 509); break;
                }
            }

            Thread.Sleep(1000);
        }

        public static void NewGame()
        {
            Mouse.ClickTo(1759, 1113);
            Thread.Sleep(7000);
        }
        public static void FocusWindow()
        {
            Mouse.ClickTo(1245, 383);
        }
    }
}