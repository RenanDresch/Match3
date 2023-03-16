using System;
using Game.Models;
using Game.Models.GameLoop;

namespace Game.Jobs.Helpers
{
    public static class JobComboFinder
    {
        public static bool WillCombo(
            ComboFindParallelJob job,
            int row,
            int column,
            PieceType piece,
            ComboDirection movementDirection
        )
        {
            return movementDirection switch
            {
                ComboDirection.Up => HasVerticalCombo(
                    job,
                    row,
                    column,
                    piece,
                    end: 2) || HasHorizontalCombo(
                    job,
                    row,
                    column,
                    piece),
                ComboDirection.Down => HasVerticalCombo(
                    job,
                    row,
                    column,
                    piece,
                    start: 2) || HasHorizontalCombo(
                    job,
                    row,
                    column,
                    piece),
                ComboDirection.Left => HasVerticalCombo(
                    job,
                    row,
                    column,
                    piece) || HasHorizontalCombo(
                    job,
                    row,
                    column,
                    piece,
                    end: 2),
                ComboDirection.Right => HasVerticalCombo(
                    job,
                    row,
                    column,
                    piece) || HasHorizontalCombo(
                    job,
                    row,
                    column,
                    piece,
                    start: 2),
                ComboDirection.Undefined => throw new ArgumentOutOfRangeException(
                    nameof(movementDirection),
                    movementDirection,
                    null),
                _ => throw new ArgumentOutOfRangeException(
                    nameof(movementDirection),
                    movementDirection,
                    null)
            };
        }


        /*
        Checks:
        0 -> 1 -> P -> 3 -> 4
        where P is at index 2
        
        x x 0 x x
        x x 1 x x
        x x P x x
        x x 3 x x
        x x 4 x x
        */
        
        static bool HasVerticalCombo(
            ComboFindParallelJob job,
            int row,
            int column,
            PieceType inputPiece,
            int start = 0,
            int end = 4
        )
        {
            var lastPiece = PieceType.Undefined;
            var sequence = 0;

            for (var i = start;
                 i <= end;
                 i++)
            {
                var targetRow = row - 2 + i;
                if (targetRow < 0)
                {
                    continue;
                }

                var targetIndex = column + targetRow * job.Columns;

                var currentPiece = i == 2
                    ? inputPiece
                    : job.Board[targetIndex];

                if (lastPiece == currentPiece)
                {
                    sequence++;
                    if (sequence >= 3)
                    {
                        return true;
                    }
                }
                else
                {
                    sequence = 0;
                    lastPiece = currentPiece;
                }
            }

            return false;
        }


        /*
        Checks:
        0 -> 1 -> P -> 2 -> 3
        x x x x x
        x x x x x
        0 1 P 2 3
        x x x x x
        x x x x x
        */
        
        static bool HasHorizontalCombo(
            ComboFindParallelJob job,
            int row,
            int column,
            PieceType inputPiece,
            int start = 0,
            int end = 4
        )
        {
            var lastPiece = PieceType.Undefined;
            var sequence = 0;

            for (var i = start;
                 i <= end;
                 i++)
            {
                var targetColumn = column - 2 + i;
                if (targetColumn < 0)
                {
                    continue;
                }

                var targetIndex = targetColumn + row * job.Columns;

                var currentPiece = i == 2
                    ? inputPiece
                    : job.Board[targetIndex];

                if (lastPiece == currentPiece)
                {
                    sequence++;
                    if (sequence >= 3)
                    {
                        return true;
                    }
                }
                else
                {
                    sequence = 0;
                    lastPiece = currentPiece;
                }
            }

            return false;
        }
    }
}