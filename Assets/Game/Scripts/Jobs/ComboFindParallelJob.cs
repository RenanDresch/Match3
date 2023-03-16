using System;
using Game.Jobs.Helpers;
using Game.Models;
using Game.Models.GameLoop;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace Game.Jobs
{
    /// <summary>
    /// ComboFindParallelFind is a parallel job that
    /// iterates through the board by index, swapping the
    /// piece at the index with the one above and with the one
    /// to the left, filling the Combos array as it goes
    ///
    /// Each index can run a distinct thread
    /// </summary>
    [BurstCompile]
    public struct ComboFindParallelJob : IJobParallelFor
    {
        public int Rows;
        public int Columns;
        
        [ReadOnly]
        public NativeArray<PieceType> Board;
        public NativeArray<ComboIndex> Combos;

        public void Execute(
            int index
        )
        {
            //We check combos by swapping every single piece with the top one and the left one
            //Piece 0 [0,0] is never tested
            if (index > 0)
            {
                var row = index / Columns;
                var column = index % Columns;

                var verticalComboCacheIndex = (index - 1) * 2;
                var horizontalComboCacheIndex = (index - 1) * 2 + 1;

                Combos[verticalComboCacheIndex] = VerticalCombo(
                    row,
                    column);
                
                Combos[horizontalComboCacheIndex] = HorizontalCombo(
                    row,
                    column);
            }
        }

        ComboIndex VerticalCombo(
            int rowA,
            int column
        )
        {
            if (rowA < 1)
            {
                return default;
            }
            
            var rowB = rowA - 1;
            
            var pieceA = Board[column + rowA * Columns];
            var pieceB = Board[column + rowB * Columns];

            var pieceAWillCombo = JobComboFinder.WillCombo(
                this,
                rowA,
                column,
                pieceB,
                ComboDirection.Down);
            
            var pieceBWillCombo = JobComboFinder.WillCombo(
                this,
                rowB,
                column,
                pieceA,
                ComboDirection.Down);
            
            return pieceAWillCombo || pieceBWillCombo
                ? new ComboIndex
                {
                    RowSum = rowA + rowB,
                    ColumSum = column,
                    SwapOrientation = PieceSwapOrientation.Vertical
                }
                : default;
        }
        
        ComboIndex HorizontalCombo(
            int row,
            int columnA
        )
        {
            if (columnA < 1)
            {
                return default;
            }
            
            var columnB = columnA - 1;
            
            var pieceA = Board[columnA + row * Columns];
            var pieceB = Board[columnB + row * Columns];

            var pieceAWillCombo = JobComboFinder.WillCombo(
                this,
                row,
                columnA,
                pieceB,
                ComboDirection.Right);
            
            var pieceBWillCombo = JobComboFinder.WillCombo(
                this,
                row,
                columnB,
                pieceA,
                ComboDirection.Left);
            
            return pieceAWillCombo || pieceBWillCombo
                ? new ComboIndex
                {
                    RowSum = row,
                    ColumSum = columnA + columnB,
                    SwapOrientation = PieceSwapOrientation.Vertical
                }
                : default;
        }
    }
}