using System.Collections.Generic;
using Game.Models;
using Game.Models.GameLoop;

namespace Game.Services.GameBoard.Helpers
{
    public static class BoardComboIndexFinder
    {
        public static ISet<ComboIndex> ComboIndexes(
            this PieceType[,] board
        )
        {
            var possibleCombos = new HashSet<ComboIndex>();
            for (var row = 1;
                 row < board.GetLength(0);
                 row++)
            {
                for (var column = 1;
                     column < board.GetLength(1);
                     column++)
                {
                    CheckComboPossibility(
                        board,
                        row,
                        column,
                        possibleCombos);
                }
            }
            return possibleCombos;
        }

        static void CheckComboPossibility(
            PieceType[,] board,
            int row,
            int column,
            ISet<ComboIndex> possibleCombos
        )
        {
            CheckVerticalSwap(
                board,
                row,
                column,
                possibleCombos);

            CheckHorizontalSwap(
                board,
                row,
                column,
                possibleCombos);
        }

        static void CheckVerticalSwap(
            PieceType[,] board,
            int row,
            int column,
            ISet<ComboIndex> possibleCombos
        )
        {
            var otherRow = row - 1;

            var otherPiece = board[otherRow,
                column];

            var piece = board[row,
                column];

            if (BoardComboValidator.GetComboDirection(
                    board,
                    otherRow,
                    column,
                    piece) != ComboDirection.Undefined || BoardComboValidator.GetComboDirection(
                    board,
                    row,
                    column,
                    otherPiece) != ComboDirection.Undefined)
            {
                possibleCombos.Add(
                    new ComboIndex
                    {
                        SwapOrientation = PieceSwapOrientation.Vertical,
                        RowSum = row + otherRow,
                        ColumSum = column
                    });
            }
        }

        static void CheckHorizontalSwap(
            PieceType[,] board,
            int row,
            int column,
            ISet<ComboIndex> possibleCombos
        )
        {
            var otherColumn = column - 1;

            var otherPiece = board[row,
                otherColumn];

            var piece = board[row,
                column];

            if (BoardComboValidator.GetComboDirection(
                    board,
                    otherColumn,
                    column,
                    piece) != ComboDirection.Undefined || BoardComboValidator.GetComboDirection(
                    board,
                    row,
                    column,
                    otherPiece) != ComboDirection.Undefined)
            {
                possibleCombos.Add(
                    new ComboIndex
                    {
                        SwapOrientation = PieceSwapOrientation.Horizontal,
                        RowSum = row,
                        ColumSum = column + otherColumn
                    });
            }
        }
    }
}