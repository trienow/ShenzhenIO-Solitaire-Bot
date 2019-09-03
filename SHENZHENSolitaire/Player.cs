using System.Collections.Generic;

namespace SHENZENSolitaire
{
    public class Player
    {
        public PlayingField Field { get; set; }

        public Player(PlayingField field)
        {
            Field = field;
        }

        public List<Turn> FindAllPossibleTurns(PlayingField field)
        {
            List<Turn> turns = new List<Turn>();
            List<Turn> bufferTurns = new List<Turn>();

            #region [FROM BUFFER]
            for (int i = 0; i < 3; i++)
            {
                if (field[i].Suit == SuitEnum.BLOCKED || field[i].Suit == SuitEnum.EMPTY) continue;

                Turn turn = new Turn { FromColumn = i, FromTop = true, ToTop = true };

                for (int j = 3; j < PlayingField.COLUMNS_TOP; j++)
                {
                    turn.ToColumn = j;
                    if (field.IsTurnAllowed(turn))
                    {
                        turns.Add(turn);
                    }
                }

                turn.ToTop = false;
                for (int j = 0; j < PlayingField.COLUMNS_FIELD; j++)
                {
                    turn.ToColumn = j;
                    if (field.IsTurnAllowed(turn))
                    {
                        turns.Add(turn);
                    }
                }
            }
            #endregion

            #region [FROM FIELD]
            for (int col = 0; col < PlayingField.COLUMNS_FIELD; col++)
            {
                int columnLength = field.GetColumnLength(col);
                for (int row = 0; row < columnLength; row++)
                {
                    if (!field.IsMovable(col, row)) continue;

                    Turn turn = new Turn { FromColumn = col, FromRow = row, FromTop = false, ToTop = true };
                    #region [TO OUTPUT]
                    for (int i = 3; i < PlayingField.COLUMNS_TOP; i++) //Check the output slots first
                    {
                        turn.ToColumn = i;
                        if (field.IsTurnAllowed(turn))
                        {
                            turns.Add(turn);
                        }
                    }
                    #endregion

                    #region [TO FIELD]
                    turn.ToTop = false;
                    for (int i = 0; i < PlayingField.COLUMNS_FIELD; i++)
                    {
                        if (i == col) continue;

                        turn.ToColumn = i;
                        if (field.IsTurnAllowed(turn))
                        {
                            turns.Add(turn);
                        }
                    }
                    #endregion

                    #region [TO BUFFER]
                    turn.ToTop = true;
                    for (int i = 0; i < 3; i++)
                    {
                        turn.ToColumn = i;
                        if (field.IsTurnAllowed(turn))
                        {
                            bufferTurns.Add(turn);
                        }
                    }
                    #endregion
                }
            } 
            #endregion

            turns.AddRange(bufferTurns);
            return turns;
        }

        public void FindSolution()
        {
            
        }
    }
}
