using SHENZENSolitaire.Game;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Schedulers;

namespace SHENZENSolitaire.Actor
{
    public class Player
    {
        public PlayingField Field { get; set; }
        public static readonly Card[] MOVEMENT_PRIORITY = new Card[]
            {
                new Card(9, SuitEnum.RED),
                new Card(9, SuitEnum.GREEN),
                new Card(9, SuitEnum.BLACK),
                new Card(8, SuitEnum.RED),
                new Card(8, SuitEnum.GREEN),
                new Card(8, SuitEnum.BLACK),
                new Card(7, SuitEnum.RED),
                new Card(7, SuitEnum.GREEN),
                new Card(7, SuitEnum.BLACK),
                new Card(6, SuitEnum.RED),
                new Card(6, SuitEnum.GREEN),
                new Card(6, SuitEnum.BLACK),
                new Card(5, SuitEnum.RED),
                new Card(5, SuitEnum.GREEN),
                new Card(5, SuitEnum.BLACK),
                new Card(4, SuitEnum.RED),
                new Card(4, SuitEnum.GREEN),
                new Card(4, SuitEnum.BLACK),
                new Card(3, SuitEnum.RED),
                new Card(3, SuitEnum.GREEN),
                new Card(3, SuitEnum.BLACK),
                new Card(2, SuitEnum.RED),
                new Card(2, SuitEnum.GREEN),
                new Card(2, SuitEnum.BLACK),
                new Card(1, SuitEnum.RED),
                new Card(1, SuitEnum.GREEN),
                new Card(1, SuitEnum.BLACK),
                new Card(0, SuitEnum.ROSE),
                Card.DRAGON_RED,
                Card.DRAGON_GREEN,
                Card.DRAGON_BLACK
            };
        private int tries = 0;
        private static readonly LimitedConcurrencyLevelTaskScheduler LIMITED_TASK_SCHED = new LimitedConcurrencyLevelTaskScheduler(20);
        private static readonly ParallelOptions PARALLEL_OPTIONS = new ParallelOptions { MaxDegreeOfParallelism = 2, TaskScheduler = LIMITED_TASK_SCHED };

        public Player(PlayingField field)
        {
            this.Field = field;
        }

