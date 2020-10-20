using DG.Tweening;
using Game.Contracts;
using Game.FX;
using System.Collections;
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
        private SoundFXController soundFXController = default;

        [SerializeField]
        private GameTimer gameTimer = default;

        [SerializeField]
        private GeneralAnimator animator = default;

        [SerializeField]
        private ScoreController scoreController = default;

        private Gem selectedGem;

        private bool canSelect = true;

        #endregion

        #region Properties

        public bool PlayingGame { get; set; }
        public System.Action<List<List<Gem>>> OnCombo { get; set; }

        #endregion

        #region Public Methods

        public void SelectGem(Gem gem, SwipeDirection direction)
        {
            if (!canSelect || !PlayingGame)
            {
                return;
            }

            if (selectedGem)
            {
                if (selectedGem == gem)
                {
                    return;
                }

                animator.AnimateDeselection(selectedGem.transform);
                selectedGem.SpriteRenderer.sortingOrder = 0;
                selectedGem = null;
            }

            var gemB = gridController.GetGemAtDirection(gem, direction);

            if (gemB)
            {
                canSelect = false;

                if (gridController.CanSwapGems(gemB, gem))
                {
                    soundFXController.PlaySwap();

                    gridController.SwapGems(gemB, gem);

                    var combos = gridController.GetGemsCombos(new Gem[] { gemB, gem });

                    var animation = animator.AnimateSwap(gemB.transform, gem.transform);

                    if (combos.Count > 0)
                    {
                        gameTimer.PauseTimer();
                        animation.AppendCallback(() => OnComboConfirm(combos));
                    }
                    else
                    {
                        gridController.SwapGems(gemB, gem);

                        var gemATransform = gemB.transform;
                        var gemBTransform = gem.transform;

                        animation.AppendCallback(() => soundFXController.PlaySwap());
                        animation.OnComplete(() =>
                        {
                            animator.AnimateSwap(gemATransform, gemBTransform).OnComplete(() =>
                            {
                                canSelect = true;
                            });
                        });
                    }
                }
            }
        }

        public void SelectGem(Gem gem)
        {
            if (!canSelect || !PlayingGame)
            {
                return;
            }

            if (selectedGem)
            {
                animator.AnimateDeselection(selectedGem.transform);
                selectedGem.SpriteRenderer.sortingOrder = 0;

                if (selectedGem == gem)
                {
                    selectedGem = null;
                    return;
                }

                else if (gridController.CanSwapGems(selectedGem, gem))
                {
                    canSelect = false;

                    soundFXController.PlaySwap();

                    gridController.SwapGems(selectedGem, gem);

                    var combos = gridController.GetGemsCombos(new Gem[] { selectedGem, gem });

                    var animation = animator.AnimateSwap(selectedGem.transform, gem.transform);

                    if (combos.Count > 0)
                    {
                        gameTimer.PauseTimer();
                        animation.AppendCallback(() => OnComboConfirm(combos));
                    }
                    else
                    {
                        gridController.SwapGems(selectedGem, gem);

                        var gemATransform = selectedGem.transform;
                        var gemBTransform = gem.transform;

                        animation.AppendCallback(() => soundFXController.PlaySwap());
                        animation.OnComplete(() =>
                        {
                            animator.AnimateSwap(gemATransform, gemBTransform).OnComplete(() =>
                            {
                                canSelect = true;
                            });
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

        private void OnComboConfirm(List<List<Gem>> combos)
        {
            if (!PlayingGame)
            {
                return;
            }

            OnCombo?.Invoke(combos);

            soundFXController.PlayClear();

            var removedGems = combos.SelectMany(x => x).ToArray();

            foreach (var gem in removedGems)
            {
                gem.Removed = true;
            }

            var gemRenerers = removedGems.Select(x => x.SpriteRenderer).ToArray();

            var droppedGems = gridController.DropGems(removedGems);

            var gemDroppedPosition = new Dictionary<Transform, Vector3>();

            foreach (var gem in droppedGems)
            {
                if (!gem.Removed)
                {
                    gemDroppedPosition.Add(
                        gem.transform,
                        gridController.GetGemWorldPosition(gem));
                }
            }

            animator.AnimateGemMove(gemDroppedPosition);

            var fadeAnimation = animator.AnimateGemFade(gemRenerers);

            fadeAnimation.AppendInterval(0.1f);

            fadeAnimation.AppendCallback(() =>
            {
                gridController.RebuildGems(removedGems);
                animator.AnimateGemSpawn(removedGems.Select(x => x.SpriteRenderer).ToArray());

                var totalModifiedGems = removedGems.Concat(droppedGems).ToArray();

                var newCombos = gridController.GetGemsCombos(totalModifiedGems);

                if (newCombos.Count > 0)
                {
                    StartCoroutine(ComboChainDelay(newCombos));
                }
                else
                {
                    if (!gridController.HasAvailableCombos())
                    {
                        StartCoroutine(ShuffleGemsDelay());
                    }
                    else
                    {
                        gameTimer.StartTimer();
                        canSelect = true;
                    }
                }
            });
        }

        #endregion

        #region Private Methods

        private void OnDestroy()
        {
            OnCombo = null;
        }

        private IEnumerator ComboChainDelay(List<List<Gem>> combos)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            if (PlayingGame)
            {
                OnComboConfirm(combos);
            }
        }

        private IEnumerator ShuffleGemsDelay()
        {
            yield return new WaitForSecondsRealtime(0.25f);
            if (PlayingGame)
            {
                gridController.ShufleGrid();

                var newGemPositions = new Dictionary<Transform, Vector3>();

                foreach (var gem in gridController.AllGems)
                {
                    newGemPositions.Add(
                        gem.transform,
                        gridController.GetGemWorldPosition(gem));
                }

                animator.AnimateGemMove(newGemPositions).AppendCallback(() =>
                {
                    var totalCombos = gridController.GetGemsCombos(gridController.AllGems);
                    if (totalCombos.Count > 0)
                    {
                        StartCoroutine(ComboChainDelay(totalCombos));
                    }
                    else
                    {
                        gameTimer.StartTimer();
                        canSelect = true;
                    }
                });
            }
        }

        #endregion
    }
}
