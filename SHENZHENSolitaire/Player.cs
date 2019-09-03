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
            for (byte i = 0; i < PlayingField.COLUMNS_BUFFER; i++)
            {
                if (field[i].Suit == SuitEnum.BLOCKED || field[i].Suit == SuitEnum.EMPTY) continue;

                Turn turn = new Turn { FromColumn = i, FromTop = true, ToTop = true };

                for (byte j = PlayingField.COLUMNS_BUFFER; j < PlayingField.COLUMNS_TOP; j++)
                {
                    turn.ToColumn = j;
                    if (field.IsTurnAllowed(turn))
                    {
                        turns.Add(turn);
                    }
                }

                turn.ToTop = false;
                for (byte j = 0; j < PlayingField.COLUMNS_FIELD; j++)
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
            for (byte col = 0; col < PlayingField.COLUMNS_FIELD; col++)
            {
                int columnLength = field.GetColumnLength(col);
                for (byte row = 0; row < columnLength; row++)
                {
                    if (!field.IsMovable(col, row)) continue;

                    Turn turn = new Turn { FromColumn = col, FromRow = row, FromTop = false, ToTop = true };
                    #region [TO OUTPUT]
                    for (byte i = PlayingField.COLUMNS_BUFFER; i < PlayingField.COLUMNS_TOP; i++) //Check the output slots first
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
                    for (byte i = 0; i < PlayingField.COLUMNS_FIELD; i++)
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
                    for (byte i = 0; i < PlayingField.COLUMNS_BUFFER; i++)
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
