using DG.Tweening;
using Game.Contracts;
using UnityEngine;

namespace Game.Logic
{
    public class SelectionController : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private GridController gridController = default;

        private Gem selectedGem;

        #endregion

        #region Public Methods

        public void SelectGem(Gem gem)
        {
            if (selectedGem)
            {
                selectedGem.transform.DOScale(1, .5f).SetEase(Ease.OutBack);
                selectedGem.SpriteRenderer.sortingOrder = 0;

                if (gridController.CanSwapGems(selectedGem, gem))
                {
                    selectedGem = null;
                    return;
                }
            }

            selectedGem = gem;
            selectedGem.SpriteRenderer.sortingOrder = 1;
            selectedGem.transform.DOScale(1.4f, .5f).SetEase(Ease.OutBack);
        }

        #endregion
    }
}
