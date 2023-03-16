using Game.Models;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace Game.Jobs
{
    [BurstCompile]
    public struct BoardBuildingJob : IJob
    {
        public uint Seed;
        
        public int Rows;
        public int Columns;
        public int MinimumRequiredCombos;

        [ReadOnly]
        public NativeArray<PieceType> AvailablePieces;
        
        public NativeArray<PieceType> PiecePool;
        public  NativeArray<PieceType> Board;

        public void Execute()
        {
            var combosAvailable = 0;

            var random = new Random(Seed);
            while (combosAvailable < MinimumRequiredCombos)
            {
                BuildBoard(random);
                //GetComboCount();
                combosAvailable++;
            }
        }
        
        void BuildBoard(
            Random random
        )
        {
            for (var row = 0;
                 row < Rows;
                 row++)
            {
                for (var column = 0;
                     column < Columns;
                     column++)
                {
                    var index = column + row * column;
                    Board[index] = GetRandomPieceAvoidingCombos(row, column, random);
                }
            }
        }
        
        PieceType GetRandomPieceAvoidingCombos(
            int row,
            int column,
            Random random
        )
        {
            var availableValidPieces = 0;
            for (var i = 0;
                 i < AvailablePieces.Length;
                 i++)
            {
                if (PieceWillNotCombo(
                        row,
                        column,
                        AvailablePieces[i]))
                {
                    PiecePool[availableValidPieces] = AvailablePieces[i];
                    availableValidPieces++;
                }
            }

            return PiecePool[random.NextInt(
                0,
                availableValidPieces)];
        }

        bool PieceWillNotCombo(
            int row,
            int column,
            PieceType piece
        )
        {
            return !TopCombo(
                row,
                column,
                piece) && !LeftCombo(
                row,
                column,
                piece);
        }

        bool TopCombo(
            int row,
            int column,
            PieceType piece
        )
        {
            if (row < 2)
            {
                //Too close to the top
                return false;
            }

            var pieceAbove = column + (row - 1) * Columns;
            var pieceAbove2 = column + (row - 2) * Columns;
            
            return piece == Board[pieceAbove] && piece == Board[pieceAbove2];
        }

        bool LeftCombo(
            int row,
            int column,
            PieceType piece
        )
        {
            if (column < 2)
            {
                //Too close to the left
                return false;
            }

            var pieceLeft = column-1 + row * Columns;
            var pieceLeft2 = column-2 + row * Columns;
            
            return piece == Board[pieceLeft] && piece == Board[pieceLeft2];
        }
    }
}