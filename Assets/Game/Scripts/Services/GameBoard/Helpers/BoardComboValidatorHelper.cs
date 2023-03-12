using Game.Models;

namespace Game.Services.GameBoard.Helpers
{
    public static class BoardComboValidatorHelper
    {
        public static bool PieceWillCombo(
            PieceType[,] board,
            int row,
            int column,
            PieceType piece
        )
        {
            return PiecesOnTopCombo(
                    board,
                    row,
                    column,
                    piece) 
             || PiecesToTheLeftCombo(
                    board,
                    row,
                    column,
                    piece);
        }

        public static bool PiecesOnTopCombo(
            PieceType[,] board,
            int row,
            int column,
            PieceType piece
        )
        {
            if (row < 2)
            {
                //Too close to top to combo
                return false;
            }

            return 
                piece 
             == board[row - 1, column] 
             && piece
             == board[row - 2, column];
        }

        public static bool PiecesToTheLeftCombo(
            PieceType[,] board,
            int row,
            int column,
            PieceType piece
        )
        {
            if (column < 2)
            {
                //Too close to the left to combo
                return false;
            }

            return piece 
             == board[row - 1, column]
             && piece 
             == board[row - 2, column];
        }
    }
}