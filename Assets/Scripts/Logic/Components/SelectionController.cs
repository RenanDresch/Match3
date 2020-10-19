using DG.Tweening;
using Game.FX;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Logic
{
    public class SelectionController : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private GridController gridController = default;

        [SerializeField]
        private SoudFXController soundFXController = default;

        [SerializeField]
        private GameTimer gameTimer = default;

        [SerializeField]
        private GeneralAnimator animator = default;

        private Gem selectedGem;

        #endregion

        #region Public Methods

        public void SelectGem(Gem gem)
        {
            if (selectedGem)
            {
                if(selectedGem == gem)
                {
                    return;
                }

                animator.AnimateDeselection(selectedGem.transform);
                selectedGem.SpriteRenderer.sortingOrder = 0;

                if (gridController.CanSwapGems(selectedGem, gem))
                {
                    soundFXController.PlaySwap();

                    gridController.SwapGems(selectedGem, gem);

                    var combos = gridController.GetGemsCombos(new Gem[] { selectedGem, gem });

                    var animation = animator.AnimateSwap(selectedGem.transform, gem.transform);

                    if (combos.Count > 0)
                    {
                        gameTimer.PauseTimer();
                        animation.AppendCallback(() => OnSwapAnimationComplete(combos));
                    }
                    else
                    {
                        gridController.SwapGems(selectedGem, gem);

                        var gemATransform = selectedGem.transform;
                        var gemBTransform = gem.transform;
                    
                        animation.AppendCallback(() => soundFXController.PlaySwap());
                        animation.OnComplete(() =>
                        {
                            animator.AnimateSwap(gemATransform, gemBTransform);
                        });
                    }

                    selectedGem = null;
                    return;
                }
            }

            selectedGem = gem;

            soundFXController.PlaySelection();

            animator.AnimateSelection(selectedGem.transform);
            selectedGem.SpriteRenderer.sortingOrder = 1;
        }

        private void OnSwapAnimationComplete(List<List<Gem>> combos)
        {
            soundFXController.PlayClear();

            var removedGems = combos.SelectMany(x => x).ToArray();

            foreach(var gem in removedGems)
            {
                gem.Removed = true;
            }

            var gemRenerers = removedGems.Select(x => x.SpriteRenderer).ToArray();

            var droppedGems = gridController.DropGems(removedGems);

            var gemDroppedPosition = new Dictionary<Transform, Vector3>();

            foreach(var gem in droppedGems)
            {
                if (!gem.Removed)
                {
                    gemDroppedPosition.Add(
                        gem.transform,
                        gridController.GetGemWorldPosition(gem));
                }
            }

            animator.AnimateGemDrop(gemDroppedPosition);

            //Contar pontos

            var fadeAnimation = animator.AnimateGemFade(gemRenerers);

            fadeAnimation.AppendInterval(0.1f);

            fadeAnimation.AppendCallback(() =>
            {
                gridController.RebuildGems(removedGems);
                gameTimer.StartTimer();
                animator.AnimateGemSpawn(removedGems.Select(x => x.SpriteRenderer).ToArray());
            });
        }

        #endregion
    }
}
