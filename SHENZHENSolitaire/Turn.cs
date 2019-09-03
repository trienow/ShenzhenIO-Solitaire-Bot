namespace SHENZENSolitaire
{
    public struct Turn
    {
        public bool FromTop { get; set; }
        public byte FromColumn { get; set; }
        public byte FromRow { get; set; }
        
        public bool ToTop { get; set; }
        public byte ToColumn { get; set; }

        public override string ToString()
        {
            return $"Turn: [{FromTop}, {FromColumn}, {FromRow}] -> [{ToTop}, {ToColumn}]";
        }
    }
}
