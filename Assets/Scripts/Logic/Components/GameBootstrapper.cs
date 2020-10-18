using DG.Tweening;
using Game.FX;
using System.Collections;
using UnityEngine;

namespace Game.Logic
{
    public class GameBootstrapper : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private GameTimer timer = default;
        [SerializeField]
        private GridController gridController = default;

        private IEnumerator gameBoostrapCoroutine;

        #endregion

        #region PrivateMethods

        private void Start()
        {
            GameMusicManager.Instance.SetMusicVolume(1,1).OnComplete(BootstrapGame);
        }

        private void BootstrapGame()
        {
            if(gameBoostrapCoroutine != null)
            {
                StopCoroutine(gameBoostrapCoroutine);
            }
            gameBoostrapCoroutine = BootstrapCoroutine();
            StartCoroutine(gameBoostrapCoroutine);
        }

        private IEnumerator BootstrapCoroutine()
        {
            timer.ResetTimer();
            gridController.BuildGrid();
            yield return new WaitForSecondsRealtime(2);
            timer.StartTimer();
        }

        #endregion
    }
}