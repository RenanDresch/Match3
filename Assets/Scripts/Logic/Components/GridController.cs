using DG.Tweening;
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

                    gemGrid.Grid[m, n].Row = m;
                    gemGrid.Grid[m, n].Column = n;

                    var gem = GameSettingsManager.Instance.GetRandomGem();

                    while (gemGrid.CanCompleteBefore(m, n, gem))
                    {
                        gem = GameSettingsManager.Instance.GetRandomGem();
                    }

                    gemGrid.Grid[m, n].SpriteRenderer.sprite = gem;

                    var xPosition = (-((float)GameSettingsManager.Instance.GridXSize - 1) / 2) + n;
                    var yPosition = (((float)GameSettingsManager.Instance.GridYSize - 1) / 2) - m;
                    gemGrid.Grid[m, n].transform.position = new Vector2(xPosition, yPosition);
                }
            }
        }

        public bool CanSwapGems(Gem gemA, Gem gemB)
        {
            bool canSwap = false;

            if (gemA.Row == gemB.Row)
            {
                canSwap = (gemB.Column == (gemA.Column + 1) ||
                    gemB.Column == (gemA.Column - 1));

            }

            else if (gemA.Column == gemB.Column)
            {
                canSwap = (gemB.Row == (gemA.Row + 1) ||
                    gemB.Row == (gemA.Row - 1));
            }

            return canSwap;
        }

        #endregion
    }
}