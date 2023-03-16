using System;
using System.Collections.Generic;
using System.Linq;
using Game.Models;
using Game.Models.GameLoop;
using Game.Services.GameBoard.Helpers;
using Gaze.Utilities;
using Random = UnityEngine.Random;

namespace Game.Services.GameBoard
{
    public class GameBoardBuilderService : BaseService
    {
        readonly PieceType[] pieceTypePool = (PieceType[])Enum.GetValues(typeof(PieceType));
        
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
        /// possible combos
        /// </summary>
        /// <param name="board">The game board</param>
        /// <param name="possibleCombos">Caches the possible combos for the generated board</param>
        void FillAvoidingCombos(
            PieceType[,] board,
            ISet<ComboIndex> possibleCombos
        )
        {
            var combosAvailable = 0;

            while (combosAvailable < 1)
            {
                BuildBoard(board);
                possibleCombos = board.ComboIndexes();
                combosAvailable = possibleCombos.Count;
            }
        }

        void BuildBoard(
            PieceType[,] board
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
                }
            }
        }
        
        PieceType GetRandomPieceAvoidingCombos(
            PieceType[,] board,
            int row,
            int column
        )
        {
            var possiblePieces = pieceTypePool.Where(
                pieceType => !BoardComboValidator.PieceWillCombo(
                    board,
                    row,
                    column,
                    pieceType)).ToArray();

            return possiblePieces[Random.Range(
                0,
                possiblePieces.Length)];
        }
    }
}