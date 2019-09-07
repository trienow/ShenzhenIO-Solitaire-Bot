using SHENZENSolitaire.Game;
using System;
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
            new Card(0, SuitEnum.ROSE),
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
            Card.DRAGON_RED,
            Card.DRAGON_GREEN,
            Card.DRAGON_BLACK
        };
        private static readonly ParallelOptions PARALLEL_OPTIONS = new ParallelOptions { MaxDegreeOfParallelism = 8 };

        public int Tries { get; set; } = 0;
        //private static readonly LimitedConcurrencyLevelTaskScheduler LIMITED_TASK_SCHED = new LimitedConcurrencyLevelTaskScheduler(8);
        //private static readonly ParallelOptions PARALLEL_OPTIONS = new ParallelOptions { MaxDegreeOfParallelism = 2, TaskScheduler = LIMITED_TASK_SCHED };

        public Player(PlayingField field)
        {
            this.Field = field;
        }

        static List<Turn> FindOptimalTurn(PlayingField field)
        {
            List<Turn> turns = new List<Turn>(1);

            //Try move cards from the buffer to the output
            #region [TO OUTPUT FROM BUFFER]
            int[] neededOutputCardValue = {
                field[4].Value + 1, //RED
                field[5].Value + 1, //BLACK
                field[6].Value + 1  //GREEN
            };

            for (byte col = 0; col < PlayingField.COLUMNS_BUFFER; col++)
            {
                Card c = field[col];

                if (c.Value == 0) continue;
                Turn turn = new Turn { FromColumn = col, FromTop = true, ToTop = true };
                if (c.Suit == SuitEnum.RED && c.Value == neededOutputCardValue[0])
                {
                    turn.ToColumn = 4;
                }
                else if (c.Suit == SuitEnum.BLACK && c.Value == neededOutputCardValue[0])
                {
                    turn.ToColumn = 5;
                }
                else if (c.Suit == SuitEnum.GREEN && c.Value == neededOutputCardValue[0])
                {
                    turn.ToColumn = 6;
                }

                if (turn.ToColumn > 0 && field.IsTurnAllowed(turn))
                {
                    turns.Add(turn);
                    break;
                }
            }
            #endregion

            if (turns.Count == 0)
            {
                #region [TO OUTPUT FROM FIELD]
                for (byte col = 0; col < PlayingField.COLUMNS_FIELD; col++)
                {
                    int row = field.GetColumnLength(col) - 1;
                    if (row == -1) continue; //<- No cards in this stack

                    Card c = field[col, row];
                    if (c.Value > 0 || c.Suit == SuitEnum.ROSE) //<- If it is a card, which can go to the output
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

            if (turns.Count == 0)
            {
                #region [MERGE DRAGONS]
                SuitEnum[] mergableDragons = field.FindMergableDragons();
                foreach (SuitEnum suit in mergableDragons)
                {
                    turns.Add(new Turn { MergeDragons = suit });
                    break;
                }
                #endregion
            }

            return turns;
        }

        /// <summary>
        /// Tries to find a <see cref="Turn"/> stack cards together without breaking existing stacks or exposing dragons
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        static List<Turn> FindStackingTurn(PlayingField field)
        {
            List<Turn> turns = new List<Turn>(1);

            Card[] neededOutputCards = {
                new Card((byte)(field[4].Value + 1), SuitEnum.RED),
                new Card((byte)(field[5].Value + 1), SuitEnum.BLACK),
                new Card((byte)(field[6].Value + 1), SuitEnum.GREEN)
            };

            int[] rows = new int[PlayingField.COLUMNS_FIELD];
            for (byte col = 0; col < PlayingField.COLUMNS_FIELD; col++)
            {
                rows[col] = field.GetColumnLength(col);
            }

            #region [TRY STACKING WITHOUT EXPOSING DRAGONS]
            byte foundTurnValue = 0;
            for (byte refValue = 9; refValue > foundTurnValue; refValue--) //<- Try to move the highest cards possible
            {
                for (byte colFrom = 0; colFrom < PlayingField.COLUMNS_FIELD; colFrom++)
                {
                    for (byte rowFrom = 0; rowFrom < rows[colFrom]; rowFrom++)
                    {
                        if (field[colFrom, rowFrom].Value != refValue || !field.IsMovable(colFrom, rowFrom)) continue;

                        bool routedToEmpty = false;
                        Turn t = new Turn { FromColumn = colFrom, FromRow = rowFrom, FromTop = false, ToTop = false };
                        for (byte colTo = 0; colTo < PlayingField.COLUMNS_FIELD; colTo++) //<- Search for an output stack
                        {
                            if (colTo == colFrom) continue;

                            t.ToColumn = colTo;
                            if (rowFrom == 0) //<- If the card is on the bottom
                            {
                                if (rows[colTo] > 0 && field.IsTurnAllowed(t)) //<- AND if the card should be moved to a stack, that is not empty
                                {
                                    turns.Add(t);
                                    foundTurnValue = refValue;
                                }
                            }
                            else
                            {
                                Card cardBehind = field[colFrom, rowFrom - 1];

                                bool allowedToMove = cardBehind.Value > 0;//<- Make sure we are not exposing a dragon
                                allowedToMove = allowedToMove && !(rows[colTo] == 0 && routedToEmpty); //<- We don't want to route to an empty spot twice with the same card
                                allowedToMove = allowedToMove && (!field.IsMovable(colFrom, rowFrom - 1) || neededOutputCards.Contains(cardBehind)); //<- Make sure we are not breaking stacks. The card behind the proposed one may not be movable OR the card is needed on an output stack.
                                allowedToMove = allowedToMove && field.IsTurnAllowed(t); //<- Make sure the turn is allowed at all

                                if (allowedToMove)
                                {
                                    turns.Add(t);
                                    foundTurnValue = refValue;
                                    routedToEmpty |= rows[colTo] == 0; //<- Set the routed to empty flag
                                }
                            }

                            t.ToColumn = colTo;
                        }
                    }
                }
            }
            #endregion

            return turns;
        }

        static List<Turn> FindOtherTurns(PlayingField field)
        {
            List<Turn> turns = new List<Turn>();

            int[] rows = new int[PlayingField.COLUMNS_FIELD];
            for (byte col = 0; col < PlayingField.COLUMNS_FIELD; col++)
            {
                rows[col] = field.GetColumnLength(col);
            }

            #region [FROM FIELD TO FIELD / BUFFER]
            foreach (Card refCard in MOVEMENT_PRIORITY)
            {
                bool transferedToEmpty = false; //<- We only want to transfer to an empty slot once, since the outcomes are the same
                for (byte colFrom = 0; colFrom < PlayingField.COLUMNS_FIELD; colFrom++)
                {
                    for (byte rowFrom = 0; rowFrom < rows[colFrom]; rowFrom++)
                    {
                        Card c = field[colFrom, rowFrom];
                        if (c != refCard || !field.IsMovable(colFrom, rowFrom)) continue;

                        Turn t = new Turn { FromColumn = colFrom, FromRow = rowFrom, FromTop = false, ToTop = false };

                        #region [TO FIELD]
                        for (byte colTo = 0; colTo < PlayingField.COLUMNS_FIELD; colTo++)
                        {
                            if (colFrom == colTo) continue;

                            t.ToColumn = colTo;
                            if (field.IsTurnAllowed(t))
                            {
                                if (c.Value == 0 || (rowFrom > 0 && field[colFrom, rowFrom - 1].Value == 0))
                                {
                                    turns.Add(t);
                                }
                                else if (rows[colTo] == 0)
                                {
                                    if (!transferedToEmpty)
                                    {
                                        turns.Add(t);
                                        transferedToEmpty = true;
                                    }
                                }
                                else
                                {
                                    turns.Add(t);
                                }
                            }
                        }
                        #endregion

                        #region [TO BUFFER]
                        t.ToTop = true;
                        for (byte colTo = 0; colTo < PlayingField.COLUMNS_BUFFER; colTo++)
                        {
                            if (field[colTo].Suit != SuitEnum.EMPTY) continue;

                            t.ToColumn = colTo;
                            if (field.IsTurnAllowed(t))
                            {
                                turns.Add(t);
                                break; //<- It is enough if it works once
                            }
                        }
                        #endregion

                        if (c.Value > 0) break; //<- There is only one of each number card
                    }
                }
            }
            #endregion

            #region [FROM BUFFER TO FIELD]
            for (byte colFrom = 0; colFrom < PlayingField.COLUMNS_BUFFER; colFrom++)
            {
                bool transferedToEmpty = false; //<- We only want to transfer to an empty slot once, since the outcomes are the same
                Turn t = new Turn { FromColumn = colFrom, FromTop = true, ToTop = false };
                for (byte colTo = 0; colTo < PlayingField.COLUMNS_FIELD; colTo++)
                {
                    t.ToColumn = colTo;
                    if (field.IsTurnAllowed(t))
                    {
                        if (field[colFrom].Value > 0)
                        {
                            if (rows[colTo] == 0)
                            {
                                if (!transferedToEmpty)
                                {
                                    turns.Add(t); //<- Only enqueue, if we haven't yet put the card onto an empty slot
                                    transferedToEmpty = true;
                                }
                            }
                            else
                            {
                                turns.Add(t);
                            }
                        }
                        else
                        {
                            turns.Add(t);
                            break; //<- Dragons can only be put in empty columns. Since we found one, we don't need to look for more since they have the same outcome
                        }
                    }
                }
            }
            #endregion

            return turns;
        }

        public static List<Turn> FindTurns(PlayingField field)
        {
            List<Turn> turns = FindOptimalTurn(field);

            if (turns.Count == 0)
            {
                turns = FindStackingTurn(field);
            }

            if (turns.Count == 0)
            {
                turns = FindOtherTurns(field);
            }

            return turns;
        }

        public GameState FindSolution()
        {
            Queue<GameState> states = new Queue<GameState>();
            states.Enqueue(new GameState(Field));
            int lowestCardCount = 41;
            GameState finalState = null;
            float thresholdNumerator = 524288f;

            while (states.Count > 0)
            {
                int stateCount = states.Count + 1;
                float threshold = Math.Max(Math.Min(thresholdNumerator / stateCount, 20), 2);

                Queue<GameState> nextStates = new Queue<GameState>(states.Count);
                Parallel.ForEach(states, PARALLEL_OPTIONS, (currentState, loopState) =>
                {
                    if (stateCount > 2097152 && currentState.RemainingCards > lowestCardCount) return; //<- Out of Memory Saver
                    if (currentState.RemainingCards - threshold > lowestCardCount) return;

                    List<Turn> turns = FindTurns(currentState.FieldResult);
                    foreach (Turn turn in turns)
                    {
                        lock (this)
                        {
                            Tries++;
                        }

                        GameState newState = new GameState(currentState, turn);
                        int remainingCards = newState.PerformTurn();
                        if (remainingCards == 0)
                        {
                            finalState = newState;
                            break;
                        }
                        else if (newState.IsStateUnique())
                        {
                            lock (this)
                            {
                                nextStates.Enqueue(newState);

                                if (remainingCards < lowestCardCount)
                                {
                                    lowestCardCount = remainingCards;

                                    if (lowestCardCount < 12)
                                    {
                                        thresholdNumerator = 131072f;
                                    }

                                    Console.WriteLine($"Cards left to distribute: {lowestCardCount,-2}  Threshold: {lowestCardCount + (int)threshold,-2}  Current States: {stateCount,-5}  Total Tries: {Tries}");
                                }
                            }
                        }
                    }
                });

                states = nextStates;
            }

            states.TrimExcess();

            return finalState;
        }
    }
}
