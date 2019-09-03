using System.Collections.Generic;
using System.Linq;

namespace SHENZENSolitaire
{
    class GameState
    {
        public const byte TOP_FINGERPRINT_INDEX = PlayingField.COLUMNS_FIELD;

        public byte[][] Fingerprints { get; set; }
        public Turn PrecedingTurn { get; set; }

        public bool HasEquivaltentStack(List<GameState> states)
        {
            if (PrecedingTurn.ToTop)
            {
                byte col = TOP_FINGERPRINT_INDEX;
                foreach (GameState state in states)
                {
                    if (state.Fingerprints[col].SequenceEqual(Fingerprints[col]))
                    {
                        return true;
                    }
                }
            }
            else
            {
                byte[] targetFingerprint = Fingerprints[PrecedingTurn.ToColumn];
                foreach (GameState state in states)
                {
                    for (byte col = 0; col < PlayingField.COLUMNS_FIELD; col++)
                    {
                        if (targetFingerprint.SequenceEqual(state.Fingerprints[col]))
                        {
                            return true;
                        }
                    }

                }
            }

            return false;
        }
    }
}
