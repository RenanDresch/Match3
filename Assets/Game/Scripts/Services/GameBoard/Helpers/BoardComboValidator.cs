using Game.Models;
using Game.Models.GameLoop;

namespace Game.Services.GameBoard.Helpers
{
    public static class BoardComboValidator
    {
        public static bool PieceWillCombo(
            PieceType[,] board,
            int row,
            int column,
            PieceType piece
        )
        {
            return TopCombo(
                board,
                row,
                column,
                piece) || LeftCombo(
                board,
                row,
                column,
                piece);
        }

        public static bool TopCombo(
            PieceType[,] board,
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

            return piece == board[row - 1,
                column] && piece == board[row - 2,
                column];
        }

        public static bool BottomCombo(
            PieceType[,] board,
            int row,
            int column,
            PieceType piece
        )
        {
            if (row >= board.GetLength(1) - 2)
            {
                //Too close to the bottom
                return false;
            }

            return piece == board[row + 1,
                column] && piece == board[row + 2,
                column];
        }

        public static bool LeftCombo(
            PieceType[,] board,
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

            return piece == board[row,
                column - 1] && piece == board[row,
                column - 2];
        }

        public static bool RightCombo(
            PieceType[,] board,
            int row,
            int column,
            PieceType piece
        )
        {
            if (column >= board.GetLength(1) - 2)
            {
                //Too close to the right
                return false;
            }

            return piece == board[row,
                column + 1] && piece == board[row,
                column + 2];
        }

        public static ComboDirection GetComboDirection(
            PieceType[,] board,
            int row,
            int column,
            PieceType piece
        )
        {
            if (TopCombo(
                    board,
                    row,
                    column,
                    piece))
            {
                return ComboDirection.Up;
            }
            
            if (BottomCombo(
                    board,
                    row,
                    column,
                    piece))
            {
                return ComboDirection.Down;
            }
            
            if (LeftCombo(
                    board,
                    row,
                    column,
                    piece))
            {
                return ComboDirection.Left;
            }
            
            if (RightCombo(
                    board,
                    row,
                    column,
                    piece))
            {
                return ComboDirection.Right;
            }
            
            return ComboDirection.Undefined;
        }
    }
}