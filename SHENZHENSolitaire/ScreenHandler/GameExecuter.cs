using SHENZENSolitaire.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SHENZENSolitaire.ScreenHandler
{
    public class GameExecuter
    {
        [DllImport("user32.dll")]
        private static extern long SetCursorPos(int x, int y);

        private GameState[] turns;

        public GameExecuter(GameState[] states)
        {
            turns = states;
        }

        public GameExecuter() { }

        public void Execute()
        {
            SetCursorPos(500, 500);
        }
    }
}