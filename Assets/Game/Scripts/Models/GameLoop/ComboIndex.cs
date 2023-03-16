namespace Game.Models.GameLoop
{
    public struct ComboIndex
    {
        public PieceSwapOrientation SwapOrientation { get; set; }
        public int RowSum { get; set; }
        public int ColumSum { get; set; }
    }
}