using UnityEngine;

namespace Game.Logic
{
    public class GemGrid
    {
        #region Properties

        public Gem[,] Grid;

        #endregion

        #region Public Methods

        public bool CanCompleteBefore(int m, int n, Sprite gem)
        {
            //Check Line
            if (n > 1)
            {
                bool combinationFound = true;

                for (int i = n - 1; i > n - 3; i--)
                {
                    if (!Grid[m, i].Equals(gem))
                    {
                        combinationFound = false;
                        break;
                    }
                }
                if (combinationFound)
                {
                    return true;
                }
            }

            //Column
            if (m > 1)
            {
                bool combinationFound = true;

                for (int i = m - 1; i > m - 3; i--)
                {
                    if (!Grid[i, n].Equals(gem))
                    {
                        combinationFound = false;
                        break;
                    }
                }
                if (combinationFound)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}