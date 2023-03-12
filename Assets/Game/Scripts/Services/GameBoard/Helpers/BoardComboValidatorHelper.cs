namespace Game.Services.GameBoard.Helpers
{
    public static class BoardComboValidatorHelper
    {
        public static bool PieceWillCombo(
            int[,] board,
            int row,
            int column,
            int piece
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
            int[,] board,
            int row,
            int column,
            int piece
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
            int[,] board,
            int row,
            int column,
            int piece
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