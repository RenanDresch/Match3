using Game.Models;
using Game.Services.GameBoard.Helpers;
using Gaze.Utilities;

namespace Game.Services.GameBoard
{
    public class GameBoardBuilderService : BaseService
    {

        public GameBoardBuilderService(
            IDestroyable destroyable,
            ModelsRepositoryWrapper models
        ) : base(
            destroyable,
            models)
        {
        }

        /// <summary>
        /// Fills from top to bottom, left to right while checking already
        /// placed pieces to avoid generating combos on start, while also caching
        /// possible combos as it goes
        /// </summary>
        /// <param name="board">The game board</param>
        void FillAvoidingCombos(
            int[,] board
        )
        {
            for (var row = 0;
                 row < board.GetLength(0);
                 row++)
            {
                for (var column = 0;
                     column < board.GetLength(1);
                     column++)
                {
                    board[row,
                        column] = GetRandomPieceAvoidingCombos(board, row, column);

                    CheckAndCachePossibleCombos(
                        board,
                        row,
                        column);
                }
            }

            //No combos are possible with this board, re-generate
            if (combos.Count < 1)
            {
                FillAvoidingCombos(board);
            }
        }
        
        void CheckAndCachePossibleCombos(
            int[,] board,
            int row,
            int column,
            int piece
        )
        {
            ByMovingUp(board, row, column, piece);
            ByMovingLeft(board, row, column, piece);
        }
        
        void ByMovingUp(
            int[,] board,
            int row,
            int column,
            int piece)
        {
            var willCombo = BoardComboValidatorHelper.PiecesOnTopCombo(
                board,
                row - 1,
                column,
                piece);
        }
        
        void ByMovingLeft(
            int[,] board,
            int row,
            int column,
            int piece)
        {
            var willCombo = BoardComboValidatorHelper.PiecesToTheLeftCombo(
                board,
                row,
                column - 1,
                piece);
        }


        int GetRandomPieceAvoidingCombos(
            int[,] board,
            int row,
            int column,
            int piece
        )
        {
            
        }
    }
}