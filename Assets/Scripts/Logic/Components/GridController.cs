using DG.Tweening;
using Game.Contracts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Logic
{
    public class GridController : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private Gem genericGemPrefab = default;

        private GemGrid gemGrid = new GemGrid();

        #endregion

        #region Properties

        public Gem[] AllGems
        {
            get
            {
                return gemGrid.Grid.Cast<Gem>().ToArray();
            }
        }

        #endregion

        #region Private Methods

        private List<Gem> GetRowCombo(Gem gem)
        {
            var rowCombo = new List<Gem>() { gem };

            var currentColumn = gem.Position.Column - 1;

            while (currentColumn > -1)
            {
                if (gemGrid.Grid[gem.Position.Row, currentColumn].Equals(gem.SpriteRenderer.sprite))
                {
                    rowCombo.Add(gemGrid.Grid[gem.Position.Row, currentColumn]);
                    currentColumn--;
                }
                else
                {
                    break;
                }
            }

            currentColumn = gem.Position.Column + 1;

            while (currentColumn < gemGrid.Grid.GetLength(1))
            {
                if (gemGrid.Grid[gem.Position.Row, currentColumn].Equals(gem.SpriteRenderer.sprite))
                {
                    rowCombo.Add(gemGrid.Grid[gem.Position.Row, currentColumn]);
                    currentColumn++;
                }
                else
                {
                    break;
                }
            }

            return rowCombo;
        }

        private List<Gem> GetColumnCombo(Gem gem)
        {
            var columnCombo = new List<Gem>() { gem };

            var currentRow = gem.Position.Row - 1;

            while (currentRow > -1)
            {
                if (gemGrid.Grid[currentRow, gem.Position.Column].Equals(gem.SpriteRenderer.sprite))
                {
                    columnCombo.Add(gemGrid.Grid[currentRow, gem.Position.Column]);
                    currentRow--;
                }
                else
                {
                    break;
                }
            }

            currentRow = gem.Position.Row + 1;

            while (currentRow < gemGrid.Grid.GetLength(0))
            {
                if (gemGrid.Grid[currentRow, gem.Position.Column].Equals(gem.SpriteRenderer.sprite))
                {
                    columnCombo.Add(gemGrid.Grid[currentRow, gem.Position.Column]);
                    currentRow++;
                }
                else
                {
                    break;
                }
            }

            return columnCombo;
        }

        #endregion

        #region Public Methods

        public void BuildGrid()
        {
            if (gemGrid.Grid != null)
            {
                for (int m = 0; m < gemGrid.Grid.GetLength(0); m++)
                {
                    for (int n = 0; n < gemGrid.Grid.GetLength(1); n++)
                    {
                        Destroy(gemGrid.Grid[m, n].gameObject);
                    }
                }
            }

            gemGrid.Grid = new Gem[GameSettingsManager.Instance.GridXSize,
                GameSettingsManager.Instance.GridYSize];

            for (int m = 0; m < gemGrid.Grid.GetLength(0); m++)
            {
                for (int n = 0; n < gemGrid.Grid.GetLength(1); n++)
                {
                    gemGrid.Grid[m, n] = Instantiate(genericGemPrefab).GetComponent<Gem>();

                    gemGrid.Grid[m, n].Position.Row = m;
                    gemGrid.Grid[m, n].Position.Column = n;

                    var gem = GameSettingsManager.Instance.GetRandomGem();

                    while (gemGrid.CanCompleteBefore(m, n, gem))
                    {
                        gem = GameSettingsManager.Instance.GetRandomGem();
                    }

                    gemGrid.Grid[m, n].SpriteRenderer.sprite = gem;
                }
            }

            if (!HasAvailableCombos())
            {
                ShufleGrid();
            }

            for (int m = 0; m < gemGrid.Grid.GetLength(0); m++)
            {
                for (int n = 0; n < gemGrid.Grid.GetLength(1); n++)
                {
                    gemGrid.Grid[m, n].transform.localScale = Vector3.zero;
                    gemGrid.Grid[m, n].transform.position = GetGemWorldPosition(gemGrid.Grid[m, n]);
                }
            }
        }

        public Vector2 GetGemWorldPosition(Gem gem)
        {
            var xPosition = (-((float)GameSettingsManager.Instance.GridXSize - 1) / 2) + gem.Position.Column;
            var yPosition = (((float)GameSettingsManager.Instance.GridYSize - 1) / 2) - gem.Position.Row;

            return new Vector2(xPosition, yPosition);
        }

        public bool CanSwapGems(Gem gemA, Gem gemB)
        {
            bool canSwap = false;

            if (gemA.Position.Row == gemB.Position.Row)
            {
                canSwap = (gemB.Position.Column == (gemA.Position.Column + 1) ||
                    gemB.Position.Column == (gemA.Position.Column - 1));

            }

            else if (gemA.Position.Column == gemB.Position.Column)
            {
                canSwap = (gemB.Position.Row == (gemA.Position.Row + 1) ||
                    gemB.Position.Row == (gemA.Position.Row - 1));
            }

            return canSwap;
        }

        public void SwapGems(Gem gemA, Gem gemB)
        {
            gemGrid.SwapGemsPositions(gemA, gemB);
        }

        public Gem GetGemAtDirection(Gem gem, SwipeDirection direction)
        {
            switch (direction)
            {
                case SwipeDirection.Up:
                    if (gem.Position.Row > 0)
                    {
                        return gemGrid.Grid[gem.Position.Row - 1, gem.Position.Column];
                    }
                    break;

                case SwipeDirection.Down:
                    if (gem.Position.Row < gemGrid.Grid.GetLength(0) - 1)
                    {
                        return gemGrid.Grid[gem.Position.Row + 1, gem.Position.Column];
                    }
                    break;

                case SwipeDirection.Left:
                    if (gem.Position.Column > 0)
                    {
                        return gemGrid.Grid[gem.Position.Row, gem.Position.Column - 1];
                    }
                    break;

                case SwipeDirection.Right:
                    if (gem.Position.Column < gemGrid.Grid.GetLength(1) - 1)
                    {
                        return gemGrid.Grid[gem.Position.Row, gem.Position.Column + 1];
                    }
                    break;
            }

            return null;
        }

        public List<List<Gem>> GetGemsCombos(Gem[] gems)
        {
            var gemCombos = new List<List<Gem>>();

            foreach (var gem in gems)
            {
                var columnCombo = GetColumnCombo(gem);
                var rowCombo = GetRowCombo(gem);

                if (columnCombo.Count >= 3)
                {
                    gemCombos.Add(columnCombo);
                }
                if (rowCombo.Count >= 3)
                {
                    gemCombos.Add(rowCombo);
                }
            }

            return gemCombos;
        }

        public Gem[] DropGems(Gem[] removedGems)
        {
            var modifiedColumns = removedGems.Select(x => x.Position.Column).Distinct().ToArray();
            var droppedGems = new List<Gem>();

            for (int i = 0; i < modifiedColumns.Length; i++)
            {
                var n = modifiedColumns[i];

                for (var m = gemGrid.Grid.GetLength(0); m > -1; m--)
                {
                    var rowBellow = m + 1;
                    var dropHeight = 0;
                    while (rowBellow < gemGrid.Grid.GetLength(0)
                        && gemGrid.Grid[rowBellow, n].Removed)
                    {
                        dropHeight++;
                        rowBellow++;
                    }

                    if (dropHeight > 0)
                    {
                        droppedGems.Add(gemGrid.Grid[m, n]);

                        gemGrid.SwapGemsPositions(
                            gemGrid.Grid[m, n],
                            gemGrid.Grid[m + dropHeight, n]);
                    }
                }
            }

            return droppedGems.ToArray();
        }

        public void RebuildGems(Gem[] removedGems)
        {
            foreach (var gem in removedGems)
            {
                gem.transform.position = GetGemWorldPosition(gem);
                gem.transform.localScale = Vector3.zero;
                gem.SpriteRenderer.sprite = GameSettingsManager.Instance.GetRandomGem();
                gem.Removed = false;
            }
        }

        public bool HasAvailableCombos()
        {
            for (int m = 0; m < gemGrid.Grid.GetLength(0) - 1; m++)
            {
                for (int n = 0; n < gemGrid.Grid.GetLength(1); n++)
                {
                    SwapGems(gemGrid.Grid[m, n], gemGrid.Grid[m + 1, n]);

                    if (GetGemsCombos(new Gem[] { gemGrid.Grid[m, n], gemGrid.Grid[m + 1, n] }).Count > 0)
                    {
                        SwapGems(gemGrid.Grid[m, n], gemGrid.Grid[m + 1, n]);
                        return true;
                    }
                    else
                    {
                        SwapGems(gemGrid.Grid[m, n], gemGrid.Grid[m + 1, n]);

                        if (n + 1 < gemGrid.Grid.GetLength(1))
                        {
                            SwapGems(gemGrid.Grid[m, n], gemGrid.Grid[m, n + 1]);

                            if (GetGemsCombos(new Gem[] { gemGrid.Grid[m, n], gemGrid.Grid[m, n + 1] }).Count > 0)
                            {
                                SwapGems(gemGrid.Grid[m, n], gemGrid.Grid[m, n + 1]);
                                return true;
                            }
                            else
                            {
                                SwapGems(gemGrid.Grid[m, n], gemGrid.Grid[m, n + 1]);
                            }
                        }
                    }
                }
            }
            return false;
        }

        public void ShufleGrid(int attempt = 0)
        {
            //Max shuffles before rebuilding the grid
            if (attempt == 10)
            {
                BuildGrid();
                return;
            }

            var availablePositions = new List<GridPosition>();
            for (int m = 0; m < gemGrid.Grid.GetLength(0); m++)
            {
                for (int n = 0; n < gemGrid.Grid.GetLength(1); n++)
                {
                    availablePositions.Add(new GridPosition()
                    {
                        Row = m,
                        Column = n
                    });
                }
            }

            for (int m = 0; m < gemGrid.Grid.GetLength(0); m++)
            {
                for (int n = 0; n < gemGrid.Grid.GetLength(1); n++)
                {
                    var randomPosition = availablePositions[Random.Range(0, availablePositions.Count)];
                    gemGrid.Grid[m, n].Position = randomPosition;
                    availablePositions.Remove(randomPosition);
                }
            }

            var newGrid = new Gem[GameSettingsManager.Instance.GridXSize,
                GameSettingsManager.Instance.GridYSize];

            foreach (var gem in gemGrid.Grid)
            {
                newGrid[gem.Position.Row, gem.Position.Column] = gem;
            }

            gemGrid.Grid = newGrid;

            if (!HasAvailableCombos())
            {
                ShufleGrid(attempt + 1);
            }
        }

        #endregion
    }
}