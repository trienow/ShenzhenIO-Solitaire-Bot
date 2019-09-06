using SHENZENSolitaire.Game;
using System.Linq;

namespace SHENZENSolitaire.Actor
{
    public class GameState
    {
        public GameState PreviousState { get; set; } = null; //<- Used to chain the states
        public PlayingField FieldResult { get; set; } //<- Used to generate the next state
        public int PathLength { get; private set; } = 0;
        public Turn ExecutedTurn { get; set; } //<- Used to generate the next field
        public Card MovedCard { get; set; } = Card.EMPTY; //<- Decor
        public int RemainingCards { get => FieldResult.RemainingCards; } //<- Convenience 
        public byte[][] Fingerprints { get; set; }

        public GameState(PlayingField initialField)
        {
            PreviousState = null;
            FieldResult = initialField;
        }

        public GameState(GameState previousState, Turn turn)
        {
            PreviousState = previousState;
            ExecutedTurn = turn;

            if (turn.MergeDragons == default)
            {
                MovedCard = turn.FromTop ? previousState.FieldResult[turn.FromColumn] : previousState.FieldResult[turn.FromColumn, turn.FromRow];
            }

            PathLength = previousState.PathLength + 1;
        }

        /// <summary>
        /// Performs a <see cref="Turn"/> and returns the amount of <see cref="Card"/>s on the <see cref="PlayingField"/>.
        /// </summary>
        /// <returns>The amount of <see cref="Card"/>s left on the <see cref="PlayingField"/></returns>
        public int PerformTurn()
        {
            FieldResult = PreviousState.FieldResult.PerformTurn(ExecutedTurn);
            Fingerprints = FieldResult.MakeFingerprints();
            return RemainingCards;
        }

        /// <summary>
        /// Tests, if the just generated stack has not been generated before!
        /// </summary>
        /// <returns></returns>
        public bool IsStateUnique()
        {
            bool result = true;
            GameState prevState = this.PreviousState;

            if (ExecutedTurn.ToTop)
            {
                byte col = PlayingField.COLUMNS_FIELD;
                byte[] stack = Fingerprints[col];
                while (prevState.PreviousState != null)
                {
                    if (stack.SequenceEqual(prevState.Fingerprints[col]))
                    {
                        result = false;
                        break;
                    }

                    prevState = prevState.PreviousState;
                }
            }
            else if (ExecutedTurn.MergeDragons == default)
            {
                byte[] stack = Fingerprints[ExecutedTurn.ToColumn]; //<- Compare the resulting stack with all other field stacks
                while (prevState.PreviousState != null)
                {
                    for (int col = 0; col < PlayingField.COLUMNS_FIELD; col++)
                    {
                        if (stack.SequenceEqual(prevState.Fingerprints[col]))
                        {
                            result = false;
                            break;
                        }
                    }

                    prevState = prevState.PreviousState;
                }
            }

            return result;
        }

        public override string ToString()
        {
            if (ExecutedTurn.MergeDragons == default)
            {
                return $"[{PathLength}]: {MovedCard} {ExecutedTurn}";
            }
            else
            {
                return $"[{PathLength}]: {ExecutedTurn}";
            }

        }

        /// <summary>
        /// Turns the linked <see cref="GameState"/>s into a linear array
        /// </summary>
        /// <param name="state">The state to flatten</param>
        /// <returns>An array of the linked <see cref="GameState"/>s</returns>
        public static GameState[] Linearize(GameState state)
        {
            GameState[] linearStates = new GameState[state.PathLength + 1];
            while (state != null)
            {
                linearStates[state.PathLength] = state;
                state = state.PreviousState;
            }

            return linearStates;
        }
    }
}
