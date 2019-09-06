namespace SHENZENSolitaire.Game
{
    public struct Turn
    {
        public bool FromTop { get; set; }
        public byte FromColumn { get; set; }
        public byte FromRow { get; set; }
        
        public bool ToTop { get; set; }
        public byte ToColumn { get; set; }

        public SuitEnum MergeDragons { get; set; }

        public override string ToString()
        {
            if (MergeDragons == default)
            {
                return $"Turn: [{FromTop}, {FromColumn}, {FromRow}] -> [{ToTop}, {ToColumn}]";
            }
            else
            {
                return $"Turn: [Merge {MergeDragons} Dragons]";
            }
        }
    }
}
