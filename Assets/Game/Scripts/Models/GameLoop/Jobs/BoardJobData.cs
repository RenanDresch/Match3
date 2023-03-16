using System;
using Unity.Collections;

namespace Game.Models.GameLoop.Jobs
{
    [Serializable]
    public class BoardJobData
    {
        public uint Seed;
        
        public int Rows;
        public int Columns;
        public int MinimumRequiredCombos;
        
        public NativeArray<PieceType> AvailablePieces;
        public NativeArray<PieceType> PiecePool;
        public NativeArray<PieceType> Board;
        public NativeArray<ComboIndex> Combos;
    }
}