        public static List<Turn> FindAllPossibleTurns(PlayingField field)
        {
            List<Turn> turns = new List<Turn>();

            #region [FORCED MOVE]
            //Try move cards from the buffer to the output
            #region [FROM BUFFER TO OUTPUT]
            for (byte col = 0; col < PlayingField.COLUMNS_BUFFER; col++)
            {
                Card c = field[col];

                if (c.Value == 0) continue;

                Turn turn = new Turn { FromColumn = col, FromTop = true, ToTop = true };
                switch (c.Suit)
                {
                    case SuitEnum.ROSE: turn.ToColumn = 3; break;
                    case SuitEnum.RED: turn.ToColumn = 4; break;
                    case SuitEnum.BLACK: turn.ToColumn = 5; break;
                    case SuitEnum.GREEN: turn.ToColumn = 6; break;
                }

                if (field.IsTurnAllowed(turn))
                {
                    turns.Add(turn);
                    break;
                }
            }
            #endregion

            if (turns.Count == 0)
            {
                #region [FROM FIELD TO OUTPUT]
                for (byte col = 0; col < PlayingField.COLUMNS_FIELD; col++)
                {
                    int row = field.GetColumnLength(col) - 1;

                    if (row == -1) continue; //<- No cards in this stack

                    Card c = field[col, row];
                    //bool forcedMove = row == 0 || field[col, row - 1].Value > 0; //<- If it's the only card or there is a number-card behind it
                    bool forcedMove = c.Value > 0 || c.Suit == SuitEnum.ROSE; //<- If this is a playable card, it is a forced move

                    if (forcedMove)
                    {
                        Turn turn = new Turn { FromColumn = col, FromRow = (byte)row, FromTop = false, ToTop = true };
                        switch (c.Suit)
                        {
                            case SuitEnum.ROSE: turn.ToColumn = 3; break;
                            case SuitEnum.RED: turn.ToColumn = 4; break;
                            case SuitEnum.BLACK: turn.ToColumn = 5; break;
                            case SuitEnum.GREEN: turn.ToColumn = 6; break;
                        }

                        if (field.IsTurnAllowed(turn))
                        {
                            turns.Add(turn);
                            break;
                        }
                    }
                }
                #endregion
            }
            #endregion

            if (turns.Count == 0)
            {
                #region [DRAGONS]
                SuitEnum[] mergableDragons = field.FindMergableDragons();
                foreach (SuitEnum suit in mergableDragons)
                {
                    turns.Add(new Turn { MergeDragons = suit });
                }
                #endregion
            }

            if (turns.Count == 0)
            {
                List<Turn> toBufferTurns = new List<Turn>();
                #region [FROM FIELD TO FIELD / BUFFER]

                int[] neededOutputCardValue =
                {
                    field[4].Value + 1, //RED
                    field[5].Value + 1, //BLACK
                    field[6].Value + 1  //GREEN
                };

                foreach (Card card in MOVEMENT_PRIORITY)
                {
                    bool foundCard = false;
                    for (byte col = 0; col < PlayingField.COLUMNS_FIELD; col++)
                    {
                        int columnLength = field.GetColumnLength(col);
                        for (int row = columnLength - 1; row >= 0; row--) // <- reverse!
                        {
                            if (!field.IsMovable(col, row)) break; //<- If the top card isn't movable, the ones behind it definitely won't be
                            if (field[col, row] != card) continue; //<- If it isn't the card we are searching for, just continue.

                            if (row > 0 && field.IsMovable(col, row - 1)) //<- Check for illegal stack breaking
                            {
                                Card cardBehind = field[col, row - 1];
                                if (cardBehind.Suit == SuitEnum.RED && cardBehind.Value != neededOutputCardValue[0]) break;
                                else if (cardBehind.Suit == SuitEnum.BLACK && cardBehind.Value != neededOutputCardValue[1]) break;
                                else if (cardBehind.Suit == SuitEnum.GREEN && cardBehind.Value != neededOutputCardValue[2]) break;
                            }

                            Turn turn = new Turn { FromColumn = col, FromRow = (byte)row, FromTop = false, ToTop = false };

                            #region [TO FIELD]
                            Turn turnToEmptyStack = new Turn(); //<- Make sure empty stacks are prioritized last
                            bool evaluatedEmptyStack = false;
                            for (byte i = 0; i < PlayingField.COLUMNS_FIELD; i++)
                            {
                                if (i == col) continue;

                                if (evaluatedEmptyStack) continue; //<---------- AND : Make sure, empty stacks are routed to, only once
                                bool stackEmpty = field.GetColumnLength(i) == 0; //  |
                                if (stackEmpty) evaluatedEmptyStack = true; // <------

                                turn.ToColumn = i;
                                if (field.IsTurnAllowed(turn))
                                {
                                    if (stackEmpty)
                                    {
                                        turnToEmptyStack = turn;
                                    }
                                    else
                                    {
                                        turns.Add(turn);
                                    }
                                }
                            }

                            if (evaluatedEmptyStack) //<- Make sure empty stacks are prioritized last
                            {
                                turns.Add(turnToEmptyStack);
                            }
                            #endregion

                            #region [TO BUFFER]
                            turn.ToTop = true;
                            for (byte i = 0; i < PlayingField.COLUMNS_BUFFER; i++)
                            {
                                turn.ToColumn = i;
                                if (field.IsTurnAllowed(turn))
                                {
                                    toBufferTurns.Add(turn);
                                    break; // <- If it works once, it can be definitely be filled.
                                }
                            }
                            #endregion

                            if (card.Value > 0 || card.Suit == SuitEnum.ROSE)
                            {
                                foundCard = true; //<- These cards only exist once, so we can directly go to the next card
                            }
                        }

                        if (foundCard) break;
                    }
                }
                #endregion

                #region [FROM BUFFER TO FIELD]
                for (byte col = 0; col < PlayingField.COLUMNS_BUFFER; col++)
                {
                    if (field[col].Suit == SuitEnum.BLOCKED || field[col].Suit == SuitEnum.EMPTY) continue;

                    Turn turnToEmptyStack = new Turn(); //<- Make sure empty stacks are prioritized last
                    bool evaluatedEmptyStack = false;
                    Turn turn = new Turn { FromColumn = col, FromTop = true, ToTop = false };
                    for (byte i = 0; i < PlayingField.COLUMNS_FIELD; i++)
                    {
                        if (evaluatedEmptyStack) continue; //<---------- AND : Make sure, empty stacks are routed to, only once
                        bool stackEmpty = field.GetColumnLength(i) == 0; //  |
                        if (stackEmpty) evaluatedEmptyStack = true; // <------

                        turn.ToColumn = i;
                        if (field.IsTurnAllowed(turn))
                        {
                            if (stackEmpty)
                            {
                                turnToEmptyStack = turn;
                            }
                            else
                            {
                                turns.Add(turn);
                            }
                        }
                    }

                    if (evaluatedEmptyStack) //<- Make sure empty stacks are prioritized last
                    {
                        turns.Add(turnToEmptyStack);
                    }
                }
                #endregion

                turns.AddRange(toBufferTurns);
            }

            return turns;
        }

        private List<GameState> EvaluateState(PlayingField field, List<GameState> path)
        {
            if (path.Count < 200)
            {
                List<Turn> turns = FindAllPossibleTurns(field);
                int totalPossibilities = turns.Count;
                if (totalPossibilities == 0 && field.IsGameOver())
                {
                    GameState newState = new GameState();
                    newState.Fingerprints = path.Last().Fingerprints;
                    newState.PrecedingTurn = new Turn { Finished = true };
                    path.Add(newState);
                }
                else
                {
                    int localTries = 0;
                    Parallel.ForEach(turns, PARALLEL_OPTIONS, (t, loopState) =>//foreach (Turn t in turns)                    
                    {
                        GameState newState = new GameState();

                        lock (this)
                        {
                            this.tries++;
                            newState.Tries = localTries++;
                        }

                        newState.TotalPossibilities = totalPossibilities;
                        newState.MakeState(field, t);

                        if (!newState.HasEquivaltentStack(path))
                        {
                            List<GameState> newPath = new List<GameState>(path) { newState };
                            newPath = this.EvaluateState(newState.Field, newPath);
                            if (newPath.Count > path.Count + 1)
                            {
                                path = newPath;
                                loopState.Break();
                                //break;
                            }
                        }
                    });
                }
            }
            return path;
        }

        public List<GameState> FindSolution()
        {
            List<GameState> result = null;
            List<Turn> turns = FindAllPossibleTurns(this.Field);

            Parallel.ForEach(turns, PARALLEL_OPTIONS, (t, loopState) =>
            {
                GameState initialGameState = new GameState();
                initialGameState.MakeState(this.Field, t);
                List<GameState> path = this.EvaluateState(initialGameState.Field, new List<GameState> { initialGameState });

                if (path.Count > 1)
                {
                    result = path;
                    loopState.Stop();
                }
            });

            return result;
        }
    }
}
