namespace SHENZENSolitaire
{
    public struct Turn
    {
        public bool FromTop { get; set; }
        public int FromColumn { get; set; }
        public int FromRow { get; set; }
        
        public bool ToTop { get; set; }
        public int ToColumn { get; set; }

        public override string ToString()
        {
            return $"Turn: [{FromTop}, {FromColumn}, {FromRow}] -> [{ToTop}, {ToColumn}]";
        }
    }
}
